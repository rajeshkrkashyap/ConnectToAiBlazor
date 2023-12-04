using DataModel.Entities;
using DataModel.Utility;
using InstructionAdminPanel.ViewModel;
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

namespace InstructionAdminPanel
{
    /// <summary>
    /// Interaction logic for SubjectInstruction.xaml
    /// </summary>
    public partial class SubjectInstruction : UserControl
    {
        //SubjectInstructionViewModel context; 
        //public SubjectInstruction(AppSettings appSettings)
        //{
        //    DataContext = new SubjectInstructionViewModel(appSettings);
        //    InitializeComponent();
        //    context= ((SubjectInstructionViewModel)DataContext);
        //}
        MainWindowViewModel _mainWindowViewModel;
        Instruction _instruction;
        public SubjectInstruction(MainWindowViewModel mainWindowViewModel, Instruction instruction)
        {
            _mainWindowViewModel = mainWindowViewModel;
            InitializeComponent();
            if (instruction.InstructionData != null)
            {
                textBox.Text = instruction.InstructionData;
                _instruction = instruction;
            }

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _instruction.InstructionData = textBox.Text;
          var status =  await _mainWindowViewModel.UpdateInstruction(_instruction);
            if (status)
            {
                MessageBox.Show("Instruction has benn saved successfully!");
            }
            //textBlockMessage.Visibility = Visibility.Collapsed;
        }

        private void Delay()
        {
            Thread.Sleep(1000);
        }

        //private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string id = ((Instruction)comboBoxSubject.SelectedValue).Id;

        //    var instruction = await context.GetInstructionById(id);
        //    textBox.Text = instruction.InstructionData;


        //}
    }
}
