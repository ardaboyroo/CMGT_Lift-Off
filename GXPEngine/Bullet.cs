using System;
using System.Collections.Generic;
using GXPEngine;

namespace GXPEngine
{
    class Bullet : Sprite
    {
        private int bulletSpeed = 10;
        private GameObject player;
        private int direction;

        public Bullet(GameObject player, int direction, string filename = "Assets/circle.png") : base(filename)
        {
            this.player = player;
            this.direction = direction;
            SetOrigin(width / 2, height / 2);
            scale = 0.25f;
            x = player.x;
            y = player.y;
            CalculateRotation();
        }

        private void CalculateRotation()
        {
            // Rotate the sprite to the player direction
            rotation = player.rotation;
        }

        public void Update()
        {
            Move(direction * bulletSpeed, 0);
            if (DistanceTo(player) > 500)
            {
                LateDestroy();
            }
        }
    }
}
