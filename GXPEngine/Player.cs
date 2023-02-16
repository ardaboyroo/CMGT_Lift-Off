using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        private int moveSpeed = 3;          // in pixels per frame
        private int rotationSpeed = 3;      // in degrees

        public Player(string filename = "Assets/barry.png", int columns = 7, int rows = 1) : base(filename, columns, rows)
        {
            SetOrigin(width / 2, height / 2);
            x = 500;
            y = 500;
        }


        public void Update()
        {
            // Move the player based on its current rotation
            Move(0, -moveSpeed);

            if (Input.GetKey(Key.A))
            {
                rotation -= rotationSpeed;
            }
            else if (Input.GetKey(Key.D))
            {
                rotation += rotationSpeed;
            }

            if (currentFrame < 6)
            {
                currentFrame++;
            }
            else
            {
                currentFrame = 0;
            }
        }

    }
}
