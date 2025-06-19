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
            var usuarios = File.ReadAllText("C:\\Users\\Gabriel\\Downloads\\bancodados\\bancodados.txt");
           
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
    }
}
