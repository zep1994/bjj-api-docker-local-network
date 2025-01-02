using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Components
{
    public class MoveSelectionViewModel : BaseViewModel
    {
        public ObservableCollection<UpdateMoveDto> Moves { get; set; }
        public Command<UpdateMoveDto> ToggleMoveCommand { get; }

        public MoveSelectionViewModel(ObservableCollection<UpdateMoveDto> moves)
        {
            Moves = new ObservableCollection<UpdateMoveDto>(moves);
            ToggleMoveCommand = new Command<UpdateMoveDto>(ToggleMove);
        }

        private void ToggleMove(UpdateMoveDto move)
        {
            move.IsSelected = !move.IsSelected;
        }

        public ObservableCollection<int> GetSelectedMoveIds()
        {
            return new ObservableCollection<int>(Moves
                .Where(m => m.IsSelected)
                .Select(m => m.Id));
        }
    }
}
