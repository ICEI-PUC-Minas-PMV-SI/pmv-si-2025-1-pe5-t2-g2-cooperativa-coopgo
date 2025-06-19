using System.Collections.Generic;
using CadastroUsuarios.Models;
using CadastroUsuarios.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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





    }
}
