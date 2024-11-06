using BjjTrainer.ViewModels;
using Microsoft.Maui.Controls;

namespace BjjTrainer.Views.Lessons
{
    public partial class LessonSectionPage : ContentPage
    {
        private LessonSectionViewModel _viewModel;

        public LessonSectionPage(int lessonId)
        {
            InitializeComponent();
            // Retrieve the LessonSectionViewModel from the service provider
            _viewModel = Handler.MauiContext.Services.GetService<LessonSectionViewModel>();
            BindingContext = _viewModel;
            LoadSections(lessonId);
        }

        private async void LoadSections(int lessonId)
        {
            await _viewModel.LoadLessonSectionsAsync(lessonId);
        }
    }
}
