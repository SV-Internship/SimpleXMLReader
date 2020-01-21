using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XmlReader.ViewModel;

namespace XmlReader.Model
{
    class Filter 
    {
        private ObservableCollection<string> _types;
        private ObservableCollection<string> _colors;
        private int _fromYear;
        private int _toYear;
        private bool _isGasoline;
        private bool _isDiesel;
        private bool _isCNG;
        private bool _isLPG;
        private bool _isOthers;

        public ObservableCollection<string> Types
        {
            get { return _types; }
            set { _types = value; }
        }
        
        public ObservableCollection<string> Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }
        
        public int FromYear
        {
            get { return _fromYear; }
            set { _fromYear = value; }
        }
        
        public int ToYear
        {
            get { return _toYear; }
            set { _toYear = value; }
        }

        public bool IsGasoline
        {
            get { return _isGasoline; }
            set { _isGasoline = value; }
        }
        public bool IsDiesel
        {
            get { return _isDiesel; }
            set { _isDiesel = value; }
        }
        public bool IsCNG
        {
            get { return _isCNG; }
            set { _isCNG = value; }
        }
        public bool IsLPG
        {
            get { return _isLPG; }
            set { _isLPG = value; }
        }
        public bool IsOthers
        {
            get { return _isOthers; }
            set { _isOthers = value; }
        }
    }
}
