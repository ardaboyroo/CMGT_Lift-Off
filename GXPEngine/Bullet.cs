using System;
using System.Collections.Generic;
using GXPEngine;

namespace GXPEngine
{
    class Bullet : AnimationSprite
    {
        private int bulletSpeed;
        private GameObject player;
        private int direction;

        public Bullet(GameObject player, int direction, int bulletSpeed, string filename = "Assets/cannonball.png", int columns = 4, int rows = 1) : base(filename, columns, rows)
        {
            this.player = player;
            this.direction = direction;
            this.bulletSpeed = bulletSpeed;
            SetOrigin(width / 2, height / 2);
            //scale = 0.25f;
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
