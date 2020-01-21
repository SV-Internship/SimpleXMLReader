using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using XmlReader.Model;

namespace XmlReader.ViewModel
{
    class FilterViewModel : BaseViewModel
    {
        MainViewModel mainViewModel;
        private Filter _filter;
        
        #region DataBinding

        private string _selectedColor;
        public string SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }
        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public ObservableCollection<string> FilterTypes
        {
            get { return _filter.Types; }
            set 
            {
                _filter.Types = value;
                OnPropertyChanged("FilterTypes");
            }
        }
        public ObservableCollection<string> FilterColors
        {
            get { return _filter.Colors; }
            set
            {
                _filter.Colors = value;
                OnPropertyChanged("FilterColors");
            }
        }
        private ObservableCollection<int> _comboYears;
        public ObservableCollection<int> ComboYears
        {
            get { return _comboYears; }
            set
            {
                _comboYears = value;
                OnPropertyChanged("ComboYears");
            }
        }
        public int FromYear
        {
            get { return _filter.FromYear; }
            set
            {
                _filter.FromYear = value+1950;
                OnPropertyChanged("FromYear");
                Filtering();
            }
        }
        public int ToYear
        {
            get { return _filter.ToYear; }
            set
            {
                _filter.ToYear = value+1950;
                OnPropertyChanged("ToYear");
                Filtering();
            }
        }
        public bool IsGasoline
        {
            get { return _filter.IsGasoline; }
            set
            {
                _filter.IsGasoline = value;
                OnPropertyChanged("IsGasoline");
                Filtering();
            }
        }
        public bool IsDiesel
        {
            get { return _filter.IsDiesel; }
            set
            {
                _filter.IsDiesel = value;
                OnPropertyChanged("IsDiesel");
                Filtering();
            }
        }
        public bool IsCNG
        {
            get { return _filter.IsCNG; }
            set
            {
                _filter.IsCNG = value;
                OnPropertyChanged("IsCNG");
                Filtering();
            }
        }
        public bool IsLPG
        {
            get { return _filter.IsLPG; }
            set
            {
                _filter.IsLPG = value;
                OnPropertyChanged("IsLPG");
                Filtering();
            }
        }
        public bool IsOthers
        {
            get { return _filter.IsOthers; }
            set
            {
                _filter.IsOthers = value;
                OnPropertyChanged("IsOthers");
                Filtering();
            }
        }
        private string _typeText;
        public string TypeText
        {
            get { return _typeText; }
            set
            {
                _typeText = value;
                OnPropertyChanged("TypeText");
            }
        }
        private string _colorText;
        public string ColorText
        {
            get { return _colorText; }
            set
            {
                _colorText = value;
                OnPropertyChanged("ColorText");
            }
        }
        #endregion

        #region commands
        public RelayCommand TypeAdd { get; private set; }
        public RelayCommand TypeDel { get; private set; }
        public RelayCommand ColorAdd { get; private set; }
        public RelayCommand ColorDel { get; private set; }

        public FilterViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            _filter = new Filter();
            FilterTypes = new ObservableCollection<string>();
            FilterColors = new ObservableCollection<string>();
            ComboYears = new ObservableCollection<int>();
            TypeAdd = new RelayCommand(ExTypeAdd);
            TypeDel = new RelayCommand(ExTypeDel);
            ColorAdd = new RelayCommand(ExColorAdd);
            ColorDel = new RelayCommand(ExColorDel);
            for (int i = 1950; i <= 2020 ; i++) ComboYears.Add(i);
            FromYear = 0;
            ToYear = 70;
        }
        private void ExTypeAdd()
        {
            if(TypeText != null)
            {
                FilterTypes.Add(TypeText);
                Filtering();
            }
            
        }
        private void ExTypeDel()
        {
            if (SelectedType != null)
                FilterTypes.Remove(SelectedType);
            Filtering();
        }
        private void ExColorAdd()
        {
            if (ColorText != null)
            {
                FilterColors.Add(ColorText);
                Filtering();
            }
        }
        private void ExColorDel()
        {
            if (SelectedColor != null)
                FilterColors.Remove(SelectedColor);
            Filtering();
        }
        #endregion
        private void Filtering()
        {
            mainViewModel.Cars.Clear();
            ObservableCollection<Car> OriginalCars = new ObservableCollection<Car>(mainViewModel.DummyDataClass);
            for(int i = 0; i < OriginalCars.Count; i++)
            {
                if (ChkFiltering(OriginalCars[i])) mainViewModel.Cars.Add(OriginalCars[i]);
            }
        }
        private bool ChkFiltering(Car c)
        {
            bool type = false;
            bool year = false;
            bool color = false;
            bool fuel;
            
            //filtering type
            for (int i = 0; i < FilterTypes.Count; i++)
            {
                if (c.Type.Contains(FilterTypes[i]))
                {
                    type = true;
                    break;
                }

            }
            if (FilterTypes.Count == 0) type = true;
            
            //filtering color
            for (int i = 0; i < FilterColors.Count; i++)
            {
                if (c.Color.Contains(FilterColors[i]))
                {
                    color = true;
                    break;
                }
            }
            if (FilterColors.Count == 0) color = true;
            
            //filtering fuel
            bool f1, f2, f3, f4, f5;
            f1 = IsGasoline && c.Fuel.Contains("Gasoline");
            f2 = IsCNG && c.Fuel.Contains("CNG");
            f3 = IsDiesel && c.Fuel.Contains("Diesel");
            f4 = IsLPG && c.Fuel.Contains("LPG");
            f5 = IsOthers && !(c.Fuel.Contains("Gasoline") ||
                c.Fuel.Contains("Diesel") ||
                c.Fuel.Contains("CNG") ||
                c.Fuel.Contains("LPG"));

            if (!IsGasoline && !IsCNG && !IsDiesel && !IsLPG && !IsOthers) fuel = true;
            else fuel = f1 || f2 || f3 || f4 || f5;


            //filtering year
            if (FromYear <= c.Years && c.Years <= ToYear) year = true;
            return type && year && fuel && color;
        }
    }
}
