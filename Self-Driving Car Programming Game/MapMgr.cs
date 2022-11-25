using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonUI;
using System.ComponentModel;
using System.IO;

namespace Self_Driving_Car_Programming_Game
{
    public class MapMgr
    {
        Texture2D map;
        public TextureMgr TxtMgr;
        GraphicsDevice graphics;

        public MapMgr(GraphicsDevice graphicsDevice) 
        {
            graphics = graphicsDevice;
            TxtMgr = new TextureMgr(graphicsDevice);
        }

        public void CreateMap(string path)
        {
            map = TxtMgr.Rectangle(new Rectangle(0, 0, 1600, 900*10), Color.Green);
            Rectangle size = new Rectangle(0, 0, 1600, 900 * 10);           
            Texture2D road = Textures.roads["twoLaneRoad"];
            int left = (size.Width - road.Width) / 2;
            for (int i = 1; i < 30; i++) 
            {
                map = TxtMgr.Merge(size, map, new Point(0,0), road, new Point(left, size.Height - road.Height * i));
            }
            ExportMap(path);
        }

        public void ExportMap(string path)
        {
            Stream stream = File.Create(path);
            map.SaveAsPng(stream, map.Width, map.Height);
        }

        public void LoadMap(string path)
        {
            map = Texture2D.FromFile(graphics, path);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle screen)
        {
            Rectangle destination = new Rectangle(screen.X, screen.Y, map.Width, map.Height);
            spriteBatch.Draw(map, destination, Color.White);
        }


    }
}
