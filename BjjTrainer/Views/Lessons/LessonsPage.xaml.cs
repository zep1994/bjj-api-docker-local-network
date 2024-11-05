using BjjTrainer.ViewModels;

namespace BjjTrainer.Views.Lessons
{
    public partial class LessonsPage : ContentPage
    {
        private readonly LessonsViewModel _viewModel;

        public LessonsPage()
        {
            InitializeComponent();
            _viewModel = new LessonsViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadLessonsAsync();
        }

        private void OnReadMoreClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var label = button.Parent.FindByName<Label>("Description");
                if (label != null)
                {
                    if (label.MaxLines == 3)
                    {
                        label.MaxLines = 0; // Expand to show all text
                        button.Text = "Read Less";
                    }
                    else
                    {
                        label.MaxLines = 3; // Collapse to show truncated text
                        button.Text = "Read More";
                    }
                }
            }
        }

    }
}
