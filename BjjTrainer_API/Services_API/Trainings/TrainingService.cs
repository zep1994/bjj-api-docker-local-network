using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Joins;
using BjjTrainer_API.Models.Trainings;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Trainings
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

        public async Task<UserProgressDto> GetUserProgressAsync(string applicationUserId)
        {
            try
            {
                // Fetch all necessary data in a single query
                var userProgressData = await _context.TrainingLogs
                    .Where(log => log.ApplicationUserId == applicationUserId)
                    .Select(log => new
                    {
                        log.TrainingTime,
                        log.RoundsRolled,
                        log.Submissions,
                        log.Taps,
                        Moves = log.TrainingLogMoves
                            .GroupBy(tlm => tlm.MoveId)
                            .Select(g => new
                            {
                                MoveId = g.Key,
                                TrainingLogCount = g.Count(),
                                MoveDetails = g.Select(tlm => tlm.Move).FirstOrDefault()
                            })
                    })
                    .ToListAsync();

                // Aggregate data from the query
                var totalTrainingTime = userProgressData.Sum(log => log.TrainingTime);
                var totalRoundsRolled = userProgressData.Sum(log => log.RoundsRolled);
                var totalSubmissions = userProgressData.Sum(log => log.Submissions);
                var totalTaps = userProgressData.Sum(log => log.Taps);

                var movesPerformed = userProgressData
                    .SelectMany(log => log.Moves)
                    .GroupBy(move => move.MoveId)
                    .Select(group => new MoveDto
                    {
                        Id = group.Key,
                        Name = group.First().MoveDetails.Name,
                        Description = group.First().MoveDetails.Description,
                        SkillLevel = group.First().MoveDetails.SkillLevel,
                        TrainingLogCount = group.Sum(m => m.TrainingLogCount)
                    })
                    .ToList();

                // Assign to the DTO
                var userProgress = new UserProgressDto
                {
                    TotalTrainingTime = totalTrainingTime,
                    TotalRoundsRolled = totalRoundsRolled,
                    TotalSubmissions = totalSubmissions,
                    TotalTaps = totalTaps,
                    MovesPerformed = movesPerformed // Ensure type alignment
                };

                return userProgress;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while fetching user progress.", ex);
            }
        }

    }
}