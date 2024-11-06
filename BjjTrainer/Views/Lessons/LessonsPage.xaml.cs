using BjjTrainer.Models.Lessons;
using BjjTrainer.Services;
using BjjTrainer.ViewModels;

namespace BjjTrainer.Views.Lessons
{
    public partial class LessonsPage : ContentPage
    {
        private readonly LessonsViewModel _viewModel;

        public LessonsPage()
        {
            InitializeComponent();
            // Initialize the HttpClient
            var httpClient = new HttpClient
            {
                BaseAddress = GetApiBaseUrl()
            };
            var apiService = new ApiService();
            _viewModel = new LessonsViewModel(apiService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadLessonsAsync();
        }

        public async void OnViewLessonSectionsClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var selectedLesson = (Lesson)button.BindingContext;

            if (selectedLesson != null)
            {
                await Navigation.PushAsync(new LessonSectionPage(selectedLesson.Id));
            }
        }

        private Uri GetApiBaseUrl()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                return new Uri("http://10.0.2.2:5057/api/"); // Android Emulator
            }
            else
            {
                return new Uri("http://localhost:5057/api/"); 
            }
        }
    }
}
