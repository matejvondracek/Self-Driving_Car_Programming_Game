using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Self_Driving_Car_Programming_Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Screens
{
    public class Screen_Gameplay : Screen
    {
        Texture2D background, carTexture;
        int distance = 0;
        Car car = new();
        MapMgr mapMgr;

        public override void LoadContent()
        {
            background = Game1.self.Content.Load<Texture2D>("backgrounds/background");
            carTexture = Game1.self.Content.Load<Texture2D>("car/car");
            car.LoadScript("Autopilot.txt");
            Textures.Generate();
            mapMgr = new MapMgr(Game1.self.GraphicsDevice);
            //mapMgr.CreateMap("C:\\Users\\matej\\source\\repos\\Self-Driving Car Programming Game\\Self-Driving Car Programming Game\\Content\\maps\\map.png");
            mapMgr.LoadMap("C:\\Users\\matej\\source\\repos\\Self-Driving Car Programming Game\\Self-Driving Car Programming Game\\Content\\maps\\map.png");
        }

        public override void Update()
        {
            base.Update();

            car.RunScript();
            distance += car.speed / 10;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            mapMgr.Draw(spriteBatch, new Rectangle(0, -900 * 9 + distance, 1600, 900));

            spriteBatch.Draw(carTexture, new Rectangle((1600 - 200) / 2, (900 - 400) / 2, 200, 400), Color.White);
        }
    }
}
