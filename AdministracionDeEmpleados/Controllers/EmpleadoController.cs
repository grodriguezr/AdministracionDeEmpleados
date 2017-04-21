using AdmonEmpleadosModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace AdministracionDeEmpleados.Controllers
{
    public class EmpleadoController : Controller
    {
        IRepositoryUoW Repository = new AdmonEmpleadosModel.RepositoryUoW();
        private AdmonEmpleadosEntities db = new AdmonEmpleadosEntities();
        // GET: Empleado
        public ActionResult Index()
        {
            //var Modelo = Repository.FindEntitySet<Empleado>("Nivel");  
            //string[] arreglo = { "Nivel", "Rol" };
            var Modelo = Repository.FindEntitySet<Empleado>();





            return View(Modelo.ToList());
        }

        public ActionResult Create()
        {
            llenarListaNiveles();
            llenarListaRoles();//lista de roles
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "primerNombre, segundoNombre, primerApellido, segundoApellido, edad, telefono, idNivel, idRol")]Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Create(empleado);
                    Repository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                ModelState.AddModelError("", "No es posible guardar los cambios");
            }
            llenarListaNiveles(empleado.idNivel);
            llenarListaRoles(empleado.idRol);//lista de roles
            return View(empleado);
        }

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
            //Empleado empleado = Repository.FindEntity<Empleado>(c => c.id == id);
            //Empleado empleado = db.Empleadoes.Find(id);
            Empleado empleado = Repository.FindEntity<Empleado>(c => c.id == id, "Nivel");//mostrar
            
            if(empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Empleado empleado = Repository.FindEntity<Empleado>(c => c.id == id);//borrar
                Repository.Delete(empleado);
                Repository.Save();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = Repository.FindEntity<Empleado>(c => c.id == id);
            if(empleado == null)
            {
                return HttpNotFound();
            }
            llenarListaNiveles(empleado.idNivel);
            llenarListaRoles(empleado.idRol);//lista de roles
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, primerNombre, segundoNombre, primerApellido, segundoApellido, edad, telefono, idNivel, idRol")] Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Update(empleado);
                    Repository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
                ModelState.AddModelError("", "Error al guardar los cambios");
            }
            llenarListaNiveles(empleado.idNivel);
            llenarListaRoles(empleado.idRol);//lista de roles
            return View(empleado);
        }

        public void llenarListaNiveles(object nivelSeleccionado = null)
        {
            ViewBag.idNivel = new SelectList(Repository.FindEntitySet<Nivel>(n => true).OrderBy(n => n.numero), "id", "numero", nivelSeleccionado);
            Debug.Write("entra en llenarlista");
        }

        public void llenarListaRoles(object rolSeleccionado = null)
        {
            ViewBag.idRol = new SelectList(Repository.FindEntitySet<Rol>(r => true).OrderBy(n => n.nombre), "id", "nombre", rolSeleccionado);
            Debug.Write("entra en llenarlista");
        }
    }
}
