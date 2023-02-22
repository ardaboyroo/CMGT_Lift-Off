using GXPEngine.GXPEngine.Utils;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        private int moveSpeed = 3;          // in pixels per frame
        private int rotationSpeed = 5;      // negative for reversed rotation, positive for normal
        private int lastRotation = 0;
        public bool isAlive = true;
        public int lives = 1;

        public Player(string filename = "Assets/Player_Sprites.png", int columns = 16, int rows = 1) : base(filename, columns, rows)
        {
            SetOrigin(width / 2, height / 2);
            scale = 0.5f;
            x = 1000;
            y = 600;
        }


        public void Update()
        {
            // Move the player based on its current rotation
            Move(0, -moveSpeed);

            if (!ArduinoInput.isConnected)
            {
                if (Input.GetKey(Key.A))
                {
                    rotation -= rotationSpeed;
                }
                else if (Input.GetKey(Key.D))
                {
                    rotation += rotationSpeed;
                }
            }

            if (ArduinoInput.isConnected)
            {
                if (lastRotation < ArduinoInput.rotationCounter)
                {
                    rotation += rotationSpeed;
                }
                else if (lastRotation > ArduinoInput.rotationCounter)
                {
                    rotation -= rotationSpeed;
                }
            }

            //if (currentFrame < 6)
            //{
            //    currentFrame++;
            //}
            //else
            //{
            //    currentFrame = 0;
            //}

            //Bounds for the player so it cannot go outside of the rocks
            if (x <= 64 + width / 2)
            {
                x = 64 + width / 2;
            }

            if (y <= 64 + height / 2)
            {
                y = 64 + height / 2;
            }
            
            //24 and 14 are taken from the New Terrain (btw I think you have naming mapHeight and mapWidth mixed)
            if (x >= 24 * 64 - width / 2)
            {
                x = 24 * 64 - width / 2;
            }

            if (y >= 14 * 64 - height / 2)
            {
                y = 14 * 64 - height / 2;
            }

            lastRotation = ArduinoInput.rotationCounter;

            // Lose game:
            if (lives <= 0)
            {
                isAlive = false;
            }
        }

    }
}
