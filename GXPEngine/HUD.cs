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
        public AnimationSprite background;

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
                    background = new AnimationSprite("Assets/EndScreen.png", 3, 1);
                    AddChild(background);

                    scoreText.SetXY(125, 50);
                    scoreText.Text(string.Format("Score: {0}", mygame.score), true);
                    AddChild(scoreText);
                    Console.WriteLine("ADDED");
                    highScoreText.SetXY(MyGame.screenSize.x - 175, 50);
                    AddChild(highScoreText);
                    break;
            }
        }

        public void Update()
        {
            if (mygame.player != null)
            {
                if (mygame.player.isAlive)
                {
                    scoreText.x = mygame.player.x + 400;
                    scoreText.y = mygame.player.y - 300;

                    SetScore(mygame.score);
                }
            }
        }

        public void SetScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.Text(String.Format("Score: {0}", score), true);
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
