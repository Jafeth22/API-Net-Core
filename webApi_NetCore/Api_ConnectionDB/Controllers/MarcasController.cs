using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Api_ConnectionDB.Context;
using Api_ConnectionDB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api_ConnectionDB.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly AppDbContext _context;
        string badMsj;

        public MarcasController(AppDbContext context)
        {
            this._context = context;
            badMsj = "Something wrong happened, check and try it again, please.";
        }

        // GET: api/<MarcasController>
        /// <summary>
        /// Gets tall marcas
        /// </summary>
        /// <returns>The list of all Marcas</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IEnumerable<Marcas> Get()
        {
            return _context.Marcas.ToList();
        }

        // GET api/<MarcasController>/5
        /// <summary>
        /// Gets the information only of the Marca specified
        /// </summary>
        /// <param name="id">The ID of the Marca</param>
        /// <returns>The values of the Marca specified</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public Marcas Get(string id)
        {
            var marca = _context.Marcas.ToList().FirstOrDefault(x => Equals(x.ID.ToString(), id));
            return marca;
        }

        // POST api/<MarcasController>
        /// <summary>
        /// Creates a new Marca
        /// </summary>
        /// <param name="marcas">The values of the new Marca</param>
        /// <returns>The state of the task</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult Post([Microsoft.AspNetCore.Mvc.FromBody] Marcas marcas)
        {
            try
            {
                _context.Marcas.Add(marcas);
                _context.SaveChanges();
                return Ok("Save successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(badMsj);
            }
        }

        // PUT api/<MarcasController>/5
        /// <summary>
        /// Updates the values of the Marca specified
        /// </summary>
        /// <param name="marcas">The values updated of the Marca</param>
        /// <returns>The state of the task</returns>
        [Microsoft.AspNetCore.Mvc.HttpPut]
        public ActionResult Put([Microsoft.AspNetCore.Mvc.FromBody] Marcas marcas)
        {
            if (marcas.ID >0)
            {
                _context.Entry(marcas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Ok("Changes were made successfully");
            }
            else
            {
                return BadRequest(badMsj);
            }
        }

        /*
        //This is another way to use PUT, it means, and UPDATe
        public IHttpActionResult PutMarca(Marcas marca)
        {
            if (!ModelState.IsValid)
            {
                return (IHttpActionResult)BadRequest("This is invalid model. Please check");
            }
            using (var x = new Marcas)
            {
                var checkMarca = x.Marca.Where(y = y.id == marca.ID).FisrtOrDefault<Marcas>();
                if (checkMarca != null)
                {
                    checkMarca.nombre = marca.Nombre;
                    x.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }*/

        // DELETE api/<MarcasController>/5
        /// <summary>
        /// Deteles the Marca specified
        /// </summary>
        /// <param name="id">The ID to Detele the marca</param>
        /// <returns>The state of the task</returns>
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var marca = _context.Marcas.ToList().FirstOrDefault(x => x.ID == id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
                _context.SaveChanges();
                return Ok("Deleted successfully!");
            }
            else
            {
                return BadRequest(badMsj);
            }
        }
    }
}
