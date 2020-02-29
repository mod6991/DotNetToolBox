using DotNetToolBox.Icons.OpenIconLibrary;
using DotNetToolBox.MVVM;
using DotNetToolBox.RibbonDock;
using DotNetToolBox.RibbonDock.Ribbon;
using DotNetToolBox.Tester.View;
using DotNetToolBox.Tester.ViewModel;
using System.Windows;

namespace DotNetToolBox.Tester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected RibbonDockWindowViewModel vm;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RibbonDock.RibbonDockWindow win = RibbonDockWindowViewModel.GetNewWindow();
            vm = (RibbonDockWindowViewModel)win.DataContext;

            RibbonButtonViewModel button = new RibbonButtonViewModel("test button");
            button.Command = new RelayCommand(ButtonClick, ReturnTrue);
            button.LargeImage = PngIcons.GetIcon(IconName.Home, IconSize.Size32);
            vm.AddRibbonButton("HomeTab", "1st group", button);

            RibbonButtonViewModel button2 = new RibbonButtonViewModel("test button 2");
            button2.Command = new RelayCommand(ButtonClick2, ReturnTrue);
            button2.LargeImage = PngIcons.GetIcon(IconName.Help, IconSize.Size32);
            vm.AddRibbonButton("HomeTab", "1st group", button2);

            DockDataTemplateSelector.RegisterDockViewModel(typeof(TestDocument), typeof(TestDocumentView));
            vm.Documents.Add(new TestDocument(vm));

            win.Show();
        }

        private bool ReturnTrue(object param)
        {
            return true;
        }

        private void ButtonClick(object param)
        {
            vm.Documents.Add(new TestDocument(vm));
        }

        private void ButtonClick2(object param)
        {
            MessageBox.Show("Test !!!!!");
        }
    }

    
}
