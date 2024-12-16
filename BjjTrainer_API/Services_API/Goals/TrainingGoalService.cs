using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.DTO.TrainingGoals;
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

        public async Task<TrainingGoalDto> GetTrainingGoalByIdAsync(int goalId)
        {
            var goal = await _context.TrainingGoals
                .Include(g => g.UserTrainingGoalMoves)
                .ThenInclude(tgm => tgm.Move)
                .FirstOrDefaultAsync(g => g.Id == goalId);

            if (goal == null)
                throw new Exception("Training goal not found.");

            return new TrainingGoalDto
            {
                Id = goal.Id,
                GoalDate = goal.GoalDate,
                Notes = goal.Notes,
                MoveIds = goal.UserTrainingGoalMoves.Select(tgm => tgm.MoveId).ToList(),
                Moves = goal.UserTrainingGoalMoves.Select(tgm => new MoveDto
                {
                    Id = tgm.Move.Id,
                    Name = tgm.Move.Name,
                    TrainingLogCount = tgm.Move.TrainingLogCount
                }).ToList()
            };
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

        public async Task UpdateTrainingGoalAsync(int goalId, CreateTrainingGoalDto dto)
        {
            var goal = await _context.TrainingGoals
                .Include(g => g.UserTrainingGoalMoves)
                .FirstOrDefaultAsync(g => g.Id == goalId);

            if (goal == null)
                throw new Exception("Training goal not found.");

            // Update goal fields
            goal.GoalDate = dto.GoalDate;
            goal.Notes = dto.Notes;

            // Remove existing moves
            _context.UserTrainingGoalMoves.RemoveRange(goal.UserTrainingGoalMoves);

            // Add updated moves
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

        public async Task DeleteTrainingGoalAsync(int goalId)
        {
            var goal = await _context.TrainingGoals
                .Include(g => g.UserTrainingGoalMoves)
                .FirstOrDefaultAsync(g => g.Id == goalId);

            if (goal == null)
                throw new Exception("Training goal not found.");

            _context.UserTrainingGoalMoves.RemoveRange(goal.UserTrainingGoalMoves);
            _context.TrainingGoals.Remove(goal);

            await _context.SaveChangesAsync();
        }
    }
}