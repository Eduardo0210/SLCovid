using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SLCovid.Clases;
using SLCovid.Models;
using Newtonsoft.Json;

namespace SLCovid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable tb = (new Clases.FnConxBD()).ObtenerTabla("Exec PC_GETPRACTICE 0 " );
            SLCovid.Models.Practices practice = new SLCovid.Models.Practices();
            if (tb != null)
            {
                if (tb.Rows.Count != 0)
                {
                    practice.nombre = tb.Rows[0][0].ToString();
                    practice.odsRelacionado = tb.Rows[0][1].ToString();
                    practice.anexo = tb.Rows[0][2].ToString();
                    practice.reacciones = int.Parse(tb.Rows[0][3].ToString());
                    practice.meEncanta = int.Parse(tb.Rows[0][4].ToString());
                    practice.meAsombra = int.Parse(tb.Rows[0][5].ToString());
                    practice.listasPracticas = JsonConvert.DeserializeObject<List<PracticaListas>>(tb.Rows[0][6].ToString());
                };
            }
            return View("Main",practice);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PracticeView(int kiidPractica)
        {
            DataTable tb = (new Clases.FnConxBD()).ObtenerTabla("Exec PC_GETPRACTICE "+kiidPractica);
            SLCovid.Models.Practices practice = new SLCovid.Models.Practices();
            if (tb != null)
            {
                if (tb.Rows.Count != 0) {
                    practice.nombre = tb.Rows[0][0].ToString();
                    practice.odsRelacionado = tb.Rows[0][1].ToString();
                    practice.anexo = tb.Rows[0][2].ToString();
                    practice.reacciones = int.Parse(tb.Rows[0][3].ToString());
                    practice.meEncanta = int.Parse(tb.Rows[0][4].ToString());
                    practice.meAsombra = int.Parse(tb.Rows[0][5].ToString());
                    practice.listasPracticas = JsonConvert.DeserializeObject<List<PracticaListas>>(tb.Rows[0][6].ToString());
                 
                };
                }                                    
            return View(practice);
        }

        [HttpPost]
        public string LikeButton(int kiidPractica,int reaccion) {
            DataTable tb = new DataTable();
            string result = "";
            if (Session["Practica" + kiidPractica + "-" + reaccion] == null)
            {
                 tb = (new Clases.FnConxBD()).ObtenerTabla("Exec PA_ADD_LIKE " + kiidPractica + ","+reaccion );
                result = tb.Rows[0][1].ToString();
                Session["Practica" + kiidPractica+"-"+reaccion] = "1";
            }
            return result;
        }

        [HttpPost]
        public string SearchPractice(string categoria, string sector, string anio, string estado) {
            DataTable tb = (new Clases.FnConxBD()).ObtenerTabla(string.Format("Exec PC_SEARCH_PRACTICES {0},'{1}','{2}','{3}'",categoria,sector,anio,estado));
            List<PracticaListas> practicas = new List<PracticaListas>();
            foreach (DataRow row in tb.Rows) {
                practicas.Add(new PracticaListas()
                {
                    kiidPractica = int.Parse(row[0].ToString()),
                    nombre = row["nombre"].ToString(),
                    kiidCategoria = int.Parse(row["kiidCategoria"].ToString()),
                    institucion = row["institucion"].ToString(),
                    estado = row["estado"].ToString(),
                    tipo = row["tipo"].ToString(),
                    consiste = row["consiste"].ToString(),
                    objetivo = row["objetivo"].ToString(),
                    importante = row["importante"].ToString(),
                    linkFinal = row["linkFinal"].ToString(),
                    odsRelacionado = row["odsRelacionado"].ToString(),
                    anexo = row["anexo"].ToString(),
                    reacciones = int.Parse(row["reacciones"].ToString())
                });            
            }
            return JsonConvert.SerializeObject(practicas);
        }
    }
}