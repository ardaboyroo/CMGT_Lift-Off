using System;
using System.Security.Permissions;
using GXPEngine;

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        Player player;
        private EnemySpawner _enemySpawner;
        private int moveSpeed = 1;
        public bool stopped = false;
        public int lives = 1;
        private bool dead = false;
        private int timer = 0;

        public Enemy(Player player, int x, int y, string filename = "Assets/triangle.png", int columns = 1, int rows = 1) : base(filename, columns, rows)
        {
            this.x = x;
            this.y = y;
            SetOrigin(width / 2, height / 2);
            this.player = player;
            _enemySpawner = new EnemySpawner(player);
            MyGame myGame = (MyGame)game;
            myGame.enemies.Add(this);
            Console.WriteLine("Spawned at: " + x + " : " + y);
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
            

            if (Input.GetKey(Key.K))
            {
                lives = 0;
            }

            // Remove if 0 hp:
            if (lives <= 0)
            {
                MyGame myGame = (MyGame)game;
                myGame.enemies.Remove(this);
                LateDestroy();
            }

            
        }
    }
}
