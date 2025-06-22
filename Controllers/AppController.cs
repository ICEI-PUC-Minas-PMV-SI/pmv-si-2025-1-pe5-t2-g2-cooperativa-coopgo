using System.Collections.Generic;
using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
using COOPGO.Models.Repository;
using COOPGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static CadastroUsuarios.Models.Repository.UsuariosRepository;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("SalvarUsuario")]
        public async Task<object> Salvar([FromBody] Usuarios cadastro)
        {
            try
            {
                var usuarios = new UsuariosRepository(_context);

                // NÃO define o ID - deixa o banco gerar automaticamente
                cadastro.id = 0; // Garante que seja 0 para novo usuário

                await usuarios.Salvar(cadastro);

                return new
                {
                    sucesso = true,
                    mensagem = "Usuário salvo com sucesso.",
                    usuario = new { id = cadastro.id, nome = cadastro.nome } // Retorna o ID gerado
                };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = ex.Message };
            }
        }

        [HttpPost("Alterar")]
        public async Task<object> Alterar([FromBody] Usuarios cadastro)
        {
            try
            {
                var usuarios = new UsuariosRepository(_context);

                // Verifica se o usuário existe
                var usuarioExistente = await _context.Usuarios.FindAsync(cadastro.id);

                if (usuarioExistente == null)
                {
                    return new { sucesso = false, mensagem = "Usuário não encontrado." };
                }

                // Verifica se o novo nome já está em uso por outro usuário
                var nomeEmUso = await _context.Usuarios
                    .AnyAsync(u => u.nome == cadastro.nome && u.id != cadastro.id);

                if (nomeEmUso)
                {
                    return new { sucesso = false, mensagem = "Este nome já está em uso." };
                }

                // Realiza a alteração
                await usuarios.Alterar(cadastro);

                return new { sucesso = true, mensagem = "Conta alterada com sucesso." };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = "Erro ao alterar conta: " + ex.Message };
            }
        }

        [HttpGet("Listar")]
        public async Task<object> Listar()
        {
            try
            {
                var usuarios = new UsuariosRepository(_context);
                var listaCli = await usuarios.Listar();
                return listaCli;
            }
            catch (Exception ex)
            {
                return new { erro = ex.Message };
            }
        }

        [HttpDelete("DeletarUsuario")]
        public async Task<object> DeletarUsuario([FromBody] DeletarUsuarioDto dados)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var usuarios = new UsuariosRepository(_context);

                // Verifica se o usuário existe
                var usuarioExistente = await _context.Usuarios.FindAsync(dados.id);

                if (usuarioExistente == null)
                {
                    return new { sucesso = false, mensagem = "Usuário não encontrado." };
                }

                // Remove primeiro as transações do usuário (se não estiver configurado cascade delete)
                var transacoes = await _context.Transacoes
                    .Where(t => t.usuarioId == dados.id)
                    .ToListAsync();

                _context.Transacoes.RemoveRange(transacoes);

                // Realiza a exclusão do usuário
                await usuarios.DeletarPorId(dados.id);

                await transaction.CommitAsync();

                return new { sucesso = true, mensagem = "Conta excluída com sucesso." };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new { sucesso = false, mensagem = "Erro ao excluir conta: " + ex.Message };
            }
        }

        [HttpGet("GetUsuario")]
        public async Task<object> GetUsuario(string nome)
        {
            try
            {
                var usuarios = new UsuariosRepository(_context);
                var retorno = await usuarios.GetUsuario(nome);

                if (retorno == null)
                {
                    return new { sucesso = false, mensagem = "Usuário não encontrado." };
                }

                // Remove a senha antes de retornar
                return new
                {
                    id = retorno.id,
                    nome = retorno.nome
                };
            }
            catch (Exception ex)
            {
                return new { erro = ex.Message };
            }
        }

        [HttpPost("VerificarLogin")]
        public async Task<object> VerificarLogin([FromBody] LoginDto credenciais)
        {
            try
            {
                var usuarioEncontrado = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.nome == credenciais.nome && u.senha == credenciais.senha);

                if (usuarioEncontrado != null)
                {
                    return new
                    {
                        sucesso = true,
                        usuario = new
                        {
                            id = usuarioEncontrado.id,
                            nome = usuarioEncontrado.nome
                        }
                    };
                }
                else
                {
                    return new { sucesso = false, mensagem = "Nome de usuário ou senha incorretos." };
                }
            }
            catch (Exception ex)
            {
                return new { sucesso = false, erro = ex.Message };
            }
        }

        public class DeletarUsuarioDto
        {
            public int id { get; set; }
        }

        public class LoginDto
        {
            public string nome { get; set; }
            public string senha { get; set; }
        }

        [HttpPost("AdicionarTransacao")]
        public async Task<object> AdicionarTransacao([FromBody] Transacao transacao)
        {
            try
            {
                var transacoesRepo = new TransacoesRepository(_context);

                // Verifica se o usuário tem saldo suficiente para saque
                if (transacao.tipo == "Saque")
                {
                    var saldoAtual = await transacoesRepo.ObterSaldoUsuario(transacao.usuarioId);

                    if (saldoAtual + transacao.valor < 0) // transacao.valor é negativo para saque
                    {
                        return new { sucesso = false, mensagem = "Saldo insuficiente." };
                    }
                }

                // Define a data atual se não foi informada
                if (transacao.data == default(DateTime))
                {
                    transacao.data = DateTime.Now;
                }

                await transacoesRepo.Salvar(transacao);

                return new { sucesso = true, mensagem = "Transação realizada com sucesso." };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = "Erro ao processar transação: " + ex.Message };
            }
        }

        [HttpGet("ListarExtrato/{usuarioId}")]
        public async Task<object> ListarExtrato(int usuarioId)
        {
            try
            {
                var transacoesRepo = new TransacoesRepository(_context);
                var extrato = await transacoesRepo.ListarPorUsuario(usuarioId);

                return extrato;
            }
            catch (Exception ex)
            {
                return new List<Transacao>();
            }
        }
    }
}
