using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Classes
{
    public class Contato
    {
        public string nome { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string email { get; set; }
        
        // ENDERÇO
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }

    }
}
