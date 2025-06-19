using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CadastroUsuarios.Models.Repository
{
    public class UsuariosRepository
    {
        public void Salvar(Usuarios usuarios)
        {
            var listaUsuarios = Listar();

            var item = listaUsuarios.Where(t => t.nome == usuarios.nome).FirstOrDefault();
            
            if (item != null) 
            {
                Deletar(usuarios.nome);
            }

            var usuariosTexto = JsonConvert.SerializeObject(usuarios) + "," + Environment.NewLine; // verificar após criação banco
            File.AppendAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt", usuariosTexto); // verificar após criação banco
        }
        public List<Usuarios> Listar()
        {
            var usuarios = File.ReadAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt"); // verificar após criação banco

            List<Usuarios> usuariosLista = JsonConvert.DeserializeObject<List<Usuarios>>("["+usuarios+"]");
            
            return usuariosLista.OrderByDescending(t=>t.nome).ToList();
        }

        public bool Deletar(string nome)
        {
            var listaUsuarios = Listar();
           var item = listaUsuarios.Where(t=>t.nome == nome).FirstOrDefault();

            if (item != null) 
            {
                listaUsuarios.Remove(item);
                File.WriteAllText("C:\\Users\\Usuario\\Downloads\\CadastroBaseClientes\\CadastroBaseClientes\\BancoDados\\bancodados.txt", string.Empty); // definir arquivo txt e adaptar ao banco de dados após criação


                foreach (var usuario in listaUsuarios)
                {
                    Salvar(usuario);
                }
                return true;
            }
            return false;

        }

        public Usuarios GetUsuario(string nome)
        {
            var usuarioLista = Listar();
            var item = usuarioLista.Where(t => t.nome == nome).FirstOrDefault();

            return item;
        }

        public IActionResult ValidarLogin([FromBody] LoginDTO credenciais)
        {
            try
            {
                // Lista todos os usuários
                var listaUsuarios = Listar();

                // Procura o usuário com o nome e senha fornecidos
                var usuario = listaUsuarios.FirstOrDefault(u =>
                    u.nome == credenciais.nome &&
                    u.senha == credenciais.senha
                );

                if (usuario != null)
                {
                    // Login válido
                    return Ok(new
                    {
                        sucesso = true,
                        mensagem = "Login realizado com sucesso",
                        usuario = new
                        {
                            id = usuario.id,
                            nome = usuario.nome
                            // Não retorna a senha por segurança
                        }
                    });
                }
                else
                {
                    // Credenciais inválidas
                    return Ok(new
                    {
                        sucesso = false,
                        mensagem = "Nome de usuário ou senha incorretos"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    sucesso = false,
                    mensagem = "Erro ao processar login",
                    erro = ex.Message
                });
            }
        }

        // DTO para receber as credenciais
        public class LoginDTO
        {
            public string nome { get; set; }
            public string senha { get; set; }
        }
    }
}
