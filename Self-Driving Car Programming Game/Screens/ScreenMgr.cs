using Microsoft.Xna.Framework.Graphics;
using Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Driving_Car_Programming_Game.Screens
{
    public static class ScreenMgr
    {
        readonly static Dictionary<string, Screen> Screens = new ();
        static Screen activeScreen;

        public static void LoadScreens()
        {
            Screens.Clear ();
            Screens.Add("gameplay", new Screen_Gameplay());

            activeScreen = Screens["gameplay"];
        }
        
        public static void LoadContent()
        {
            foreach (Screen screen in Screens.Values)
            {
                screen.LoadContent();
            }
        }

        public static void Initialize()
        {
            foreach (Screen screen in Screens.Values)
            {
                screen.Initialize();
            }

        }

        public static void Update()
        {
            activeScreen.Update();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            activeScreen.Draw(spriteBatch);
        }
    }
}
