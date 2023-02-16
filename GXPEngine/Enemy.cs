﻿using System;
using System.Security.Permissions;
using GXPEngine;

namespace GXPEngine
{
    class Enemy : AnimationSprite
    {
        Player player;
        private int moveSpeed = 1;
        public bool stopped = false;

        public Enemy(Player player, string filename = "Assets/triangle.png", int columns = 1, int rows = 1) : base(filename, columns, rows)
        {
            SetOrigin(width / 2, height / 2);
            this.player = player;
        }

        private void CalculateRotation()
        {
            // Rotate the sprite to the player direction
            rotation = Mathf.CalculateAngleDeg(x, y, player.x, player.y);
        }

        private void CheckStopped()
        {
            if (!stopped)
            {
                if (DistanceTo(player) < 200)
                {
                    stopped = true;
                }
            }
            else
            {
                if (DistanceTo(player) > 300)
                {
                    stopped = false;
                }
            }
        }
        private void CalculateRotateCannon()
        {
            CalculateRotation();
            rotation += 90;
        }

        public void Update()
        {
            CheckStopped();
            if (!stopped)
            {
                CalculateRotation();
                Move(0, -moveSpeed);
            }
            else
            {
                CalculateRotateCannon();
            }
            Console.WriteLine(rotation);
        }

    }
}