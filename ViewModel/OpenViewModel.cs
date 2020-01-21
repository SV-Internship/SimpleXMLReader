using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using XmlReader.Model;

namespace XmlReader.ViewModel
{
    class OpenViewModel : BaseViewModel
    {
        MainViewModel mainViewModel;
        #region DataBinding
        private string _xmlPath;
        public string XmlPath
        {
            get { return _xmlPath; }
            set 
            { 
                _xmlPath = value;
                OnPropertyChanged("XmlPath");
            }
        }
        #endregion
        #region Commands
        public RelayCommand FindPath { get; private set; }
        public RelayCommand OpenXml { get; private set; }
        public OpenViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            FindPath = new RelayCommand(ExFindPath);
            OpenXml = new RelayCommand(ExOpenXml);
        }
        private void ExFindPath()
        {
            try
            {
                var dlg = new CommonOpenFileDialog();
                dlg.Filters.Add(new CommonFileDialogFilter("xml", "xml"));
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    XmlPath = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }
        private void ExOpenXml()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(XmlPath);
                XmlNodeList xnList = xml.SelectNodes("CarInfo/CarInfo");
                if (mainViewModel.DummyDataClass.Count > 0)
                {
                    mainViewModel.DummyDataClass.Clear();
                }
                foreach (XmlNode xn in xnList)
                {
                    mainViewModel.DummyDataClass.Add(new Car(xn["Car"].InnerText, xn["Type"].InnerText, int.Parse(xn["Year"].InnerText), xn["Fuel"].InnerText, xn["Color"].InnerText));
                }
                mainViewModel.Cars = new ObservableCollection<Car>(mainViewModel.DummyDataClass);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("XML problem happened!!\r\n" + ex);
            }
            XmlPath = null;
        }
        #endregion
    }
}
