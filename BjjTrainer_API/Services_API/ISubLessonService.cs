using BjjTrainer_API.Models.Lessons;

namespace BjjTrainer_API.Services_API
{
    public interface ISubLessonService
    {
        Task<List<SubLesson>> GetSubLessonsBySectionAsync(int lessonSectionId);
        Task<SubLesson> GetSubLessonByIdAsync(int id);
        Task<SubLesson> CreateSubLessonAsync(SubLesson subLesson);
        Task<SubLesson> UpdateSubLessonAsync(SubLesson subLesson);
        Task<bool> DeleteSubLessonAsync(int id);
    }
}
