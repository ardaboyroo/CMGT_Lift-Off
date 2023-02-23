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
        public bool shot = false;

        public Enemy(Player player, int x, int y, string filename = "Assets/smallship.png", int columns = 17, int rows = 1) : base(filename, columns, rows)
        {
            this.x = x;
            this.y = y;
            scale = 0.7f;
            SetOrigin(width / 2, height / 2);
            this.player = player;
            _enemySpawner = new EnemySpawner(player);
            MyGame myGame = (MyGame)game;
            myGame.enemies.Add(this);
            currentFrame = 9;
            //Console.WriteLine("Spawned at: " + x + " : " + y);
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

        public void CannonAnim()
        {
            if (dead)
            {
                Console.WriteLine("returned");
                return;
            }
            Console.WriteLine("or not");
            if (shot)
            {
                if (timer > 400)
                {
                    timer = 0;
                    if (currentFrame <= 8)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 9;
                        shot = false;
                    }
                }

                timer += Time.deltaTime;

            }
        }

        private void DeathAnim()
        {
            if (timer > 75)
            {
                timer = 0;
                if (currentFrame < 16)
                {
                    currentFrame++;
                }
            }

            timer += Time.deltaTime;
        }

        public void Update()
        {
            CannonAnim();
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
                if (!dead)
                {
                    dead = true;
                    currentFrame = 10;
                }
                DeathAnim();
                if (currentFrame >= 16)
                {
                    myGame.enemies.Remove(this);
                    myGame.score++;
                    myGame.shipSinking.Play();
                    LateDestroy();
                }
            }


        }
    }
}
