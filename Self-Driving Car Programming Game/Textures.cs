using ButtonUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Self_Driving_Car_Programming_Game
{   
    public static class Textures
    {
        private static readonly TextureMgr mgr = new(Game1.self.GraphicsDevice);
        private static Dictionary<string, Texture2D> general;

        public static Dictionary<string, Texture2D> roads;
        public static Dictionary<string, Texture2D> cars;
        public static Dictionary<string, Texture2D> backgrounds;

        private static Color asphaltColor = new(30, 30, 30, 255);
        private static Color middleLineColor = Color.White;
        private static Color sideLineColor = Color.Yellow;

        public static void Generate()
        {
            GenerateGeneral();
            GenerateRoads();
            GenerateCars();
            GenerateBackgrounds();
        }

        private static void GenerateGeneral()
        {
            general = new();
        }

        private static void GenerateRoads()
        {
            roads = new();

            int laneWidth = 200;
            int laneSide = laneWidth / 5;
            int height = 300;
            int width;

            //two lane road
            width = laneWidth * 2 + laneSide * 2;
            Texture2D asphalt = mgr.Rectangle(new Rectangle(0, 0, width, height), asphaltColor);
            Texture2D middleLane = mgr.Rectangle(new Rectangle(0, 0, laneWidth / 10, height / 2), middleLineColor);
            Texture2D sideLane = mgr.Rectangle(new Rectangle(0, 0, laneWidth / 10, height), sideLineColor);
            Texture2D road = mgr.Merge(new Rectangle(0, 0, width, height), asphalt, new Point(0, 0), middleLane, new Point((width - middleLane.Width) / 2, height / 4));
            road = mgr.Merge(new Rectangle(0, 0, width, height), road, new Point(0, 0), sideLane, new Point(laneWidth / 10, 0));
            road = mgr.Merge(new Rectangle(0, 0, width, height), road, new Point(0, 0), sideLane, new Point(width - laneWidth / 5, 0));
            roads.Add("twoLaneRoad", road);
        }

        private static void GenerateCars()
        {

        }

        private static void GenerateBackgrounds()
        {

        }
    }
}
