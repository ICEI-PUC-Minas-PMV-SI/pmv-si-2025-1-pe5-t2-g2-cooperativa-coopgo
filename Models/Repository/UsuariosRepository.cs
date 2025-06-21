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

        public void DeletarPorId(int id)
        {
            var listaUsuarios = Listar();

            // Remove o usuário da lista
            var usuarioRemovido = listaUsuarios.RemoveAll(u => u.id == id);

            if (usuarioRemovido == 0)
            {
                throw new Exception("Usuário não encontrado para exclusão.");
            }

            // Reescreve o arquivo sem o usuário excluído
            File.WriteAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt", string.Empty);

            foreach (var usuario in listaUsuarios)
            {
                var usuarioTexto = JsonConvert.SerializeObject(usuario) + "," + Environment.NewLine;
                File.AppendAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt", usuarioTexto);
            }
        }

        public Usuarios GetUsuario(string nome)
        {
            var usuarioLista = Listar();
            var item = usuarioLista.Where(t => t.nome == nome).FirstOrDefault();

            return item;
        }

        public void Alterar(Usuarios usuario)
        {
            var listaUsuarios = Listar();

            // Remove todos os registros (para reescrever o arquivo)
            File.WriteAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt", string.Empty);

            // Atualiza o usuário na lista
            foreach (var u in listaUsuarios)
            {
                if (u.id == usuario.id)
                {
                    // Atualiza os dados
                    u.nome = usuario.nome;
                    u.senha = usuario.senha;
                }

                // Reescreve no arquivo
                var usuarioTexto = JsonConvert.SerializeObject(u) + "," + Environment.NewLine;
                File.AppendAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt", usuarioTexto);
            }
        }
    }
}
