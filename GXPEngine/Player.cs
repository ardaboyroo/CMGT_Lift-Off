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
        private int[,] map;
        private MyGame _myGame;
        private float boostTimer = 5;
        private float time = 0;

        public Player(string filename = "Assets/Player_Sprites.png", int columns = 16, int rows = 1) : base(filename, columns, rows)
        {
            SetOrigin(width / 2, height / 2);
            scale = 0.5f;
            x = 1000;
            y = 600;
            _myGame = (MyGame)game;
            map = _myGame.GetMap();
        }


        public void Update()
        {
            // Move the player based on its current rotation
            Move(0, -moveSpeed);

            if (!ArduinoInput.isConnected)
            // {
                if (Input.GetKey(Key.A))
                {
                    rotation -= rotationSpeed;
                }
                else if (Input.GetKey(Key.D))
                {
                    rotation += rotationSpeed;
                }
            // }
            //
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
            if (x <= width / 2)
            {
                x = width / 2;
            }

            if (y <= height / 2 - 10)
            {
                y = height / 2 - 10;
            }
            
            //24 and 14 are taken from the New Terrain (btw I think you have naming mapHeight and mapWidth mixed)
            if (x >= 25 * 64 - width / 2)
            {
                x = 25 * 64 - width / 2;
            }

            if (y >= 15 * 64 - height / 2 + 10)
            {
                y = 15 * 64 - height / 2 + 10;
            }

            if (map != null)
            {
                //Console.WriteLine(map[(int)x/64, (int)y/64]);
                // Console.WriteLine((int)x/64 + " : " + (int)y/64);
                // Console.WriteLine(map[9,15]);           //first y than x

                if (map[(int)y / 64, (int)x / 64] == 1)
                {
                    moveSpeed = 4;
                }
                else if (map[(int)y / 64, (int)x / 64] == 2)
                {
                    moveSpeed = 1;
                }
                else
                {
                    moveSpeed = 3;
                }
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
