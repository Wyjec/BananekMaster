using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JakiesGowno
{   
    class Camera
    {
        public int xMoveDir = 0; // (< 0 - left, 0 stop, > 0 - right)
        public int yMoveDir = 0; // (<0 up, 0, stop, >0 down)
        public Rectangle cameraRect;
        public Rectangle previousRect;

        public int seedOffsetX = 0;
        public int seedOffsetY = 0;

        public void Initialize(Rectangle initCam)
        {
            Vector2 tempVector = new Vector2(initCam.X, initCam.Y);
            seedOffsetX = ServerMap.ActiveAreaCol(tempVector);
            seedOffsetY = ServerMap.ActiveAreaRow(tempVector);
            Update(initCam);
        }

        public void Update(Rectangle camera)
        {
            if (camera.Top < cameraRect.Top)
                yMoveDir = -1;
            else if (camera.Bottom > cameraRect.Bottom)
                yMoveDir = 1;
            else
                yMoveDir = 0;

            if (camera.Left < cameraRect.Left)
                xMoveDir = -1;
            else if (camera.Right > cameraRect.Right)
                xMoveDir = 1;
            else
                xMoveDir = 0;

            previousRect = cameraRect;
            cameraRect = camera;
        }

        public bool ChangedColRow()
        {
            if (ServerMap.ActiveAreaCol(new Vector2(cameraRect.X, cameraRect.Y)) != ServerMap.ActiveAreaCol(new Vector2(previousRect.X, previousRect.Y)))
            {
                if(xMoveDir < 0)
                    seedOffsetX = -1;
                return true;
            }

            if (ServerMap.ActiveAreaRow(new Vector2(cameraRect.X, cameraRect.Y)) != ServerMap.ActiveAreaRow(new Vector2(previousRect.X, previousRect.Y)))
            {
                if (yMoveDir < 0)
                    seedOffsetY = -1;
                return true;
            }

            return false;
        }

        public bool CenterChangedColRow()
        {
            if (ServerMap.ActiveAreaCol(new Vector2(cameraRect.Center.X, cameraRect.Center.Y)) != ServerMap.ActiveAreaCol(new Vector2(previousRect.Center.X, previousRect.Center.Y)))
            {
                seedOffsetX = 0;
                return true;
            }

            if (ServerMap.ActiveAreaRow(new Vector2(cameraRect.Center.X, cameraRect.Center.Y)) != ServerMap.ActiveAreaRow(new Vector2(previousRect.Center.X, previousRect.Center.Y)))
            {
                seedOffsetY = 0;
                return true;
            }
            
            return false;
        }

        public Vector2 GetCorner() { return new Vector2(cameraRect.X, cameraRect.Y); }
        
    }
}
