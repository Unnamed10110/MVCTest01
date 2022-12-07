using Microsoft.AspNetCore.Mvc;
using MVCTest01.Models;
using System.Diagnostics;
using MVCTest01.ViewModels;
using System.IO;


namespace MVCTest01.Controllers
{
    public class HomeController : Controller
    {
        private IAmigoAlmacen amigoAlmacen;
        private readonly IWebHostEnvironment _env;

        public HomeController(IAmigoAlmacen AmigoAlmacen, IWebHostEnvironment env)
        {
            amigoAlmacen = AmigoAlmacen;
            _env = env;

        }


        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ViewResult Index()
        {
            var modelo = amigoAlmacen.DameTodosLosAMigos();
            //return View("~/MyViews/Index.cshtml");
            return View(modelo);
        }

        [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)// el campo id puede ser null
        {
            //Amigo amigo = amigoAlmacen.dameDatosAmigo(2);

            //ViewData["Cabecera"] = "LISTA AMIGOS";
            //ViewData["Amigo"] = amigo;

            //ViewBag.Titulo = "LISTA AMIGOS ViewBag";
            //ViewBag.Amigo = amigo;

            //throw new Exception("Forzar error---------------------------");

            DetallesView detalles = new DetallesView();
            detalles.amigo = amigoAlmacen.dameDatosAmigo(id ?? 1);// si id es null toma por defecto  el valor 1

            detalles.Titulo = "Lista de amigos ViewModel";
            detalles.SubTitulo = "+----------------------------------------+";

            if (detalles.amigo == null)
            {
                Response.StatusCode = 404;
                return View("AmigoNoEncontrado", id);
            }

            return View(detalles);
        }

        [Route("Home/Create")]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CrearAmigoModelo a)
        {
            if (ModelState.IsValid)
            {
                string guidImagen = null;
                if (a.Foto != null)
                {
                    string ficherosImagenes = Path.Combine(_env.WebRootPath, "images");
                    guidImagen = Guid.NewGuid().ToString() + a.Foto.FileName;
                    string rutaDefinitiva = Path.Combine(ficherosImagenes, guidImagen);
                    a.Foto.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    
                }

                Amigo nuevoAmigo = new Amigo();
                nuevoAmigo.Nombre = a.Nombre;
                nuevoAmigo.Email = a.Email;
                nuevoAmigo.Ciudad = a.Ciudad;
                nuevoAmigo.rutaFoto = guidImagen;
                //nuevoAmigo.rutaFoto = "++";
                amigoAlmacen.nuevo(nuevoAmigo);
                return RedirectToAction("details", new { id = nuevoAmigo.Id });
            }
            return View();

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Amigo amigo = amigoAlmacen.dameDatosAmigo(id);
            EditarAmigoModelo amigoEditar = new EditarAmigoModelo
            {
                Id = amigo.Id,
                Nombre = amigo.Nombre,
                Email = amigo.Email,
                Ciudad = amigo.Ciudad,
                rutaFotoExistente = amigo.rutaFoto
            };
            return View(amigoEditar);

        }

        [HttpPost]
        public IActionResult Edit(EditarAmigoModelo model)
        {
            //Comprobamos que los dat is son correctos
            if (ModelState.IsValid)
            {
                // Obtenemos los datos de nuestro amigo de la BBDD
                Amigo amigo = amigoAlmacen.dameDatosAmigo(model.Id);
                // Actualizamos los datos de nuestro objeto del modelo
                amigo.Nombre = model.Nombre;
                amigo.Email = model.Email;
                amigo.Ciudad = model.Ciudad;
                if (model.Foto != null)
                {
                    //si el usuario sube una foto.Debe borrarse la anterior
                    if (model.rutaFotoExistente != null)
                    {
                        string ruta = Path.Combine(_env.WebRootPath, "images", model.rutaFotoExistente);
                        System.IO.File.Delete(ruta);
                        //Guardamos la foto en wwwroot/images
                    }
                    amigo.rutaFoto = SubirImagen(model);
                }
                Amigo amigoModificado = amigoAlmacen.modificar(amigo);
                return RedirectToAction("index");
            }
            return View(model);
        }

        private string SubirImagen(EditarAmigoModelo model)
        {
            string nombreFichero = null;
            if (model.Foto != null){
                string carpetaSubida = Path.Combine(_env.WebRootPath, "images");
                nombreFichero = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                string ruta = Path.Combine(carpetaSubida, nombreFichero);
                using (var fileStream = new FileStream(ruta, FileMode.Create))
                {
                    model.Foto.CopyTo(fileStream);
                }
                    
            }
            return nombreFichero;
        }

        public IActionResult borrar(int id)
        {
            Amigo amigo = amigoAlmacen.borrar(id);
            return RedirectToAction("index");

        }
    }
}