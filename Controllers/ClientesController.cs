using System.Collections.Generic;
using CadastroClientes.Models;
using CadastroClientes.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpPost("Salvar")]
        public object Salvar([FromBody] Clientes cadastro)
        {
            try
            {
                ClientesRepository clientes = new ClientesRepository();
                clientes.Salvar(cadastro); // 30min parte6
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpPost("Alterar")]
        public object Alterar([FromBody] Clientes cadastro)
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
            List<Clientes> listaCli = null;
            try
            {
                ClientesRepository clientesRepo = new ClientesRepository();
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
                ClientesRepository clientes = new ClientesRepository();
                bool retornoDelete = clientes.Deletar(Documento);

                return retornoDelete;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpGet("GetCliente")]
        public object GetCliente(string Documento)
        {
            try
            {
                ClientesRepository cliente = new ClientesRepository();
                var retorno = cliente.GetCliente(Documento);
                return retorno;
                
            }
            catch (Exception ex)
            {

            }

            return null;
        }





    }
}
