using Newtonsoft.Json;

namespace COOPGO.Models.Repository
{
    public class TransacoesRepository
    {
        private string caminhoArquivo = "C:\\Users\\Gabriel\\Downloads\\bancodados\\listaextrato.txt";

        public void Salvar(Transacao transacao)
        {
            var transacaoTexto = JsonConvert.SerializeObject(transacao) + "," + Environment.NewLine;
            File.AppendAllText(caminhoArquivo, transacaoTexto);
        }

        public List<Transacao> Listar()
        {
            if (!File.Exists(caminhoArquivo))
            {
                File.Create(caminhoArquivo).Close();
                return new List<Transacao>();
            }

            var conteudo = File.ReadAllText(caminhoArquivo);

            if (string.IsNullOrWhiteSpace(conteudo))
            {
                return new List<Transacao>();
            }

            try
            {
                List<Transacao> transacoes = JsonConvert.DeserializeObject<List<Transacao>>("[" + conteudo + "]");
                return transacoes ?? new List<Transacao>();
            }
            catch
            {
                return new List<Transacao>();
            }
        }

        public List<Transacao> ListarPorUsuario(int usuarioId)
        {
            var todasTransacoes = Listar();
            return todasTransacoes.Where(t => t.usuarioId == usuarioId).ToList();
        }

        public decimal ObterSaldoUsuario(int usuarioId)
        {
            var transacoesUsuario = ListarPorUsuario(usuarioId);
            return transacoesUsuario.Sum(t => t.valor);
        }
    }
}
