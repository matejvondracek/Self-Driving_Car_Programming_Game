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
        public string[] script;

        public Car()
        {

        }

        public void LoadScript()
        {
            script = File.ReadAllLines("Autopilot.txt");
        }
    }
}
