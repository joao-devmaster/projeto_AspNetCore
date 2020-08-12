using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_PLAYERS_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace E_PLAYERS_AspNetCore.Controllers
{
    public class NoticiasController : Controller
    {
        /// <summary>
        /// exibe Index da View
        /// </summary>
        /// <returns>View</returns>
        Noticias noticiasModel = new Noticias();
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiasModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Publica as informações inseridas
        /// </summary>
        /// <param name="form">informações</param>
        /// <returns>retorna para mesma pagina</returns>
        public IActionResult Publicar(IFormCollection form)
        {
            Noticias noticias = new Noticias();
            noticias.IdNoticia = Int32.Parse( form["IdNoticia"]);
            noticias.Titulo   = form["Titulo"];
            noticias.Texto    = form["Texto"];
            // Inicio Upload Imagem
            
            var file    = form.Files[0];

            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }


                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias", folder, file.FileName);
                // Cadastra a imagem
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                noticias.Imagem   = file.FileName;
            }
            else
            {
                noticias.Imagem   = "padrao.png";
            }
            // Fim Upload Imagem

            noticiasModel.Create(noticias);
            return LocalRedirect("~/Noticias");
        }

        [Route("Noticias/{id}")]
        public IActionResult Excluir(int id)
        {
            noticiasModel.Delete(id);
            return LocalRedirect("~/Noticias");

        }

    }
}