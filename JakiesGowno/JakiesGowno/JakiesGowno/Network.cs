using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace JakiesGowno
{
    public enum Packet
    {
        Connect = 0,
        DisconnectClient = 1,
        ConnectClient = 2,
        Chat = 3,
        PlayerState = 4,
        MapParam = 5, 
        AreaParam = 6,
        AreaDiffs = 7,
        Unknown = 0xFF
    }

    class Network
    {
        List<Player> listOfClients;
        ContentManager content;
        ServerMap map;

        bool gotMessage = false;
        int messageLength = -1;
        
        TcpClient tcpClient = new TcpClient();
        NetworkStream networkStream;

        public bool bConnected = false;
        bool enabled = true;
        byte[] inBuffer;

        TimeSpan previousTicks = new TimeSpan(0);

        public Network(List<Player> listOfPlayers, ContentManager content, ServerMap map)
        {
            this.content = content;
            this.listOfClients = listOfPlayers;
            this.map = map;
        }

        public void Connect()
        {
            try
            {
                tcpClient.Connect("trewq.pl", 7100);
                networkStream = tcpClient.GetStream();
                inBuffer = new byte[1024];
                networkStream.BeginRead(inBuffer, 0, inBuffer.Length, new AsyncCallback(ReceiveMessage), null);
            }
            catch
            {

            }
        }

        public void ConnectLocalhost()
        {
            try
            {
                tcpClient.Connect("localhost", 7100);
                networkStream = tcpClient.GetStream();
                inBuffer = new byte[1024];
                networkStream.BeginRead(inBuffer, 0, inBuffer.Length, new AsyncCallback(ReceiveMessage), null);
            }
            catch
            {
                
            }
        }

        public void TryJoin(string nick)
        {
            if (!tcpClient.Connected)
            {
                bConnected = false;
                Connect();
                //ConnectLocalhost();
            }
            if (!bConnected)
            {
                byte[] byteMessage = new Byte[nick.Length + 2];
                byteMessage[0] = (byte)Packet.Connect;
                byteMessage[1] = (byte)nick.Length;
                for (int i = 0; i < nick.Length; i++)
                    byteMessage[i + 2] = (byte)nick[i];
                try
                {
                    networkStream.Write(byteMessage, 0, byteMessage.Length);
                }
                catch
                {
                }
                listOfClients[0].nick = nick; // myself
            }
        }

        public void Update(GameTime gameTime)
        {
            if(bConnected)
            {
                if (gameTime.TotalGameTime - previousTicks > TimeSpan.FromMilliseconds(200))
                {
                    previousTicks = gameTime.TotalGameTime;
                    SendPlayerState();
                }
            }
        }

        public void SendPlayerState()
        {
            int[] playerPos = new int[2];
            playerPos[0] = (int)listOfClients[0].position.X;
            playerPos[1] = (int)listOfClients[0].position.Y;

            float[] playerState = new float[4];
            playerState[0] = listOfClients[0].speed.X;
            playerState[1] = listOfClients[0].speed.Y;
            playerState[2] = listOfClients[0].acc.X;
            playerState[3] = listOfClients[0].acc.Y;

            byte[] byteMessage = new byte[3 + 2*sizeof(int) + 4*sizeof(float)];
            byteMessage[0] = (byte)Packet.PlayerState;
            byteMessage[1] = listOfClients[0].id;
            Buffer.BlockCopy(playerPos, 0, byteMessage, 2, sizeof(int) * 2);
            Buffer.BlockCopy(playerState, 0, byteMessage, 2 + 2 * sizeof(int), sizeof(float) * 4);
            byteMessage[26] = (byte)(((float)listOfClients[0].actualHealth / listOfClients[0].maxHealth)*100);

            swapByteOrder4(ref byteMessage, 2);
            swapByteOrder4(ref byteMessage, 2 + sizeof(int));

            swapByteOrder4(ref byteMessage, 2 + 2 * sizeof(int));
            swapByteOrder4(ref byteMessage, 2 + 2 * sizeof(int) + sizeof(float));

            swapByteOrder4(ref byteMessage, 2 + 2 * sizeof(int) + 2*sizeof(float));
            swapByteOrder4(ref byteMessage, 2 + 2 * sizeof(int) + 3*sizeof(float));

            try
            {
                networkStream.Write(byteMessage, 0, byteMessage.Length);
            }
            catch { }
        }

        public void ReceiveMessage(IAsyncResult ar)
        {            
            IPEndPoint e = new IPEndPoint(IPAddress.Any, 7100);
           
            while (enabled)
            {
                if (!gotMessage)
                {
                    try
                    {
                        messageLength = networkStream.EndRead(ar);
                    }
                    catch
                    {
                        return;
                    }
                    if (messageLength == 0)
                    {
                        return;
                    }

                    gotMessage = true;
                    networkStream.BeginRead(inBuffer, 0, inBuffer.Length, new AsyncCallback(ReceiveMessage), null);
                }
            }
        }

        public Packet ReceivedMessage(Chat chat)
        {
            Packet returnPacket = Packet.Unknown;

            int offset = 0;
            if (gotMessage)
            {
                while (offset < messageLength)
                {
                    switch ((Packet)inBuffer[0 + offset])
                    {
                        case Packet.Connect:
                            returnPacket = Packet.Connect;
                            bConnected = true;
                            listOfClients[0].id = inBuffer[1 + offset];
                            chat.AddMessage(new Message("Connected to server.", Color.LightGreen));
                            offset += 2;
                            break;
                        case Packet.DisconnectClient:
                            returnPacket = Packet.DisconnectClient;
                            for (int i = 0; i < listOfClients.Count; i++)
                            {
                                if (listOfClients[i].id == inBuffer[1 + offset])
                                {
                                    chat.AddMessage(new Message("Player " + listOfClients[i].nick + " (" + listOfClients[i].id + ") disconnected.", Color.Red));
                                    listOfClients.RemoveAt(i);
                                    break;
                                }
                            }
                            offset += 2;
                            break;
                        case Packet.ConnectClient:
                            returnPacket = Packet.ConnectClient;
                            string nick;
                            bool exist = false;
                            byte id = inBuffer[1 + offset];
                            byte len = inBuffer[2 + offset];
                            nick = Encoding.ASCII.GetString(inBuffer, 3 + offset, len);
                            foreach (Player nC in listOfClients)
                            {
                                if (nC.id == id)
                                {
                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                chat.AddMessage(new Message("Player " + nick + " (" + id + ") connected.", Color.LightGreen));
                                Animation animation = new Animation();
                                animation.Initialize(listOfClients[0].animation.texture, 9, TimeSpan.FromMilliseconds(100), true);
                                Player player = new Player();
                                player.Initialization(animation, Vector2.Zero, Vector2.Zero, Vector2.Zero, nick, id);
                                listOfClients.Add(player);
                            }
                            offset += 3 + len;
                            break;
                        case Packet.Chat:
                            returnPacket = Packet.Chat;
                            byte srcId = inBuffer[1 + offset];
                            byte dstId = inBuffer[2 + offset];
                            byte messageLen = inBuffer[3 + offset];
                            string message = Encoding.ASCII.GetString(inBuffer, 4 + offset, messageLen);
                            foreach (Player nC in listOfClients)
                            {
                                if (nC.id == srcId)
                                {
                                    Color color;
                                    if (srcId == listOfClients[0].id)
                                        color = Color.LightSkyBlue;
                                    else
                                        color = Color.White;
                                    chat.AddMessage(new Message(nC.nick + ": " + message, color));
                                    break;
                                }
                            }
                            offset += 4 + messageLen;
                            break;
                        case Packet.PlayerState:
                            returnPacket = Packet.PlayerState;
                            byte playerId;
                            byte hp;
                            int[] playerPos = new int[2];
                            float[] playerState = new float[4];

                            playerId = inBuffer[1 + offset];
                            hp = inBuffer[26 + offset];
                            swapByteOrder4(ref inBuffer, 2 + offset);
                            swapByteOrder4(ref inBuffer, 2 + sizeof(int) + offset);
                            swapByteOrder4(ref inBuffer, 2 + 2 * sizeof(int) + offset);
                            swapByteOrder4(ref inBuffer, 2 + 2 * sizeof(int) + sizeof(float) + offset);
                            swapByteOrder4(ref inBuffer, 2 + 2 * sizeof(int) + 2 * sizeof(float) + offset);
                            swapByteOrder4(ref inBuffer, 2 + 2 * sizeof(int) + 3 * sizeof(float) + offset);
                            Buffer.BlockCopy(inBuffer, 2 + offset, playerPos, 0, 2 * sizeof(int));
                            Buffer.BlockCopy(inBuffer, 2 + 2* sizeof(int) + offset, playerState, 0, 4 * sizeof(float));

                            for (int i = 1; i < listOfClients.Count; i++)
                            {
                                if (listOfClients[i].id == playerId)
                                {
                                    listOfClients[i].position = new Vector2(playerPos[0], playerPos[1]);
                                    listOfClients[i].speed = new Vector2(playerState[0], playerState[1]);
                                    listOfClients[i].acc = new Vector2(playerState[2], playerState[3]);
                                    listOfClients[i].actualHealth = (int)(listOfClients[i].maxHealth * (hp / 100.0f));
                                    break;
                                }
                            }
                            offset += 27;
                            break;
                        case Packet.MapParam:
                            returnPacket = Packet.MapParam;
                            if (inBuffer[1 + offset] == 0)
                            {
                                swapByteOrder8(ref inBuffer, 2 + offset);
                                long[] seed = new long[1];
                                Buffer.BlockCopy(inBuffer, 2 + offset, seed, 0, sizeof(long));                                
                                chat.AddMessage(new Message("Got map seed...", Color.Green));
                                map.SetSeed(seed[0]);
                                map.GenerateMap(content);
                                //NodesRequest();
                                offset += sizeof(long); // seed options offset
                            }
                            offset += 2; // header offset
                            break;
                        case Packet.AreaParam:
                            swapByteOrder8(ref inBuffer, 3 + offset);
                            long[] areaSeed = new long[1];
                            Buffer.BlockCopy(inBuffer, 3 + offset, areaSeed, 0, sizeof(long));
                            map.seedsBuffer[inBuffer[1 + offset]] = areaSeed[0];                            
                           // chat.AddMessage(new Message("Got area " + inBuffer[1+offset] + " seed...", Color.Green));
                            if (inBuffer[1 + offset] == 8)
                            {
                                chat.AddMessage(new Message("Got all area seeds, generating map.", Color.Green));
                                returnPacket = Packet.AreaParam;
                                map.GenerateMap(content);
                            }
                            offset += 11;
                            break;
                        case Packet.AreaDiffs:
                            break;
                    }
                }
                gotMessage = false;
                messageLength = -1;
            }
            return returnPacket;
        }

        public bool SendChat(string chatMessage)
        {
            if (!bConnected)
                return false;
            else
            {                
                byte[] byteMessage = new Byte[chatMessage.Length + 4];
                byteMessage[0] = (byte)Packet.Chat;
                byteMessage[1] = listOfClients[0].id;
                byteMessage[2] = 0;
                byteMessage[3] = (byte)chatMessage.Length;
                for (int i = 0; i < chatMessage.Length; i++)
                    byteMessage[i + 4] = (byte)chatMessage[i];

                try
                {
                    networkStream.Write(byteMessage, 0, byteMessage.Length);
                }
                catch { }
                
            }
            return true;
        }

        public void NodesRequest()
        {
            byte[] byteMessage = new byte[6];
            byteMessage[0] = (byte)Packet.AreaParam;
            try
            {
                networkStream.Write(byteMessage, 0, byteMessage.Length);
            }
            catch { }
        }

        public static bool swapByteOrder2 (ref byte[] value, int offset)
        {
            if (offset + 2 > value.Length - 1)
                return false;
            byte temp;            
            temp = value[offset];
            value[offset] = value[offset+1];
            value[offset+1] = temp;
            return true;
        }

        public static bool swapByteOrder4 (ref byte[] value, int offset)
        {
            if (offset + 4 > value.Length - 1)
                return false;

            byte temp;            
            temp = value[offset+3];
            value[offset+3] = value[offset];
            value[offset] = temp;

            temp = value[offset+2];
            value[offset+2] = value[offset+1];
            value[offset + 1] = temp;

            return true;
        }

        public static bool swapByteOrder8(ref byte[] value, int offset)
        {
            if (offset + 8 > value.Length - 1)
                return false;

            byte temp;
            temp = value[offset + 7];
            value[offset + 7] = value[offset];
            value[offset] = temp;

            temp = value[offset + 6];
            value[offset + 6] = value[offset + 1];
            value[offset + 1] = temp;

            temp = value[offset + 5];
            value[offset + 5] = value[offset+2];
            value[offset+2] = temp;

            temp = value[offset + 4];
            value[offset + 4] = value[offset + 3];
            value[offset + 3] = temp;
            return true;
        }
    }
}
