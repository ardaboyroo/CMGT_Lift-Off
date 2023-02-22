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

        public HUD(int CASE)
        {
            font = Utils.LoadFont("Assets/ARCADE.ttf", 40);
        }


    }
}
