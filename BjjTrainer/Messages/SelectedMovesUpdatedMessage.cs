namespace BjjTrainer.Messages
{
    public class SelectedMovesUpdatedMessage
    {
        public List<int> SelectedMoveIds { get; }

        public SelectedMovesUpdatedMessage(List<int> selectedMoveIds)
        {
            SelectedMoveIds = selectedMoveIds;
        }
    }
}