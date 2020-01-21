using System;
using System.Collections.Generic;
using System.Text;

namespace XmlReader.Model
{
    class Car
    {
        private string _model;
        private string _type;
        private int _years;
        private string _fuel;
        private string _color;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int Years
        {
            get { return _years; }
            set { _years = value; }
        }
        public string Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Car(string Model, string Type, int Years, string Fuel, string Color)
        {
            this._model = Model;
            this._type = Type;
            this._years = Years;
            this._fuel = Fuel;
            this._color = Color;
        }
    }
}
