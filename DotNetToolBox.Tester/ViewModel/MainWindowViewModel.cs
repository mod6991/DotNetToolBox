using DotNetToolBox.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DotNetToolBox.Tester.ViewModel
{
    public class MainWindowViewModel : ViewModelBase<Window>
    {
        private string _title;

        #region Constructor

        public MainWindowViewModel(Window window)
            : base(window)
        {
            Title = "hello tests !";
        }

        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        #endregion
    }
}
