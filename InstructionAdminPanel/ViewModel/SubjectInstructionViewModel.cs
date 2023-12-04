using CommunityToolkit.Mvvm.ComponentModel;
using DataModel.Entities;
using DataModel.Utility;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionAdminPanel.ViewModel
{
    public partial class SubjectInstructionViewModel : BaseViewModel
    {
       
        private readonly InstructionService _instructionService;
        public SubjectInstructionViewModel(AppSettings appSettings)
        {
            _instructionService = new InstructionService(appSettings);
            LoadInstructions();
        }

        [ObservableProperty]
        private Instruction selectedInstruction = new();

        [ObservableProperty]
        private List<Instruction> instructions = new();

        [ObservableProperty]
        private Instruction instructionData = new();
        private async void LoadInstructions()
        {
            Instructions = await _instructionService.ListAsync();
            SelectedInstruction= Instructions[0];
        }
        public async Task<Instruction> GetInstructionById(string id)
        {
            return await _instructionService.GetById(id);
        }


    }
}
