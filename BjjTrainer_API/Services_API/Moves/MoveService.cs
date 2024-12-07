using BjjTrainer_API.Data;
using BjjTrainer_API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using BjjTrainer_API.Models.Moves;

namespace BjjTrainer_API.Services_API.Moves
{
    public class MoveService
    {
        private readonly ApplicationDbContext _context;

        public MoveService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all moves
        public async Task<List<Move>> GetAllMovesAsync()
        {
            return await _context.Moves.ToListAsync();
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
                Tags = move.Tags
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
                Tags = moveDto.Tags
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
            move.Tags = moveDto.Tags;

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
                    Tags = slm.Move.Tags
                })
                .ToListAsync();
        }
    }
}
