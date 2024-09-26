using Newtonsoft.Json;
using System.Collections.Generic;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class DepartamentosApiClient : BaseClient
    {
        private const string TOKEN_AUTENTICACAO = "AEEFC184-9F62-4B3E-BB93-BE42BF0FFA36";

        private const string DEPARTAMENTOS_LIST_URL = "api/Departamentos/Listar";
        private const string DEPARTAMENTOS_GRAVAR_URL = "api/Departamentos/Gravar";
        private const string DEPARTAMENTOS_OBTER_URL = "api/Departamentos/Obter";
        private const string DEPARTAMENTOS_EXCLUIR_URL = "api/Departamentos/Excluir";

        private const string DESAFIO_API_URL = "https://localhost:44388/"; // Endereço API IIS-Express

        public DepartamentosApiClient() : base()
        {
            //TODO
        }

        public List<DepartamentoViewModel> DepartamentosListar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = default(Dictionary<string, object>); // Não há parâmetros para essa chamada

            var response = base.Get($"{DESAFIO_API_URL}{DEPARTAMENTOS_LIST_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(json);
        }

        public DepartamentoViewModel DepartamentoObter(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Get($"{DESAFIO_API_URL}{DEPARTAMENTOS_OBTER_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<DepartamentoViewModel>(json);
        }

        public bool DepartamentoGravar(DepartamentoViewModel departamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var response = base.Post($"{DESAFIO_API_URL}{DEPARTAMENTOS_GRAVAR_URL}", departamento, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool DepartamentoExcluir(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", TOKEN_AUTENTICACAO }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Delete($"{DESAFIO_API_URL}{DEPARTAMENTOS_EXCLUIR_URL}", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}
