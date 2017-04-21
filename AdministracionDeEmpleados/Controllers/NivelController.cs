using AdmonEmpleadosModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdministracionDeEmpleados.Controllers
{
    public class NivelController : Controller
    {
        //IRepository Repository = new AdmonEmpleadosModel.Repository();
        IRepositoryUoW Repository = new AdmonEmpleadosModel.RepositoryUoW();
        // GET: Nivel
        public ActionResult Index()
        {
            var Modelo = Repository.FindEntitySet<Nivel>(c => true);
            return View(Modelo.ToList());

            //IRepository Repository = new Modelo.Repository();
            //var Categories = Repository.FindEntitySet<Category>(c => true);
            //var Category1 = Repository.Create(new Category
            //{
            //    CategoryName = "CatDemoA"
            //});
            //var Customer1 = Repository.Create(new Customer
            //{
            //    CustomerID = "CA",
            //    CompanyName = "CNABC"
            //});
            //Console.Write("Presiona <enter> para finalizar, cambios:" + Changes);
            //Console.ReadLine();


            /*
             public class Repository : EFRepository.Repository, IDisposable, IRepository
                {
                    public Repository(
                        bool autoDetectChangesEnabled = false,
                        bool proxyCreationEnabled = false)
                        :base(new AdmonEmpleadosEntities(), autoDetectChangesEnabled, proxyCreationEnabled)
                    { }
                }
              */



        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "numero, precioHora")]Nivel nivel)
        {
            //
            try
            {
                if (ModelState.IsValid)
                {
                    /* Repository.Create(new Nivel {
                         numero = 1,
                         precioHora = 250
                     });*/

                    Repository.Create(nivel);
                    Repository.Save();
                    
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "No es posible guardar los cambios. Intente de nuevo y si el problema persiste contacte con el administradr del sistema.");

            }
            return View(nivel);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Eliminación fallida";
            }
            Nivel nivel = Repository.FindEntity<Nivel>(c => c.id == id);
            if(nivel == null)
            {
                return HttpNotFound();
            }
            return View(nivel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Nivel nivel = Repository.FindEntity<Nivel>(c => c.id == id);
                Repository.Delete<Nivel>(nivel);
                Repository.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true});
            }
            // RedirectToAction("Index");
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Nivel nivel = Repository.FindEntity<Nivel>(c => c.id == id);
            if (nivel == null)
            {
                return HttpNotFound();
            }
            return View(nivel);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, numero, precioHora")] Nivel nivel)//parámetro????????????????????
        {
            try
            {
                if (ModelState.IsValid) { 
                    //Nivel nivel = Repository.FindEntity<Nivel>(c => c.id == id);
                    Repository.Update<Nivel>(nivel);
                    Repository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al guardar los cambios");
                //return RedirectToAction("Edit", new { id = id, saveChangesError = true });
            }


            return View(nivel);
        }
    }
}