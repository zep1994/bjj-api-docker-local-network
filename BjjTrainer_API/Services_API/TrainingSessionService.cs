using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Training_Sessions;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services
{
    public class TrainingSessionService
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new training session
        public async Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession session)
        {
            _context.TrainingSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        // Get all training sessions for a specific user
        public async Task<List<TrainingSession>> GetUserTrainingSessionsAsync(string userId)
        {
            return await _context.TrainingSessions
                .Where(ts => ts.UserId == userId)
                .OrderByDescending(ts => ts.Date)
                .ToListAsync();
        }

        // Get a specific training session by ID
        public async Task<TrainingSession> GetTrainingSessionByIdAsync(int sessionId)
        {
            return await _context.TrainingSessions
                .FirstOrDefaultAsync(ts => ts.Id == sessionId);
        }

        // Update an existing training session
        public async Task<TrainingSession> UpdateTrainingSessionAsync(int sessionId, TrainingSession updatedSession)
        {
            var session = await _context.TrainingSessions.FindAsync(sessionId);
            if (session == null) return null;

            session.Name = updatedSession.Name;
            session.Duration = updatedSession.Duration;
            session.Date = updatedSession.Date;
            session.Notes = updatedSession.Notes;
            session.Location = updatedSession.Location;
            session.Tags = updatedSession.Tags;
            session.AreasTrained = updatedSession.AreasTrained;
            session.MovesTrained = updatedSession.MovesTrained;
            session.TypeOfTraining = updatedSession.TypeOfTraining;
            session.LessonMoves = updatedSession.LessonMoves;
            session.IntensityLevel = updatedSession.IntensityLevel;
            session.Rating = updatedSession.Rating;
            session.UserId = updatedSession.UserId;

            await _context.SaveChangesAsync();
            return session;
        }

        // Delete a training session
        public async Task<bool> DeleteTrainingSessionAsync(int sessionId)
        {
            var session = await _context.TrainingSessions.FindAsync(sessionId);
            if (session == null) return false;

            _context.TrainingSessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
