using BjjTrainer.Models.Moves.BjjTrainer.Models.DTO.Moves;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace BjjTrainer.ViewModels.Components
{
    public partial class MoveSelectionViewModel : BaseViewModel
    {
        public ObservableCollection<UpdateMoveDto> Moves { get; set; } = new();

        public int MoveCount => Moves.Count;

        public MoveSelectionViewModel(ObservableCollection<UpdateMoveDto> moves)
        {
            Moves = moves;
            Moves.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(MoveCount));  // Notify UI when count changes
            };
        }

        public void RefreshList()
        {
            OnPropertyChanged(nameof(Moves));
            OnPropertyChanged(nameof(MoveCount));  // Ensure MoveCount is updated
        }
    }
}
