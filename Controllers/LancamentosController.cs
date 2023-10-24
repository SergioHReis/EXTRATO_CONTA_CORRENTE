using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContaCorrenteAPI.Models;

namespace ContaCorrenteAPI.Controllers
{
    [Route("api/lancamentos")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LancamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetLancamentos(DateTime dataInicial, DateTime dataFinal)
        {
            var lancamentos = await _context.Lancamentos
                .Where(l => l.Data >= dataInicial && l.Data <= dataFinal)
                .ToListAsync();

            return Ok(lancamentos);
        }

        [HttpPost]
        public async Task<ActionResult<Lancamento>> AddLancamento(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLancamento", new { id = lancamento.Id }, lancamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLancamento(int id, Lancamento lancamento)
        {
            if (id != lancamento.Id)
            {
                return BadRequest("ID do lançamento não corresponde.");
            }

            _context.Entry(lancamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/cancelar")]
        public async Task<ActionResult> CancelarLancamento(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento == null)
            {
                return NotFound("Lançamento não encontrado.");
            }

            lancamento.Status = "Cancelado";
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
