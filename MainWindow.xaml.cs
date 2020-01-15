using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SV_toy1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filename;
        private string categorySelection;
        ObservableCollection<Car> carList;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();
        }
        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchName.Text))
                return true;
            
            else
            {
                if (String.Compare(categorySelection, "Car") == 0)
                    return ((item as Car).carName.IndexOf(SearchName.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                else if (String.Compare(categorySelection, "Type") == 0)
                    return ((item as Car).carType.IndexOf(SearchName.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                else if (String.Compare(categorySelection, "Year") == 0)
                    return ((item as Car).carYear.ToString().IndexOf(SearchName.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                else if (String.Compare(categorySelection, "Fuel") == 0)
                    return ((item as Car).carFuel.IndexOf(SearchName.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                else
                    return ((item as Car).carColor.IndexOf(SearchName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            carList = new ObservableCollection<Car>();
            CarInfo.ItemsSource = carList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CarInfo.ItemsSource);
            view.Filter = UserFilter;
        }

        private void FileOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                filename = openFile.FileName;
                TextBox textbox = new TextBox();
                FileStatus.Text = filename;

                for (int i = carList.Count() - 1; i >= 0; i--)
                {
                    carList.RemoveAt(i);
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);

                XmlNodeList xnList = xmlDoc.SelectNodes("ArrayOfCar/Car");

                foreach(XmlNode xn in xnList)
                {
                    carList.Add(new Car(xn["Model"].InnerText, xn["Type"].InnerText, int.Parse(xn["Year"].InnerText), xn["FuelType"].InnerText, xn["Color"].InnerText));
                }
            }
        }

        private void ListViewHeaderClickHandle(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
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

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }

                    else
                    {
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Searchtext_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(CarInfo.ItemsSource).Refresh();
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(CarInfo.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void Category_Changed(object sender, SelectionChangedEventArgs e)
        {
            string[] parser = Category.SelectedItem.ToString().Split(' ');
            categorySelection = parser[1];
        }
    }
}
