using GXPEngine.GXPEngine.Utils;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        private int moveSpeed = 3;          // in pixels per frame
        private int boostSpeed = 5;         // speed for when the player touches the wave tile
        private int rotationSpeed = 5;      // negative for reversed rotation, positive for normal
        private int lastRotation = 0;
        public bool isAlive = true;
        public int lives = 1;
        public bool shot = false;
        private int[,] map;
        private MyGame _myGame;
        private float boostTimer = 1000;
        private float time;

        public Player(string filename = "Assets/Player_Sprites.png", int columns = 17, int rows = 1) : base(filename, columns, rows)
        {
            SetOrigin(width / 2, height / 2);
            SetCycle(1, 8);
            scale = 0.5f;
            x = 1000;
            y = 600;
            _myGame = (MyGame)game;
            map = _myGame.GetMap();
            time = boostTimer;
        }


        private void CheckSpeedBoost()
        {
            if (time < boostTimer)
            {
                moveSpeed = boostSpeed;

            }
            time += Time.deltaTime;
        }

        public void CannonAnim()
        {
            if (shot)
            {
                Animate();
                shot = false;

                //if (currentFrame != 8)
                //{
                //    currentFrame++;
                //}
                //else
                //{
                //    currentFrame = 0;
                //    shot = false;
                //}
            }
        }

        private bool PlayerDeathAnim()
        {
            return true;
        }
        public void Update()
        {
            // Move the player based on its current rotation
            Move(0, -moveSpeed);
            CannonAnim();

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
            if (x <= width / 2)
            {
                x = width / 2;
            }

            if (y <= height / 2 - 10)
            {
                y = height / 2 - 10;
            }

            //24 and 14 are taken from the New Terrain (btw I think you have naming mapHeight and mapWidth mixed)
            if (x >= 50 * 64 - width / 2)
            {
                x = 50 * 64 - width / 2;
            }

            if (y >= 25 * 64 - height / 2 + 10)
            {
                y = 25 * 64 - height / 2 + 10;
            }

            if (map != null)
            {
                //Console.WriteLine(map[(int)x/64, (int)y/64]);
                // Console.WriteLine((int)x/64 + " : " + (int)y/64);
                // Console.WriteLine(map[9,15]);           //first y than x

                if (map[(int)y / 64, (int)x / 64] == 1)
                {
                    time = 0;
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

            CheckSpeedBoost();


            lastRotation = ArduinoInput.rotationCounter;

            // Lose game:
            if (lives <= 0)
            {
                isAlive = false;
            }
        }

    }
}
