using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using XmlReader.Model;

namespace XmlReader.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private int _sortModel;
        private int _sortType;
        private int _sortYears;
        private int _sortFuel;
        private int _sortColor;

        //Data Binding
        #region DataBinding
        private int _searchIndex;
        public int SearchIndex
        {
            get { return _searchIndex; }
            set
            {
                _searchIndex = value;
                OnPropertyChanged("SearchIndex");
            }
        }
        private List<Car> _dummyDataClass = new List<Car>();
        public List<Car> DummyDataClass
        {
            get { return _dummyDataClass; }
            set
            {
                _dummyDataClass = value;
                OnPropertyChanged("DummyDataClass");
            }
        }
        private ObservableCollection<Car> _cars = new ObservableCollection<Car>();
        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged("Cars");
            }
        }
        //Data Binding View
        private object _contentView1;
        public object ContentView1
        {
            get { return _contentView1; }
            set
            {
                _contentView1 = value;
                OnPropertyChanged("ContentView1");
            }
        }
        private object _contentView2;
        public object ContentView2
        {
            get { return _contentView2; }
            set
            {
                _contentView2 = value;
                OnPropertyChanged("ContentView2");
            }
        }
        private object _contentView3;
        public object ContentView3
        {
            get { return _contentView3; }
            set
            {
                _contentView3 = value;
                OnPropertyChanged("ContentView3");
            }
        }
        #endregion
        #region Commands
        public RelayCommand WindowLoadedCommand { get; private set; }
        public RelayCommand SortModel { get; private set; }
        public RelayCommand SortType { get; private set; }
        public RelayCommand SortYears { get; private set; }
        public RelayCommand SortFuel { get; private set; }
        public RelayCommand SortColor { get; private set; }

        public MainViewModel()
        {
            this.ContentView1 = new OpenViewModel(this);
            this.ContentView2 = new SearchViewModel(this);
            this.ContentView3 = new FilterViewModel(this);
            _sortModel = _sortType = _sortYears = _sortFuel = _sortColor = 1;
            SortModel = new RelayCommand(ExSortModel);
            SortType = new RelayCommand(ExSortType);
            SortYears = new RelayCommand(ExSortYears);
            SortFuel = new RelayCommand(ExSortFuel);
            SortColor = new RelayCommand(ExSortColor);
            _searchIndex = -1;
        }
        private void ExSortModel()
        {
            _sortColor = _sortFuel = _sortYears = _sortType = 1;
            if (_sortModel == 1)
            {
                Cars = new ObservableCollection<Car>(Cars.OrderBy(Car => Car.Model).ToList());
            }
            else
            {
                Cars = new ObservableCollection<Car>(Cars.OrderByDescending(Car => Car.Model).ToList());
            }
            _sortModel = _sortModel ^ 1;
        }
        private void ExSortType()
        {
            _sortColor = _sortFuel = _sortYears = _sortModel = 1;
            if (_sortType == 1)
            {
                Cars = new ObservableCollection<Car>(Cars.OrderBy(Car => Car.Type).ToList());
            }
            else
            {
                Cars = new ObservableCollection<Car>(Cars.OrderByDescending(Car => Car.Type).ToList());
            }
            _sortType = _sortType ^ 1;
        }
        private void ExSortYears()
        {
            _sortColor = _sortFuel = _sortType = _sortModel = 1;
            if (_sortYears == 1)
            {
                Cars = new ObservableCollection<Car>(Cars.OrderBy(Car => Car.Years).ToList());
            }
            else
            {
                Cars = new ObservableCollection<Car>(Cars.OrderByDescending(Car => Car.Years).ToList());
            }
            _sortYears = _sortYears ^ 1;
        }
        private void ExSortFuel()
        {
            _sortColor = _sortYears = _sortType = _sortModel = 1;
            if (_sortFuel == 1)
            {
                Cars = new ObservableCollection<Car>(Cars.OrderBy(Car => Car.Fuel).ToList());
            }
            else
            {
                Cars = new ObservableCollection<Car>(Cars.OrderByDescending(Car => Car.Fuel).ToList());
            }
            _sortFuel = _sortFuel ^ 1;
        }
        private void ExSortColor()
        {
            _sortFuel = _sortYears = _sortType = _sortModel = 1;
            if (_sortColor == 1)
            {
                Cars = new ObservableCollection<Car>(Cars.OrderBy(Car => Car.Color).ToList());
            }
            else
            {
                Cars = new ObservableCollection<Car>(Cars.OrderByDescending(Car => Car.Color).ToList());
            }
            _sortColor = _sortColor ^ 1;
        }
        #endregion
    }
}
