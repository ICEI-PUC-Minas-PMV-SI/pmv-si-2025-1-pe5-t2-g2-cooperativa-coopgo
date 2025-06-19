using System.Collections.Generic;
using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
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

            }
            catch (Exception ex)
            {

            }

            return null;
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

        [HttpDelete("Deletar")]
        public object Deletar(string Documento)
        {
            try
            {
                UsuariosRepository clientes = new UsuariosRepository();
                bool retornoDelete = clientes.Deletar(Documento);

                return retornoDelete;
            }
            catch (Exception ex)
            {

            }

            return null;
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


    }
}
