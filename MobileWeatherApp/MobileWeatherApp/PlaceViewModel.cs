﻿using ReactiveUI;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class PlaceViewModel : ReactiveObject
    {
        public PlaceViewModel(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
