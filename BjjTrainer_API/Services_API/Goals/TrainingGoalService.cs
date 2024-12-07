using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Goals;
using BjjTrainer_API.Models.Joins;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API.Goals
{
    public class TrainingGoalService
    {
        private readonly ApplicationDbContext _context;

        public TrainingGoalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTrainingGoalAsync(CreateTrainingGoalDto dto)
        {
            var goal = new TrainingGoal
            {
                ApplicationUserId = dto.ApplicationUserId,
                GoalDate = dto.GoalDate,
                Notes = dto.Notes
            };

            _context.TrainingGoals.Add(goal);
            await _context.SaveChangesAsync();

            foreach (var moveId in dto.MoveIds)
            {
                _context.UserTrainingGoalMoves.Add(new UserTrainingGoalMove
                {
                    TrainingGoalId = goal.Id,
                    MoveId = moveId
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<TrainingGoal>> GetGoalsByUserAsync(string userId)
        {
            return await _context.TrainingGoals
                .Include(goal => goal.UserTrainingGoalMoves)
                .ThenInclude(tgm => tgm.Move)
                .Where(goal => goal.ApplicationUserId == userId)
                .ToListAsync();
        }
    }
}