using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Moves;
using Microsoft.EntityFrameworkCore;

namespace BjjTrainer_API.Services_API
{
    public class MoveService
    {
        private readonly ApplicationDbContext _context;

        public MoveService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all moves
        public async Task<List<MoveDto>> GetAllMovesAsync()
        {
            return await _context.Moves
                .Select(m => new MoveDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Content = m.Content,
                    SkillLevel = m.SkillLevel,
                    Category = m.Category,
                    StartingPosition = m.StartingPosition,
                    History = m.History,
                    Scenarios = m.Scenarios,
                    CounterStrategies = m.CounterStrategies,
                    Tags = m.Tags,
                    LegalInCompetitions = m.LegalInCompetitions
                })
                .ToListAsync();
        }

        // Retrieve a move by ID
        public async Task<MoveDto> GetMoveByIdAsync(int id)
        {
            var move = await _context.Moves
                .Include(m => m.SubLessonMoves)
                .ThenInclude(slm => slm.SubLesson)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (move == null) return null;

            return new MoveDto
            {
                Id = move.Id,
                Name = move.Name,
                Description = move.Description,
                Content = move.Content,
                SkillLevel = move.SkillLevel,
                Category = move.Category,
                StartingPosition = move.StartingPosition,
                History = move.History,
                Scenarios = move.Scenarios,
                CounterStrategies = move.CounterStrategies,
                Tags = move.Tags,
                LegalInCompetitions = move.LegalInCompetitions
            };
        }

        // Create a new move
        public async Task<MoveDto> CreateMoveAsync(MoveDto moveDto)
        {
            var move = new Move
            {
                Name = moveDto.Name,
                Description = moveDto.Description,
                Content = moveDto.Content,
                SkillLevel = moveDto.SkillLevel,
                Category = moveDto.Category,
                StartingPosition = moveDto.StartingPosition,
                History = moveDto.History,
                Scenarios = moveDto.Scenarios,
                CounterStrategies = moveDto.CounterStrategies,
                Tags = moveDto.Tags,
                LegalInCompetitions = moveDto.LegalInCompetitions
            };

            _context.Moves.Add(move);
            await _context.SaveChangesAsync();

            moveDto.Id = move.Id;
            return moveDto;
        }

        // Update an existing move
        public async Task<bool> UpdateMoveAsync(MoveDto moveDto)
        {
            var move = await _context.Moves.FindAsync(moveDto.Id);
            if (move == null) return false;

            move.Name = moveDto.Name;
            move.Description = moveDto.Description;
            move.Content = moveDto.Content;
            move.SkillLevel = moveDto.SkillLevel;
            move.Category = moveDto.Category;
            move.StartingPosition = moveDto.StartingPosition;
            move.History = moveDto.History;
            move.Scenarios = moveDto.Scenarios;
            move.CounterStrategies = moveDto.CounterStrategies;
            move.Tags = moveDto.Tags;
            move.LegalInCompetitions = moveDto.LegalInCompetitions;

            _context.Entry(move).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete a move
        public async Task<bool> DeleteMoveAsync(int id)
        {
            var move = await _context.Moves.FindAsync(id);
            if (move == null) return false;

            _context.Moves.Remove(move);
            await _context.SaveChangesAsync();

            return true;
        }

        // Retrieve moves associated with a specific SubLesson
        public async Task<List<MoveDto>> GetMovesBySubLessonIdAsync(int subLessonId)
        {
            return await _context.SubLessonMoves
                .Where(slm => slm.SubLessonId == subLessonId)
                .Select(slm => new MoveDto
                {
                    Id = slm.Move.Id,
                    Name = slm.Move.Name,
                    Description = slm.Move.Description,
                    Content = slm.Move.Content,
                    SkillLevel = slm.Move.SkillLevel,
                    Category = slm.Move.Category,
                    StartingPosition = slm.Move.StartingPosition,
                    History = slm.Move.History,
                    Scenarios = slm.Move.Scenarios,
                    CounterStrategies = slm.Move.CounterStrategies,
                    Tags = slm.Move.Tags,
                    LegalInCompetitions = slm.Move.LegalInCompetitions
                })
                .ToListAsync();
        }
    }
}
