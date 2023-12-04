using DataModel.Entities;
using DataModel.Utility;
using InstructionAdminPanel.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstructionAdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly AppSettings _appSettings ;
        readonly MainWindowViewModel _mainWindowViewModel;
        SubjectInstruction subjectInstruction; 
        public MainWindow()
        {
            _appSettings = new AppSettings();
            _mainWindowViewModel = new MainWindowViewModel(_appSettings);
            InitializeComponent();
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            //subjectInstruction = new SubjectInstruction(new Instruction());
            //switch (index)
            //{
            //    case 0:
            //        GridPrincipal.Children.Clear();
            //        GridPrincipal.Children.Add(subjectInstruction);
            //        break;
            //    //case 1:
            //    //    GridPrincipal.Children.Clear();
            //    //    GridPrincipal.Children.Add(new UserControlEscolha());
            //    //    break;
            //    default:
            //        break;
            //}
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private async void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var textBlockControl = (TextBlock)((System.Windows.Controls.Panel)((System.Windows.Controls.ContentControl)e.Source).Content).Children[1];
            var title = textBlockControl.Text;
            var instruction = await _mainWindowViewModel.GetSujectInstruction(title);
            subjectInstruction = new SubjectInstruction(_mainWindowViewModel, instruction);
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(subjectInstruction);
        }
    }
}