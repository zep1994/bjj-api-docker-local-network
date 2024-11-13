using BjjTrainer.Models.Lessons;
using BjjTrainer.Services.Lessons;
using BjjTrainer.ViewModels;

namespace BjjTrainer.Views.Lessons
{
    public partial class LessonsPage : ContentPage
    {
        private readonly LessonsViewModel _viewModel;

        public LessonsPage()
        {
            InitializeComponent();
            var lessonService = new LessonService();
            _viewModel = new LessonsViewModel(lessonService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadLessonsAsync();
        }

        public async void OnViewLessonSectionsClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedLesson = (Lesson)button.BindingContext;

            if (selectedLesson != null)
            {
                await Navigation.PushAsync(new LessonSectionPage(selectedLesson.Id, selectedLesson.Title));
            }
        }

        private async void OnAddToFavoritesClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var lessonId = (int)button.CommandParameter;

            bool isAddedToFavorites = await _viewModel.AddToFavoritesAsync(lessonId);

            if (isAddedToFavorites)
            {
                await DisplayAlert("Success", "Lesson added to favorites!", "OK");
                await Shell.Current.GoToAsync("//FavoritesPage"); // Navigate to FavoritesPage
            }
            else
            {
                await DisplayAlert("Error", "Failed to add lesson to favorites.", "OK");
            }
        }

    }
}
