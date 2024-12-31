using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.DTO.TrainingLogDTOs;
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

        public async Task<List<TrainingLogDto>> GetTrainingLogsAsync(string userId)
        {
            return await _context.TrainingLogs
                .Where(log => log.ApplicationUserId == userId)  
                .Include(log => log.TrainingLogMoves)
                .ThenInclude(tlm => tlm.Move)
                .Select(log => new TrainingLogDto
                {
                    Id = log.Id,
                    Date = log.Date,
                    TrainingTime = log.TrainingTime,
                    RoundsRolled = log.RoundsRolled,
                    Submissions = log.Submissions,
                    Taps = log.Taps,
                    Notes = log.Notes,
                    SelfAssessment = log.SelfAssessment,          
                    MoveIds = log.TrainingLogMoves.Select(tlm => tlm.Move.Id).ToList()
                }).ToListAsync();
        }

        public async Task<TrainingLogDto?> GetTrainingLogByIdAsync(int logId)
        {
            return await _context.TrainingLogs
                .Where(log => log.Id == logId)
                .Include(log => log.TrainingLogMoves)
                .ThenInclude(tlm => tlm.Move)
                .Select(log => new TrainingLogDto
                {
                    Id = log.Id,
                    Date = log.Date,
                    TrainingTime = log.TrainingTime,
                    RoundsRolled = log.RoundsRolled,
                    Submissions = log.Submissions,
                    Taps = log.Taps,
                    Notes = log.Notes,
                    SelfAssessment = log.SelfAssessment,
                    MoveIds = log.TrainingLogMoves.Select(tlm => tlm.Move.Id).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task UpdateTrainingLogAsync(int logId, CreateTrainingLogDto dto)
        {
            var log = await _context.TrainingLogs
                .Include(tl => tl.TrainingLogMoves)
                .FirstOrDefaultAsync(tl => tl.Id == logId);

            if (log == null)
                throw new Exception("Training log not found.");

            // Update log fields
            log.Date = dto.Date;
            log.TrainingTime = dto.TrainingTime;
            log.RoundsRolled = dto.RoundsRolled;
            log.Submissions = dto.Submissions;
            log.Taps = dto.Taps;
            log.Notes = dto.Notes;
            log.SelfAssessment = dto.SelfAssessment;

            // Update moves
            _context.TrainingLogMoves.RemoveRange(log.TrainingLogMoves);
            foreach (var moveId in dto.MoveIds)
            {
                _context.TrainingLogMoves.Add(new TrainingLogMove
                {
                    TrainingLogId = log.Id,
                    MoveId = moveId
                });
            }
            await _context.SaveChangesAsync();
        }

        // ******************************** Sharing a Student Log  ************************************************
        public async Task ToggleTrainingLogSharingAsync(int logId, string userId)
        {
            var trainingLog = await _context.TrainingLogs
                .FirstOrDefaultAsync(tl => tl.Id == logId && tl.ApplicationUserId == userId);

            if (trainingLog == null)
                throw new Exception("Training log not found.");

            trainingLog.IsShared = !trainingLog.IsShared;
            await _context.SaveChangesAsync();
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
                var userLogs = await _context.TrainingLogs
                    .Where(log => log.ApplicationUserId == applicationUserId)
                    .Include(log => log.TrainingLogMoves)
                    .ThenInclude(tlm => tlm.Move)
                    .ToListAsync();

                var trainingGoals = await _context.TrainingGoals
                    .Where(goal => goal.ApplicationUserId == applicationUserId)
                    .Include(goal => goal.UserTrainingGoalMoves)
                    .ThenInclude(gtm => gtm.Move)
                    .ToListAsync();

                if (!userLogs.Any() && !trainingGoals.Any())
                {
                    return new UserProgressDto
                    {
                        TotalGoalsAchieved = 0,
                        TotalMoves = 0,
                        TotalTrainingTime = 0,
                        TotalRoundsRolled = 0,
                        TotalSubmissions = 0,
                        TotalTaps = 0,
                        WeeklyTrainingHours = 0,
                        AverageSessionLength = 0,
                        FavoriteMoveThisMonth = "No data available",
                        MovesPerformed = []
                    };
                }

                var totalTrainingTime = userLogs.Sum(log => log.TrainingTime);
                var totalRoundsRolled = userLogs.Sum(log => log.RoundsRolled);
                var totalSubmissions = userLogs.Sum(log => log.Submissions);
                var totalTaps = userLogs.Sum(log => log.Taps);

                var oneWeekAgo = DateTime.Now.AddDays(-7);
                var weeklyTrainingHours = userLogs
                    .Where(log => log.Date >= oneWeekAgo)
                    .Sum(log => log.TrainingTime / 60);

                var averageSessionLength = userLogs.Average(log => log.TrainingTime);

                var currentMonth = DateTime.Now.Month;
                var favoriteMove = userLogs
                    .Where(log => log.Date.Month == currentMonth)
                    .SelectMany(log => log.TrainingLogMoves)
                    .GroupBy(tlm => tlm.Move.Name)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key ?? "No moves practiced";

                var movesPerformed = userLogs
                    .SelectMany(log => log.TrainingLogMoves)
                    .GroupBy(tlm => tlm.Move.Id)
                    .Select(group => new MoveDto
                    {
                        Id = group.Key,
                        Name = group.First().Move.Name,
                        Description = group.First().Move.Description,
                        SkillLevel = group.First().Move.SkillLevel,
                        TrainingLogCount = group.Count()
                    })
                    .ToList();

                var totalGoalsAchieved = trainingGoals.Count(goal => goal.GoalDate <= DateTime.Now);
                var totalMoves = trainingGoals.SelectMany(goal => goal.UserTrainingGoalMoves).Select(gtm => gtm.Move).Distinct().Count();

                return new UserProgressDto
                {
                    TotalTrainingTime = totalTrainingTime,
                    TotalRoundsRolled = totalRoundsRolled,
                    TotalSubmissions = totalSubmissions,
                    TotalTaps = totalTaps,
                    WeeklyTrainingHours = weeklyTrainingHours,
                    AverageSessionLength = averageSessionLength,
                    FavoriteMoveThisMonth = favoriteMove,
                    TotalGoalsAchieved = totalGoalsAchieved,
                    TotalMoves = totalMoves,
                    MovesPerformed = movesPerformed
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while fetching user progress.", ex);
            }
        }

        public async Task DeleteTrainingLogAsync(int logId)
        {
            var log = await _context.TrainingLogs
                .Include(tl => tl.TrainingLogMoves)
                .FirstOrDefaultAsync(tl => tl.Id == logId);

            if (log == null)
                throw new Exception("Training log not found.");

            _context.TrainingLogMoves.RemoveRange(log.TrainingLogMoves);
            _context.TrainingLogs.Remove(log);

            await _context.SaveChangesAsync();
        }
    }
}