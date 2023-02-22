using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
//using Terrain.NoiseGenerator;

namespace GXPEngine
{

    internal class Terrain : Pivot
    {
        AnimationSprite[,] terrain;
        public int[,] groundMap;

        int tileWidth;
        int tileHeight;

        int tileSize;

        /// <summary>
        /// Includes all functions for drawing the terrain, and can return the mouse position on the grid.
        /// </summary>


        //for generating a new map

        public Terrain(int camWidth, int camHeight, int mapSize, int gridSize)
        {

            //tile width and height are slightly larger than the screen to avoid nullpointerexceptions and flickering edges
            tileWidth = (camWidth / gridSize) + 3;
            tileHeight = (camHeight / gridSize) + 3;

            terrain = new AnimationSprite[tileWidth, tileHeight];

            this.tileSize = gridSize;

            //sets up an array for all sprites
            for (int x = 0; x < tileWidth; x++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    terrain[x, y] = new AnimationSprite("Assets/Sea_Tiles.png", 3, 1, -1, false, true);
                    terrain[x, y].SetXY(x * gridSize - gridSize / 2, y * gridSize - gridSize / 2);
                    terrain[x, y].scale = gridSize / 16f;
                }
            }
        }

        //for loading the map from a two dimensional array, for use in for example loading a file or pregenerating in a different class

        public Terrain(int camWidth, int camHeight, int mapSize, int tileSize, int[,] groundMap, MyGame main)
        {
            //tile width and height are slightly larger than the screen to avoid nullpointerexceptions and flickering edges
            tileWidth = (camWidth / tileSize) + 3;
            tileHeight = (camHeight / tileSize) + 3;

            terrain = new AnimationSprite[tileWidth, tileHeight];

            //loads in saved maps
            this.groundMap = groundMap;

            this.tileSize = tileSize;


            //// Random map generation, replace with int array
            //this.groundMap = new int[mapSize,mapSize];
            //for(int x = 0; x < mapSize; x++)
            //{
            //    for (int y = 0; y < mapSize; y++)
            //    {
            //        this.groundMap[x,y] = Utils.Random(0,3);
            //    }
            //}

            //and sets up an array for all sprites
            for (int x = 0; x < tileWidth; x++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    terrain[x, y] = new AnimationSprite("Assets/Sea_Tiles.png", 3, 1, -1, false, false);
                    terrain[x, y].SetXY(x * tileSize - tileSize / 2, y * tileSize - tileSize / 2);
                    terrain[x, y].scale = tileSize / 64f;
                    //foreach (AnimationSprite i in terrain)
                    //{
                    //    Console.WriteLine(i.ToString());
                    //}
                    AddChild(terrain[x, y]);
                }
            }
        }

        public AnimationSprite[,] ReturnTerrain()
        {
            return terrain;
        }

        public int[] UpdateTerrain(int camX, int camY)
        {

            //updates all terrain based on the camera position after checking which tile is in the top left

            int leftTile = camX / tileSize;
            int topTile = camY / tileSize;

            //and returns the X and Y of the origin offset, which is kept within one tile
            int[] origin = new int[] { -(camX % tileSize), -(camY % tileSize) };

            for (int x = 0; x < tileWidth; x++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    //terrain[x, y].currentFrame = groundMap[leftTile + x, topTile + y];
                    //Console.WriteLine(terrain[x, y].currentFrame);
                }
            }

            x = origin[0];
            y = origin[1];

            return origin;
        }
    }
}