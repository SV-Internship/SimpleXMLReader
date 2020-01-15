using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV_toy1
{
    internal class Car
    {
        public string carName { get; set; }
        public string carType { get; set; }
        public int carYear { get; set; }
        public string carFuel { get; set; }
        public string carColor { get; set; }

        public Car(string name, string type, int year, string fuel, string color)
        {
            this.carName = name;
            this.carType = type;
            this.carYear = year;
            this.carFuel = fuel;
            this.carColor = color;
        }

    }
}
