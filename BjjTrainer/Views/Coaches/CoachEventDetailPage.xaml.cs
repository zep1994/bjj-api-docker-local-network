using BjjTrainer.ViewModels.Coaches;

namespace BjjTrainer.Views.Coaches;

[QueryProperty(nameof(EventId), "eventId")]
public partial class CoachEventDetailPage : ContentPage
{
    private readonly CoachEventDetailViewModel _viewModel;

    private int _eventId;
    public int EventId
    {
        get => _eventId;
        set
        {
            _eventId = value;
            if (_eventId > 0)
            {
                LoadEventDetails(_eventId);
            }
        }
    }

    public CoachEventDetailPage()
    {
        InitializeComponent();
        _viewModel = new CoachEventDetailViewModel();
        BindingContext = _viewModel;
    }

    private async void LoadEventDetails(int eventId)
    {
        await _viewModel.Initialize(eventId);
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CoachEventsPage");
    }
}
