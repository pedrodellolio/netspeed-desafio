using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WebApp_Desafio_FrontEnd.ViewModels
{
    [DataContract]
    public class ChamadoViewModel
    {
        private static readonly CultureInfo ptBR = new CultureInfo("pt-BR");

        [Display(Name = "ID")]
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Assunto")]
        [DataMember(Name = "Assunto")]
        [Required(ErrorMessage = "O assunto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O assunto deve ter no máximo 100 caracteres.")]
        public string Assunto { get; set; }

        [Display(Name = "Solicitante")]
        [DataMember(Name = "Solicitante")]
        [Required(ErrorMessage = "O solicitante é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do solicitante deve ter no máximo 100 caracteres.")]
        public string Solicitante { get; set; }

        [Display(Name = "IdDepartamento")]
        [DataMember(Name = "IdDepartamento")]
        [Required(ErrorMessage = "Selecione um departamento.")]
        public int IdDepartamento { get; set; }

        [Display(Name = "Departamento")]
        [DataMember(Name = "Departamento")]
        public string Departamento { get; set; }

        [Display(Name = "DataAbertura")]
        [DataMember(Name = "DataAbertura")]
        [Required(ErrorMessage = "A data de abertura é obrigatória.")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/(20[0-9]{2})$", ErrorMessage = "Data inválida.")]
        public DateTime DataAbertura { get; set; }

        [DataMember(Name = "DataAberturaWrapper")]
        public string DataAberturaWrapper
        {
            get
            {
                return DataAbertura.ToString("d", ptBR);
            }
            set
            {
                DataAbertura = DateTime.Parse(value, ptBR);
            }
        }
    }
}
