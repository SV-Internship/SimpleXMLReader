using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace XmlReader.ViewModel
{
    class SearchViewModel : BaseViewModel
    {
        string[] contents = { "Model", "Type", "Years", "Fuel", "Color" };
        string prev_search ="Godisgoodallthetime";
        MainViewModel mainViewModel;

        #region DataBinding

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                mainViewModel.SearchIndex = -1;
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        #endregion DataBinding

        #region commands
        public RelayCommand SearchCar { get; private set; }
        public SearchViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SearchCar = new RelayCommand(ExSearchCar);
            _selectedIndex = 0;
        }
        private void ExSearchCar()
        {
            if(SearchText == null)
            {
                return;
            }
            if (!(prev_search.Equals(SearchText)))
            {
                mainViewModel.SearchIndex = -1;
            }
            prev_search = SearchText;
            for (int i = mainViewModel.SearchIndex + 1; i < mainViewModel.Cars.Count; i++)
            {
                bool chk = false;
                switch (SelectedIndex)
                {
                    case 0:
                        chk = mainViewModel.Cars[i].Model.Contains(SearchText);
                        break;
                    case 1:
                        chk = mainViewModel.Cars[i].Type.Contains(SearchText);
                        break;
                    case 2:
                        chk = mainViewModel.Cars[i].Years.ToString().Contains(SearchText);
                        break;
                    case 3:
                        chk = mainViewModel.Cars[i].Fuel.Contains(SearchText);
                        break;
                    case 4:
                        chk = mainViewModel.Cars[i].Color.Contains(SearchText);
                        break;
                }
                if (chk)
                {
                    mainViewModel.SearchIndex = i;
                    return;
                }
            }
            MessageBox.Show("Not Founded");
        }
            
        #endregion commands
    }
}
