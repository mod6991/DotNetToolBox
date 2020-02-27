using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetToolBox.RibbonDock
{
    public static class RibbonDock
    {
        public static RibbonDockWindow RibbonDockWindow
        {
            get
            {
                RibbonDockWindow window = new RibbonDockWindow();
                RibbonDockWindowViewModel vm = new RibbonDockWindowViewModel(window);
                window.DataContext = vm;
                return window;
            }
        }
    }
}
