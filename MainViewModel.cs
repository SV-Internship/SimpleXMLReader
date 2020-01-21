using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;

namespace SV_toy1
{
    public class MainViewModel : BasicViewModel 
    {
        public ICommand FileOpenCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ObservableCollection<Car> CarList { get => _carList; set => _carList = value; }
        private ObservableCollection<Car> _carList;
        public CollectionView view;
        
        public MainViewModel()
        {
            CarList =new ObservableCollection<Car>();
            FileOpenCommand = new RelayCommand(FileOpen);
            view = (CollectionView)CollectionViewSource.GetDefaultView(CarList);
        }
        private string _filepath;
        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
                OnPropertyChanged("Filepath");
            }
        }

        private void FileOpen()
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

                if (openFile.ShowDialog() == true)
                {
                    FilePath = openFile.FileName;

                    for (int i = CarList.Count() - 1; i >= 0; i--)
                    {
                        CarList.RemoveAt(i);
                    }

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(FilePath);
                    XmlNodeList xnList = xmlDoc.SelectNodes("ArrayOfCar/Car");

                    foreach (XmlNode xn in xnList)
                    {
                        CarList.Add(new Car(xn["Model"].InnerText, xn["Type"].InnerText, int.Parse(xn["Year"].InnerText), xn["FuelType"].InnerText, xn["Color"].InnerText));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }

        private string _searchName;
        public string SearchName
        {
            get
            {
                return _searchName;
            }
            set
            {
                _searchName = value;
                CollectionViewSource.GetDefaultView(CarList).Refresh();
                OnPropertyChanged("SearchName");
            }
        }

        public CollectionView CarListCollection
        {
            get
            {
                view.Filter = UserFilter;
                return this.view;
            }
        }

        private bool UserFilter(object item)
        {
            try
            {
                if (String.IsNullOrEmpty(SearchName))
                    return true;
                else
                {
                    if (String.Compare(CategorySelection, "Car") == 0)
                    {
                        return ((item as Car).carName.IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (String.Compare(CategorySelection, "Type") == 0)
                    {
                        return ((item as Car).carType.IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (String.Compare(CategorySelection, "Year") == 0)
                    {
                        return ((item as Car).carYear.ToString().IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (String.Compare(CategorySelection, "Fuel") == 0)
                    {
                        return ((item as Car).carFuel.IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else
                    {
                        return ((item as Car).carColor.IndexOf(SearchName, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        private string _categorySelection;
        public string CategorySelection
        {
            get
            {
                return _categorySelection;
            }
            set
            {
                _categorySelection = value.Substring(value.IndexOf(":")+2);
                OnPropertyChanged("CategorySelection");
            }
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            try
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(sortBy, direction));
                view.Refresh();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public void ListViewHeaderClick(object sender, RoutedEventArgs e)
        {
            try
            { 
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

                if (headerClicked != null)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }
                    var header = headerClicked.Content as string;
                    string sortBy = "Car";
                    if (header == "Car") sortBy = "carName";
                    if (header == "Type") sortBy = "carType";
                    if (header == "Year") sortBy = "carYear";
                    if (header == "FuelType") sortBy = "carFuel";
                    if (header == "Color") sortBy = "carColor";

                    Sort(sortBy, direction);
                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
