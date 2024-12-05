using BjjTrainer_API.Data;
using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Users;

namespace BjjTrainer_API.Services_API
{
    public class TrainingService
    {
        private readonly ApplicationDbContext _context;

        public TrainingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTrainingLogAsync(string userId, TrainingLog trainingLog, List<int> moveIds)
        {
            // Validate user exists
            var user = await _context.ApplicationUsers.FindAsync(userId);
            if (user == null) throw new Exception("User not found");

            // Add the training log
            _context.TrainingLogs.Add(trainingLog);
            await _context.SaveChangesAsync();

            // Link moves to the training log
            foreach (var moveId in moveIds)
            {
                var move = await _context.Moves.FindAsync(moveId);
                if (move != null)
                {
                    // Increment training log count for the move
                    move.TrainingLogCount++;

                    // Create a new TrainingLogMove
                    _context.TrainingLogMoves.Add(new TrainingLogMove
                    {
                        TrainingLogId = trainingLog.Id,
                        MoveId = move.Id,
                        SelfAssessment = "Learning" // Default value
                    });
                }
            }

            // Update user's aggregate training statistics
            user.TotalTrainingTime += trainingLog.TrainingTime;
            user.TotalRoundsRolled += trainingLog.RoundsRolled;
            user.TotalSubmissions += trainingLog.Submissions;
            user.TotalTaps += trainingLog.Taps;

            await _context.SaveChangesAsync();
        }
    }

}
