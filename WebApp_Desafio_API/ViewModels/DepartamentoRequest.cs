using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Desafio_API.ViewModels
{
    /// <summary>
    /// Solicitação da chamada
    /// </summary>
    public class DepartamentoRequest
    {
        /// <summary>
        /// ID do Departamento
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Descrição do Departamento
        /// </summary>
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public string descricao { get; set; }
    }
}

