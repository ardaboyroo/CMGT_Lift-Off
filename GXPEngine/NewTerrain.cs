using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class NewTerrain : Pivot
    {
        public int[,] map;
        //make map here

        const int mapWidth = 15;
        const int mapHeight = 25;

        AnimationSprite[,] mapSprites = new AnimationSprite[mapWidth, mapHeight];

        public NewTerrain()
        {
            map = Map.map;

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    mapSprites[x, y] = new AnimationSprite("Assets/Sea_Tiles.png", 3, 1);
                    mapSprites[x, y].x = y * 64;
                    mapSprites[x, y].y = x * 64;
                    mapSprites[x, y].currentFrame = map[x, y];
                    AddChild(mapSprites[x, y]);
                }
            }
        }

        public void Update()
        {

        }
    }
}
