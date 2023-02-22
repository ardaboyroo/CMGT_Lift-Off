using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HUD : GameObject
    {
        EasyDraw scoreText;
        EasyDraw highScoreText;
        Font font;
        private string file = "Assets/HighScore.txt";
        StreamWriter writer;
        StreamReader reader;
        MyGame mygame;

        public HUD(int CASE)
        {
            mygame = (MyGame)game;

            font = Utils.LoadFont("Assets/ARCADE.ttf", 40);
            highScoreText = new EasyDraw(500, 50);
            highScoreText.TextFont(font);
            highScoreText.TextAlign(CenterMode.Center, CenterMode.Center);
            highScoreText.Fill(255, 50, 50);
            highScoreText.Text(string.Format("Highscore: {0}", GetHighScore()), true);
            highScoreText.SetOrigin(highScoreText.width / 2, highScoreText.height / 2f);

            scoreText = new EasyDraw(250, 50);
            scoreText.TextFont(font);
            scoreText.TextAlign(CenterMode.Center, CenterMode.Center);
            scoreText.Fill(255, 255, 255);
            scoreText.Text("Score: 0", true);
            scoreText.SetOrigin(125, 25);

            switch (CASE)
            {
                case 0:
                    highScoreText.SetXY(MyGame.screenSize.x / 2, MyGame.screenSize.y / 1.15f);
                    AddChild(highScoreText);
                    break;
                case 1:
                    scoreText.SetXY(MyGame.screenSize.x / 2, 35);
                    AddChild(scoreText);
                    break;
                case 2:
                    highScoreText.SetXY(MyGame.screenSize.x / 2, MyGame.screenSize.y / 2.5f);
                    AddChild(highScoreText);
                    break;
            }
        }

        public void Update()
        {
            Console.WriteLine(parent);
            if (mygame.player != null)
            {
                if (mygame.player.isAlive)
                {
                    x = mygame.player.x + 200;
                    y = mygame.player.y - 200;
                    Console.WriteLine(x + " " + y);
                }
            }
        }

        public void SetScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.Text(String.Format("Kills: {0}", score), true);
            }
        }

        public int GetHighScore()
        {
            // Make sure file exists and has something in it
            if (!File.Exists(file) || new FileInfo(file).Length == 0)
            {
                writer = new StreamWriter(file);
                writer.WriteLine(0.ToString());
                writer.Close();
            }

            reader = new StreamReader(file);
            int score = int.Parse(reader.ReadLine());
            reader.Close();

            return score;
        }
    }
}
