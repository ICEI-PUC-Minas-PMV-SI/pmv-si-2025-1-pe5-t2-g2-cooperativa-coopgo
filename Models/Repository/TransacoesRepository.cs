using Microsoft.EntityFrameworkCore;

namespace COOPGO.Models.Repository
{
    public class TransacoesRepository
    {
        private readonly AppDbContext _context;

        public TransacoesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Salvar(Transacao transacao)
        {
            try
            {
                // Se não tem ID, é uma nova transação
                if (transacao.id == 0)
                {
                    _context.Transacoes.Add(transacao);
                }
                else
                {
                    _context.Transacoes.Update(transacao);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar transação", ex);
            }
        }

        public async Task<List<Transacao>> Listar()
        {
            try
            {
                return await _context.Transacoes
                    .OrderByDescending(t => t.data)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar transações", ex);
            }
        }

        public async Task<List<Transacao>> ListarPorUsuario(int usuarioId)
        {
            try
            {
                return await _context.Transacoes
                    .Where(t => t.usuarioId == usuarioId)
                    .OrderByDescending(t => t.data)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar transações do usuário {usuarioId}", ex);
            }
        }

        public async Task<decimal> ObterSaldoUsuario(int usuarioId)
        {
            try
            {
                var saldo = await _context.Transacoes
                    .Where(t => t.usuarioId == usuarioId)
                    .SumAsync(t => (decimal?)t.valor) ?? 0;

                return saldo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter saldo do usuário {usuarioId}", ex);
            }
        }

        // Métodos adicionais úteis

        public async Task<Transacao?> ObterPorId(int id)
        {
            return await _context.Transacoes.FindAsync(id);
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                var transacao = await _context.Transacoes.FindAsync(id);
                if (transacao == null)
                    return false;

                _context.Transacoes.Remove(transacao);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Transacao>> ListarPorPeriodo(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Transacoes
                .Where(t => t.usuarioId == usuarioId &&
                           t.data >= dataInicio &&
                           t.data <= dataFim)
                .OrderByDescending(t => t.data)
                .ToListAsync();
        }

        public async Task<decimal> ObterTotalPorTipo(int usuarioId, string tipo)
        {
            return await _context.Transacoes
                .Where(t => t.usuarioId == usuarioId && t.tipo == tipo)
                .SumAsync(t => (decimal?)t.valor) ?? 0;
        }
    }
}