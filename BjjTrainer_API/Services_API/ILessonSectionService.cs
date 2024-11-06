using BjjTrainer_API.Models.Lessons;

namespace BjjTrainer_API.Services_API
{
    public interface ILessonSectionService
    {
        Task<IEnumerable<LessonSection>> GetSectionsByLessonIdAsync(int lessonId);
        Task<LessonSection> GetSectionByIdAsync(int id);
        Task<LessonSection> CreateSectionAsync(LessonSection section);
        Task<LessonSection> UpdateSectionAsync(LessonSection section);
        Task<bool> DeleteSectionAsync(int id);
    }
}
