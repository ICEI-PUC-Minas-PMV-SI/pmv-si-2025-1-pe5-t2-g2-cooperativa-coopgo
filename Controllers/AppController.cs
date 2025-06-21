using System.Collections.Generic;
using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
using COOPGO.Models.Repository;
using COOPGO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static CadastroUsuarios.Models.Repository.UsuariosRepository;

namespace CadastroUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpPost("SalvarUsuario")]
        public object Salvar([FromBody] Usuarios cadastro)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();
                usuarios.Salvar(cadastro);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpPost("Alterar")]
        public object Alterar([FromBody] Usuarios cadastro)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();

                // Verifica se o usuário existe
                var listaUsuarios = usuarios.Listar();
                var usuarioExistente = listaUsuarios.Where(u => u.id == cadastro.id).FirstOrDefault();

                if (usuarioExistente == null)
                {
                    return new { sucesso = false, mensagem = "Usuário não encontrado." };
                }

                // Verifica se o novo nome já está em uso por outro usuário
                var nomeEmUso = listaUsuarios.Where(u => u.nome == cadastro.nome && u.id != cadastro.id).Any();
                if (nomeEmUso)
                {
                    return new { sucesso = false, mensagem = "Este nome já está em uso." };
                }

                // Realiza a alteração
                usuarios.Alterar(cadastro);

                return new { sucesso = true, mensagem = "Conta alterada com sucesso." };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = "Erro ao alterar conta: " + ex.Message };
            }
        }

        [HttpGet("Listar")]
        public object Listar()
        {
            List<Usuarios> listaCli = null;
            try
            {
                UsuariosRepository clientesRepo = new UsuariosRepository();
                 listaCli = clientesRepo.Listar();
            }
            catch (Exception ex)
            {

            }

            return listaCli;
        }

        [HttpDelete("DeletarUsuario")]
        public object DeletarUsuario([FromBody] DeletarUsuarioDto dados)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();

                // Verifica se o usuário existe
                var listaUsuarios = usuarios.Listar();
                var usuarioExistente = listaUsuarios.Where(u => u.id == dados.id).FirstOrDefault();

                if (usuarioExistente == null)
                {
                    return new { sucesso = false, mensagem = "Usuário não encontrado." };
                }

                // Realiza a exclusão
                usuarios.DeletarPorId(dados.id);

                return new { sucesso = true, mensagem = "Conta excluída com sucesso." };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = "Erro ao excluir conta: " + ex.Message };
            }
        }

        public class DeletarUsuarioDto
        {
            public int id { get; set; }
        }

        [HttpGet("GetUsuario")]
        public object GetUsuario(string nome)
        {
            try
            {
                UsuariosRepository usuario = new UsuariosRepository();
                var retorno = usuario.GetUsuario(nome);
                return retorno;
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpPost("VerificarLogin")]
        public object VerificarLogin([FromBody] LoginDto credenciais)
        {
            try
            {
                UsuariosRepository usuarios = new UsuariosRepository();
                var listaUsuarios = usuarios.Listar();

                // Verifica se existe um usuário com o nome e senha fornecidos
                var usuarioEncontrado = listaUsuarios
                    .Where(u => u.nome == credenciais.nome && u.senha == credenciais.senha)
                    .FirstOrDefault();

                if (usuarioEncontrado != null)
                {
                    // Remove a senha antes de retornar
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
                    return new { sucesso = false };
                }
            }
            catch (Exception ex)
            {
                return new { sucesso = false, erro = ex.Message };
            }
        }

        public class LoginDto
        {
            public string nome { get; set; }
            public string senha { get; set; }
        }

        [HttpPost("AdicionarTransacao")]
        public object AdicionarTransacao([FromBody] Transacao transacao)
        {
            try
            {
                TransacoesRepository transacoesRepo = new TransacoesRepository();

                // Verifica se o usuário tem saldo suficiente para saque
                if (transacao.tipo == "Saque")
                {
                    var extrato = transacoesRepo.ListarPorUsuario(transacao.usuarioId);
                    decimal saldoAtual = extrato.Sum(t => t.valor);

                    if (saldoAtual + transacao.valor < 0) // transacao.valor é negativo para saque
                    {
                        return new { sucesso = false, mensagem = "Saldo insuficiente." };
                    }
                }

                // Gera um ID para a transação
                var todasTransacoes = transacoesRepo.Listar();
                transacao.id = todasTransacoes.Count > 0 ? todasTransacoes.Max(t => t.id) + 1 : 1;

                transacoesRepo.Salvar(transacao);

                return new { sucesso = true, mensagem = "Transação realizada com sucesso." };
            }
            catch (Exception ex)
            {
                return new { sucesso = false, mensagem = "Erro ao processar transação: " + ex.Message };
            }
        }

        [HttpGet("ListarExtrato/{usuarioId}")]
        public object ListarExtrato(int usuarioId)
        {
            try
            {
                TransacoesRepository transacoesRepo = new TransacoesRepository();
                var extrato = transacoesRepo.ListarPorUsuario(usuarioId);

                // Ordena por data decrescente (mais recentes primeiro)
                return extrato.OrderByDescending(t => t.data).ToList();
            }
            catch (Exception ex)
            {
                return new List<Transacao>();
            }
        }

    }
}
