using DotNetToolBox.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.Tester.Model
{
    public class SettingModel : ObservableObject
    {
        private string _section;
        private string _setting;
        private string _value;

        public string Section
        {
            get { return _section; }
            set
            {
                _section = value;
                OnPropertyChanged("Section");
            }
        }

        public string Setting
        {
            get { return _setting; }
            set
            {
                _setting = value;
                OnPropertyChanged("Setting");
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
    }
}
