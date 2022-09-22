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
        int distance;
        Car car = new();
        public override void LoadContent()
        {
            background = Game1.self.Content.Load<Texture2D>("backgrounds/background");
            carTexture = Game1.self.Content.Load<Texture2D>("car/car");
            car.LoadScript("Autopilot.txt");
        }

        public override void Update()
        {
            base.Update();

            distance += car.speed / 10;
            car.RunScript();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (distance >= 1800)
            {
                distance -= 1800;
            }
            spriteBatch.Draw(background, new Rectangle(0, -900 + distance, 1600, 1800), Color.White);
            spriteBatch.Draw(background, new Rectangle(0, - 2700 + distance, 1600, 1800), Color.White);
            spriteBatch.Draw(carTexture, new Rectangle((1600 - 200) / 2, (900 - 400) / 2, 200, 400), Color.White);
        }
    }
}
