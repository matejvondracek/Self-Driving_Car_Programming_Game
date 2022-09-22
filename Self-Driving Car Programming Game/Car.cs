using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Driving_Car_Programming_Game
{
    public class Car
    {
        public int speed;
        Translator translator = new();

        public Car()
        {

        }

        public void LoadScript(string file)
        {
            string[] script = File.ReadAllLines(file);
            translator.Translate(script);
        }

        public void RunScript()
        {
            translator.Run(this);
        }
    }
}
