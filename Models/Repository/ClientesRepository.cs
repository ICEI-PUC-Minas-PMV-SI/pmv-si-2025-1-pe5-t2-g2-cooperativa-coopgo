using Newtonsoft.Json;

namespace CadastroClientes.Models.Repository
{
    public class ClientesRepository
    {
        public void Salvar(Clientes clientes)
        {
            var listaClientes = Listar();

            var item = listaClientes.Where(t => t.Documento == clientes.Documento).FirstOrDefault();

            
            if (item != null) 
            {
                Deletar(clientes.Documento);
            }

            var clientesTexto = JsonConvert.SerializeObject(clientes) + "," + Environment.NewLine;
            File.AppendAllText("C:\\Users\\Usuario\\Downloads\\CadastroBaseClientes\\CadastroBaseClientes\\BancoDados\\bancodados.txt", clientesTexto);
        }
        public List<Clientes> Listar()
        {
            var clientes = File.ReadAllText("C:\\Users\\Usuario\\Downloads\\CadastroBaseClientes\\CadastroBaseClientes\\BancoDados\\bancodados.txt");
           
            List<Clientes> clientesLista = JsonConvert.DeserializeObject<List<Clientes>>("["+clientes+"]");
            
            return clientesLista.OrderByDescending(t=>t.Nome).ToList();
        }

        public bool Deletar(string Documento)
        {
            var listaClientes = Listar();
           var item = listaClientes.Where(t=>t.Documento == Documento).FirstOrDefault();

            if (item != null) 
            {
                listaClientes.Remove(item);
                File.WriteAllText("C:\\Users\\Usuario\\Downloads\\CadastroBaseClientes\\CadastroBaseClientes\\BancoDados\\bancodados.txt", string.Empty);


                foreach (var cliente in listaClientes)
                {
                    Salvar(cliente);
                }
                return true;
            }
            return false;

        }

        public Clientes GetCliente(string Documento)
        {
            var clienteLista = Listar();
            var item = clienteLista.Where(t => t.Documento == Documento).FirstOrDefault();

            return item;
        }
    }
}
