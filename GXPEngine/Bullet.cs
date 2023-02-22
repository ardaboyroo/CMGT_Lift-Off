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

        void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                if (this.player is Player) { }

                else
                {
                    Player player = (Player)other;
                    player.lives--;
                    LateDestroy();
                }
            }

            if (other is Enemy)
            {
                if (this.player is Enemy) { }

                else
                {
                    Enemy enemy = (Enemy)other;
                    enemy.lives--;
                    LateDestroy();
                }
            }
        }
    }
}
