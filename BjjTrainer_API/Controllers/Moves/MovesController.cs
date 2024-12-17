﻿using BjjTrainer_API.Models.DTO;
using BjjTrainer_API.Models.Moves;
using BjjTrainer_API.Services_API.Moves;
using Microsoft.AspNetCore.Mvc;

namespace BjjTrainer_API.Controllers.Moves
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private readonly MoveService _moveService;

        public MovesController(MoveService moveService)
        {
            _moveService = moveService;
        }

        // Get all Moves
        [HttpGet]
        public async Task<ActionResult<List<MoveDto>>> GetAllMoves()
        {
            var moves = await _moveService.GetAllMovesAsync();
            return Ok(moves);
        }

        // Get Move details
        [HttpGet("{id}")]
        public async Task<ActionResult<MoveDto>> GetMoveById(int id)
        {
            var move = await _moveService.GetMoveByIdAsync(id);
            if (move == null)
            {
                return NotFound("Move not found.");
            }

            return Ok(move);
        }

        // Create a new Move
        [HttpPost]
        public async Task<ActionResult<Move>> CreateMove([FromBody] Move move)
        {
            var createdMove = await _moveService.CreateMoveAsync(move);
            return CreatedAtAction(nameof(GetMoveById), new { id = createdMove.Id }, createdMove);
        }

        // Update an existing Move
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMove(int id, [FromBody] MoveDto moveDto)
        {
            if (id != moveDto.Id)
            {
                return BadRequest("Move ID mismatch.");
            }

            var success = await _moveService.UpdateMoveAsync(moveDto);
            if (!success)
            {
                return NotFound("Move not found.");
            }

            return NoContent();
        }

        // Delete a Move
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMove(int id)
        {
            var success = await _moveService.DeleteMoveAsync(id);
            if (!success)
            {
                return NotFound("Move not found.");
            }

            return NoContent();
        }
    }
}
