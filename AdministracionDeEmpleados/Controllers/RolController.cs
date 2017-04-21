using AdmonEmpleadosModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdministracionDeEmpleados.Controllers
{
    public class RolController : Controller
    {
        IRepositoryUoW Repository = new AdmonEmpleadosModel.RepositoryUoW();

        // GET: Rol
        public ActionResult Index()
        {
            var roles = Repository.FindEntitySet<Rol>(c => true);
            return View(roles.ToList());
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "nombre")]Rol rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Create(rol);
                    Repository.Save();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                ModelState.AddModelError("", "No es posible guardar");
            }
            return View(rol);
        }

        //GET: Delete
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Eliminación fallida";
            }
            var rol = Repository.FindEntity<Rol>(c => c.id == id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                Rol rol = Repository.FindEntity<Rol>(c => c.id == id);
                Repository.Delete<Rol>(rol);
                Repository.Save();
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        //GET: Edit
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rol = Repository.FindEntity<Rol>(c => c.id == id);
            if(rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, nombre")] Rol rol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Update(rol);
                    Repository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                ModelState.AddModelError("", "No es posible guardar");
            }
            return View();
        }
    }
}