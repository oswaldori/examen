using ExamenWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ExamenWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerDatos(DateTime fechaReporte, DateTime fechaSiniestro)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Examen"].ConnectionString))
            {

                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Consulta";
                cmd.Parameters.Add("@svfhreporte", SqlDbType.DateTime).Value = fechaReporte;
                cmd.Parameters.Add("@svfhsiniestro", SqlDbType.DateTime).Value = fechaSiniestro;
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                List<MDatos> objetoListaMDatos = new List<MDatos>();
                foreach (DataRow Dr in dt.Rows)
                {
                    MDatos ListaMDatos = new MDatos();
                    ListaMDatos.nrreporte = Convert.ToInt32(Dr["nrreporte"]);
                    ListaMDatos.svindice = Convert.ToInt32(Dr["svindice"]);
                    ListaMDatos.svfhreporte = Convert.ToDateTime(Dr["svfhreporte"]);
                    ListaMDatos.clnombre = Convert.ToString(Dr["clnombre"]);
                    ListaMDatos.clestatus = Convert.ToString(Dr["clestatus"]);
                    ListaMDatos.prrfc = Convert.ToString(Dr["prrfc"]);
                    ListaMDatos.sunombre = Convert.ToString(Dr["sunombre"]);
                    ListaMDatos.susupervisoria = Convert.ToString(Dr["susupervisoria"]);

                    objetoListaMDatos.Add(ListaMDatos);
                }
                return Json(objetoListaMDatos, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}