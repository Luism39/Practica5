using Practica5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica5.Controllers
{
    public class LectorController : Controller
    {
        // GET: Lector
        public ActionResult Index()
        {
            return View(new List<Data>());

        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            List<Data> datos = new List<Data>();
            string filePath = string.Empty;
            if(postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string xData = System.IO.File.ReadAllText(filePath);
                foreach(string row in xData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        datos.Add(new Data
                        {
                            Id = Convert.ToInt32(row.Split(',')[0]),
                            Nombre = row.Split(',')[1],
                            Pais = row.Split(',')[2]
                        });
                    }
                }
            }
            return View(datos);
        }
    }
}