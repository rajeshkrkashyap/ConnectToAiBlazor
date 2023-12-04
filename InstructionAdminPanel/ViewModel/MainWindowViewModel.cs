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
    public partial class MainWindowViewModel : BaseViewModel
    {
        private readonly InstructionService _instructionService;
        public MainWindowViewModel(AppSettings appSettings)
        {
            _instructionService = new InstructionService(appSettings);
        }
        public async Task<Instruction> GetSujectInstruction(string title)
        {
            return await _instructionService.GetByTitle(title);
        }

        public async Task<bool> UpdateInstruction(Instruction instruction)
        {
            return await _instructionService.UpdateAsync(instruction);
        }
    }
}
