using Newtonsoft.Json;
using System.Collections.Generic;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class ChamadosApiClient : BaseClient
    {
        private const string TOKEN_AUTENTICACAO = "AEEFC184-9F62-4B3E-BB93-BE42BF0FFA36";

        private const string CHAMADOS_LIST_URL = "api/Chamados/Listar";
        private const string CHAMADOS_SOLICITANTES_LIST_URL = "api/Chamados/ListarSolicitantes";
        private const string CHAMADOS_OBTER_URL = "api/Chamados/Obter";
        private const string CHAMADOS_GRAVAR_URL = "api/Chamados/Gravar";
        private const string CHAMADOS_EXCLUIR_URL = "api/Chamados/Excluir";

        private const string DESAFIO_API_URL = "https://localhost:44388/"; // Endereço API IIS-Express

        public ChamadosApiClient() : base()
        {
            //TODO
        }

        public List<ChamadoViewModel> ChamadosListar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = default(Dictionary<string, object>); // Não há parâmetros para essa chamada

            var response = base.Get($"{DESAFIO_API_URL}{CHAMADOS_LIST_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<ChamadoViewModel>>(json);
        }

        public List<string> SolicitantesListar(string termo)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = new Dictionary<string, object>()
            {
                { "termo", termo }
            };

            var response = base.Get($"{DESAFIO_API_URL}{CHAMADOS_SOLICITANTES_LIST_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public ChamadoViewModel ChamadoObter(int idChamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idChamado", idChamado }
            };

            var response = base.Get($"{DESAFIO_API_URL}{CHAMADOS_OBTER_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<ChamadoViewModel>(json);
        }

        public bool ChamadoGravar(ChamadoViewModel chamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var response = base.Post($"{DESAFIO_API_URL}{CHAMADOS_GRAVAR_URL}", chamado, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool ChamadoExcluir(int idChamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idChamado", idChamado }
            };

            var response = base.Delete($"{DESAFIO_API_URL}{CHAMADOS_EXCLUIR_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

    }
}
