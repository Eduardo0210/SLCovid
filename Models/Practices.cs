using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLCovid.Models
{
    //This class represents the Json Master Object and contains all his attributes
    public class Practices
    {
        public string nombre { get; set; }
        public string odsRelacionado { get; set; }
        public string anexo { get; set; }
        public int reacciones { get; set; }
        public int meEncanta { get; set; }
        public int meAsombra { get; set; }
        public List<PracticaListas> listasPracticas { get; set; }
        public int kiidPractica { get; set; }
    }


    public class PracticaListas
    { 
        public int kiidPractica { get; set; }
        public string nombre { get; set; }
        public int kiidCategoria { get; set; }
        public string institucion { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string consiste { get; set; }
        public string objetivo { get; set; }
        public string importante { get; set; }
        public string linkFinal { get; set; }
        public string odsRelacionado { get; set; }
        public string anexo { get; set; }
        public int reacciones { get; set; }
    
    }
}