using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Dock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using DotNetToolBox.Tester.ViewModel;
using DotNetToolBox.Utils;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace DotNetToolBox.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected RibbonDockWindowViewModel vm2;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RibbonDock.RibbonDockWindow win2 = RibbonDock.RibbonDock.RibbonDockWindow;
            vm2 = (RibbonDockWindowViewModel)win2.DataContext;

            RibbonTabViewModel tab = new RibbonTabViewModel("HomeTab");
            RibbonGroupViewModel group = new RibbonGroupViewModel("1st group");
            RibbonButtonViewModel button = new RibbonButtonViewModel("test button");
            button.Command = new RelayCommand(ButtonClick, ReturnTrue);
            button.LargeImage = AssemblyHelper.GetEmbeddedImage(Assembly.GetAssembly(typeof(App)), "DotNetToolBox.Tester.Images.calendar.png");
            group.Buttons.Add(button);
            tab.Groups.Add(group);
            vm2.Tabs.Add(tab);

            DockDataTemplateSelector.RegisterDockViewModel(typeof(TestDocument), typeof(TestDocumentView));
            vm2.Documents.Add(new TestDocument(vm2));

            win2.Show();

        }

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void ButtonClick(object param)
        {
            vm2.Documents.Add(new TestDocument(vm2));
        }
    }

    
}
