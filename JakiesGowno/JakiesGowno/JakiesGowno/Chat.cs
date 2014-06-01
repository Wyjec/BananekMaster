using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JakiesGowno
{
    class Message
    {
        public string messageString;
        public Color color;

        public Message(string messageString, Color color)
        {
            this.messageString = messageString;
            this.color = color;
        }

    }

    class Chat
    {
        public enum Mode
        {
            Disabled = 0,
            NoBackground = 1,
            Background = 2
        }

        public const int width = 500;
        public const int verticalSpacing = 15;
        List<Message> listOfMessages = new List<Message>();
        int noOfMessages;
        Vector2 chatPosition;
        Vector2 typePosition;
        SpriteFont chatFont;
        string typeMessage;
        public bool typeEnabled = false;
        public Mode mode;

        public void WelcomeMessage()
        {
            Message message = new Message("Welcome at JakiesGowno game.", Color.White);
            AddMessage(message);
            message = new Message("For help type /help.", Color.White);
            AddMessage(message);
            message = new Message("To connect and start playing game press F1.", Color.White);
            AddMessage(message);
            message = new Message("But firstly you should change your nickname.", Color.White);
            AddMessage(message);
            message = new Message("Press enter and type /nick nick to change your nick.", Color.White);
            AddMessage(message);
        }

        public void HelpMessage()
        {
            Message message = new Message("HEEEEEEEEEEELP", Color.WhiteSmoke);
            AddMessage(message);
            message = new Message("F1 - connect, F10 - reset position, F11 - dec. gravity, F12 - inc. gravity, Tab - debug", Color.WhiteSmoke);
            AddMessage(message);
            message = new Message("console commands: /nick, /help, /chat (0,1,2)", Color.WhiteSmoke);
            AddMessage(message);
            message = new Message("fullscreen: alt+enter, chat: enter, movement: cursors, exit: esc", Color.WhiteSmoke);
            AddMessage(message);
        }

        public void Initialize(SpriteFont chatFont, Vector2 chatPosition,Vector2 typePosition, Mode mode = Mode.NoBackground, int noOfMessages = 10)
        {
            this.typeMessage = "";
            this.noOfMessages = noOfMessages;
            this.chatPosition = chatPosition;
            this.typePosition = typePosition;
            this.chatFont = chatFont;
            this.mode = mode;
        }

        public void AddMessage(Message message)
        {
            if(listOfMessages.Count > noOfMessages-1)
                listOfMessages.RemoveAt(0);
            listOfMessages.Add(message);
        }

        public string ToggleEnable()
        {
            string returnString = null;

            typeEnabled = !typeEnabled;
            if (typeEnabled == false)
            {
                if (typeMessage != "")
                {
                    returnString = typeMessage;
                    typeMessage = "";
                }
            }
            return returnString;
        }

        public void UpdateInput(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            if(currentKeyboardState.IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
            {
                if(currentKeyboardState.IsKeyDown(Keys.LeftShift) || previousKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'Q';
                else
                    typeMessage += 'q';
            }
            if(currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || previousKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'W';
                else
                    typeMessage += 'w';
            }
            if (currentKeyboardState.IsKeyDown(Keys.E) && previousKeyboardState.IsKeyUp(Keys.E))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || previousKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'E';
                else
                    typeMessage += 'e';
            }
            if (currentKeyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || previousKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'R';
                else
                    typeMessage += 'r';
            }
            if (currentKeyboardState.IsKeyDown(Keys.T) && previousKeyboardState.IsKeyUp(Keys.T))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'T';
                else
                    typeMessage += 't';
            }
            if (currentKeyboardState.IsKeyDown(Keys.Y) && previousKeyboardState.IsKeyUp(Keys.Y))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'Y';
                else
                    typeMessage += 'y';
            }
            if (currentKeyboardState.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'U';
                else
                    typeMessage += 'u';
            }
            if (currentKeyboardState.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'I';
                else
                    typeMessage += 'i';
            }
            if (currentKeyboardState.IsKeyDown(Keys.O) && previousKeyboardState.IsKeyUp(Keys.O))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'O';
                else
                    typeMessage += 'o';
            }
            if (currentKeyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'P';
                else
                    typeMessage += 'p';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemOpenBrackets) && previousKeyboardState.IsKeyUp(Keys.OemOpenBrackets))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '{';
                else
                    typeMessage += '[';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemCloseBrackets) && previousKeyboardState.IsKeyUp(Keys.OemCloseBrackets))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '}';
                else
                    typeMessage += ']';
            }
            if (currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'A';
                else
                    typeMessage += 'a';
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'S';
                else
                    typeMessage += 's';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'D';
                else
                    typeMessage += 'd';
            }
            if (currentKeyboardState.IsKeyDown(Keys.F) && previousKeyboardState.IsKeyUp(Keys.F))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'F';
                else
                    typeMessage += 'f';
            }
            if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'G';
                else
                    typeMessage += 'g';
            }
            if (currentKeyboardState.IsKeyDown(Keys.H) && previousKeyboardState.IsKeyUp(Keys.H))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'H';
                else
                    typeMessage += 'h';
            }
            if (currentKeyboardState.IsKeyDown(Keys.J) && previousKeyboardState.IsKeyUp(Keys.J))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'J';
                else
                    typeMessage += 'j';
            }
            if (currentKeyboardState.IsKeyDown(Keys.K) && previousKeyboardState.IsKeyUp(Keys.K))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'K';
                else
                    typeMessage += 'k';
            }
            if (currentKeyboardState.IsKeyDown(Keys.L) && previousKeyboardState.IsKeyUp(Keys.L))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'L';
                else
                    typeMessage += 'l';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemSemicolon) && previousKeyboardState.IsKeyUp(Keys.OemSemicolon))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += ':';
                else
                    typeMessage += ';';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemQuotes) && previousKeyboardState.IsKeyUp(Keys.OemQuotes))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '"';
                else
                    typeMessage += '\'';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemPipe) && previousKeyboardState.IsKeyUp(Keys.OemPipe))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '|';
                else
                    typeMessage += '\\';
            }
            if (currentKeyboardState.IsKeyDown(Keys.Z) && previousKeyboardState.IsKeyUp(Keys.Z))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'Z';
                else
                    typeMessage += 'z';
            }
            if (currentKeyboardState.IsKeyDown(Keys.X) && previousKeyboardState.IsKeyUp(Keys.X))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'X';
                else
                    typeMessage += 'x';
            }
            if (currentKeyboardState.IsKeyDown(Keys.C) && previousKeyboardState.IsKeyUp(Keys.C))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'C';
                else
                    typeMessage += 'c';
            }
            if (currentKeyboardState.IsKeyDown(Keys.V) && previousKeyboardState.IsKeyUp(Keys.V))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'V';
                else
                    typeMessage += 'v';
            }
            if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'B';
                else
                    typeMessage += 'b';
            }
            if (currentKeyboardState.IsKeyDown(Keys.N) && previousKeyboardState.IsKeyUp(Keys.N))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'N';
                else
                    typeMessage += 'n';
            }
            if (currentKeyboardState.IsKeyDown(Keys.M) && previousKeyboardState.IsKeyUp(Keys.M))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += 'M';
                else
                    typeMessage += 'm';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemComma) && previousKeyboardState.IsKeyUp(Keys.OemComma))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '<';
                else
                    typeMessage += ',';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemPeriod) && previousKeyboardState.IsKeyUp(Keys.OemPeriod))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '>';
                else
                    typeMessage += '.';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemQuestion) && previousKeyboardState.IsKeyUp(Keys.OemQuestion))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '?';
                else
                    typeMessage += '/';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemTilde) && previousKeyboardState.IsKeyUp(Keys.OemTilde))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '~';
                else
                    typeMessage += '`';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D1) && previousKeyboardState.IsKeyUp(Keys.D1))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '!';
                else
                    typeMessage += '1';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D2) && previousKeyboardState.IsKeyUp(Keys.D2))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '@';
                else
                    typeMessage += '2';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D3) && previousKeyboardState.IsKeyUp(Keys.D3))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '#';
                else
                    typeMessage += '3';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D4) && previousKeyboardState.IsKeyUp(Keys.D4))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '$';
                else
                    typeMessage += '4';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D5) && previousKeyboardState.IsKeyUp(Keys.D5))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '%';
                else
                    typeMessage += '5';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D6) && previousKeyboardState.IsKeyUp(Keys.D6))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '^';
                else
                    typeMessage += '6';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D7) && previousKeyboardState.IsKeyUp(Keys.D7))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '&';
                else
                    typeMessage += '7';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D8) && previousKeyboardState.IsKeyUp(Keys.D8))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '*';
                else
                    typeMessage += '8';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D9) && previousKeyboardState.IsKeyUp(Keys.D9))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '(';
                else
                    typeMessage += '9';
            }
            if (currentKeyboardState.IsKeyDown(Keys.D0) && previousKeyboardState.IsKeyUp(Keys.D0))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += ')';
                else
                    typeMessage += '0';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemMinus) && previousKeyboardState.IsKeyUp(Keys.OemMinus))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '_';
                else
                    typeMessage += '-';
            }
            if (currentKeyboardState.IsKeyDown(Keys.OemPlus) && previousKeyboardState.IsKeyUp(Keys.OemPlus))
            {
                if (currentKeyboardState.IsKeyDown(Keys.LeftShift) || currentKeyboardState.IsKeyDown(Keys.RightShift))
                    typeMessage += '+';
                else
                    typeMessage += '=';
            }
            if (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                typeMessage += ' ';
            }
            if (currentKeyboardState.IsKeyDown(Keys.Back) && previousKeyboardState.IsKeyUp(Keys.Back))
            {
                if(typeMessage.Length > 0)
                    typeMessage = typeMessage.Remove(typeMessage.Length - 1, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
        {
            if (mode > Mode.Disabled)
            {
                if (mode == Mode.Background)
                {
                    spriteBatch.Draw(pixelTexture, new Rectangle((int)chatPosition.X, (int)chatPosition.Y, width, 4 + listOfMessages.Count * 15), Color.LightSeaGreen);
                    if (typeEnabled)
                        spriteBatch.Draw(pixelTexture, new Rectangle((int)typePosition.X, (int)typePosition.Y, width, 4 + (int)chatFont.MeasureString("|").Y), Color.LightSeaGreen);
                }
                for (int i = 0; i < listOfMessages.Count; i++)
                {
                    Vector2 shift = new Vector2(5, 2 + i * verticalSpacing);
                    spriteBatch.DrawString(chatFont, listOfMessages[i].messageString, chatPosition + shift, listOfMessages[i].color);

                }
            }
            spriteBatch.DrawString(chatFont, typeMessage, typePosition + new Vector2(5, 2), Color.White);
            if (typeEnabled)
                spriteBatch.DrawString(chatFont, "|", new Vector2(typePosition.X + chatFont.MeasureString(typeMessage).X + 5, typePosition.Y + 2), Color.Red);
        }
    }
}
