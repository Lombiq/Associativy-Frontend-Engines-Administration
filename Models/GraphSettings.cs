﻿
namespace Associativy.Frontends.Administration.Models
{
    public class GraphSettings
    {
        public bool UseCache { get; set; }
        public int InitialZoomLevel { get; set; }
        public int ZoomLevelCount { get; set; }
        public int MaxDistance { get; set; }
        public int MaxConnectionCount { get; set; }


        public GraphSettings()
        {
            UseCache = true;
            InitialZoomLevel = 0;
            ZoomLevelCount = 10;
            MaxDistance = Associativy.Models.Services.MindSettings.Default.MaxDistance;
            MaxConnectionCount = 50;
        }
    }
}