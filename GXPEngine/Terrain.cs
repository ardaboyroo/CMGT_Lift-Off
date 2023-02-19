//using GXPEngine.Managers;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GXPEngine;
////using Terrain.NoiseGenerator;

//namespace Terrain
//{

//    internal class DrawTerrain
//    {
//        AnimationSprite[,] terrain;
//        public int[,] groundMap;

//        int tileWidth;
//        int tileHeight;

//        /// <summary>
//        /// Includes all functions for drawing the terrain, and can return the mouse position on the grid.
//        /// </summary>


//        //for generating a new map

//        public DrawTerrain(int camWidth, int camHeight, int mapSize, int gridSize)
//        {

//            //tile width and height are slightly larger than the screen to avoid nullpointerexceptions and flickering edges
//            tileWidth = (camWidth / gridSize) + 3;
//            tileHeight = (camHeight / gridSize) + 3;

//            terrain = new AnimationSprite[tileWidth, tileHeight];

//            //sets up an array for all sprites
//            for (int x = 0; x < tileWidth; x++)
//            {
//                for (int y = 0; y < tileHeight; y++)
//                {
//                    //NO COLLISION
//                    terrain[x, y] = new AnimationSprite("groundTiles.png", 8, 4, -1, false, false);
//                    terrain[x, y].SetXY(x * gridSize - gridSize / 2, y * gridSize - gridSize / 2);
//                    terrain[x, y].scale = gridSize / 16f;
//                }
//            }
//        }

//        //for loading the map from a two dimensional array, for use in for example loading a file or pregenerating in a different class

//        public DrawTerrain(int camWidth, int camHeight, int mapSize, int gridSize, int[,] groundMap)
//        {
//            //tile width and height are slightly larger than the screen to avoid nullpointerexceptions and flickering edges
//            tileWidth = (camWidth / gridSize) + 3;
//            tileHeight = (camHeight / gridSize) + 3;

//            terrain = new AnimationSprite[tileWidth, tileHeight];

//            //loads in saved maps
//            this.groundMap = groundMap;

//            //and sets up an array for all sprites
//            for (int x = 0; x < tileWidth; x++)
//            {
//                for (int y = 0; y < tileHeight; y++)
//                {
//                    terrain[x, y] = new AnimationSprite("Assets/groundTiles.png", 8, 4, -1, false, false);
//                    terrain[x, y].SetXY(x * gridSize - gridSize / 2, y * gridSize - gridSize / 2);
//                    terrain[x, y].scale = gridSize / 16f;
//                }
//            }
//        }

//        public AnimationSprite[,] ReturnTerrain()
//        {
//            return terrain;
//        }

//        public int[] UpdateTerrain()
//        {

//            //updates all terrain based on the camera position after checking which tile is in the top left

//            int leftTile = camX / gridSize;
//            int topTile = camY / gridSize;

//            //and returns the X and Y of the origin offset, which is kept within one tile
//            int[] origin = new int[] { -(camX % gridSize), -(camY % gridSize) };

//            for (int x = 0; x < tileWidth; x++)
//            {
//                for (int y = 0; y < tileHeight; y++)
//                {
//                    resources[x, y].currentFrame = resourceMap[leftTile + x, topTile + y];

//                    terrain[x, y].currentFrame = groundMap[leftTile + x, topTile + y] + 8 * terrainRandomness[leftTile + x, topTile + y];
//                }
//            }

//            return origin;
//        }

//        public int[] mouseHoverPoint(int camX, int camY)
//        {
//            int mouseTileX = (camX + Input.mouseX + (gridSize / 2)) / gridSize;
//            int mouseTileY = (camY + Input.mouseY + (gridSize / 2)) / gridSize;

//            return new int[] { mouseTileX, mouseTileY };
//        }
//    }
//}