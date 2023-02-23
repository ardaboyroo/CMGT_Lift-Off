using System;
using System.Collections.Generic;

namespace GXPEngine
{
    /// <summary>
    /// Class responsible for spawning and respawning enemies
    /// </summary>
    public class EnemySpawner : GameObject
    {
        private List<Enemy> _enemies = new List<Enemy>();
        private int mapWidth = 49 * 64;
        private int mapHeight = 24 * 64;
        private Random rand = new Random();
        private Player player;

        public EnemySpawner(Player player)
        {
            this.player = player;
        }

        public void StartSpawnEnemies(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                int enemyX = rand.Next(64, mapWidth - 64);
                int enemyY = rand.Next(64, mapHeight - 64);
                Enemy newEnemy = new Enemy(player, enemyX, enemyY);
                _enemies.Add(newEnemy);
                AddChild(newEnemy);
            }
        }

        private void Update()
        {
            MyGame myGame = (MyGame)game;
            if (myGame.enemies.Count == 0)
            {
                StartSpawnEnemies(3);
            }
        }
    }
}