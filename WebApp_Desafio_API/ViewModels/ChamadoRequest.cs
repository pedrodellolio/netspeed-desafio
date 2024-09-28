using System;
using System.ComponentModel.DataAnnotations;
using WebApp_Desafio_API.Validators;

namespace WebApp_Desafio_API.ViewModels
{
    /// <summary>
    /// Solicitação da chamada
    /// </summary>
    public class ChamadoRequest
    {
        /// <summary>
        /// ID do Chamado
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Assunto do Chamado
        /// </summary>
        [Required(ErrorMessage = "O assunto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O assunto deve ter no máximo 100 caracteres.")]
        public string assunto { get; set; }

        /// <summary>
        /// Solicitante do Chamado
        /// </summary>
        [Required(ErrorMessage = "O solicitante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do solicitante deve ter no máximo 100 caracteres.")]
        public string solicitante { get; set; }

        /// <summary>
        /// ID do Departamento do Chamado
        /// </summary>
        [Required(ErrorMessage = "Selecione um departamento.")]
        public int idDepartamento { get; set; }

        /// <summary>
        /// Data de Abertura do Chamado
        /// </summary>
        [Required(ErrorMessage = "A data de abertura é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida")]
        [DataNaoRetroativa]
        public DateTime dataAbertura { get; set; }
    }
}
