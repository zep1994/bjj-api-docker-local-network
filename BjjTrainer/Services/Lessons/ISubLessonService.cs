using BjjTrainer.Models;

namespace BjjTrainer.Services.Lessons
{
    public interface ISubLessonService
    {
        Task<IEnumerable<SubLesson>> GetSubLessonsBySectionIdAsync(int lessonSectionId);
    }
}
