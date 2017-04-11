using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Vacinas.Models;

namespace Vacinas.Controllers
{
    public class EstoqueController : ApiController
    {
        private Vacinas.Models.Context db = new Vacinas.Models.Context();


        [ActionName("DefaultAction")]
        public IQueryable<EstoqueDTO> GetEstoques(int pageSize = 10)
        {
            var model = db.Estoque.AsQueryable();
                        
            return model.Select(EstoqueDTO.SELECT).Take(pageSize);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(EstoqueDTO))]
        public async Task<IHttpActionResult> GetEstoque(int id)
        {
            var model = await db.Estoque.Select(EstoqueDTO.SELECT).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [ActionName("DefaultAction")]
        public async Task<IHttpActionResult> PutEstoque(int id, Estoque model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstoqueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(EstoqueDTO))]
        public async Task<IHttpActionResult> PostEstoque(Estoque model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estoque.Add(model);
            await db.SaveChangesAsync();
            var ret = await db.Estoque.Select(EstoqueDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(EstoqueDTO))]
        public async Task<IHttpActionResult> DeleteEstoque(int id)
        {
            Estoque model = await db.Estoque.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Estoque.Remove(model);
            await db.SaveChangesAsync();
            var ret = await db.Estoque.Select(EstoqueDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return Ok(ret);
        }

        [HttpGet]
        [Route("Api/Estoque/BuscarSaldo/{id}")]
        [ActionName("BuscarSaldo")]
        public IHttpActionResult GetBuscarSaldo(int id)
        {
            var ret = db.Estoque.Sum(x => x.Quantidade) - db.Pacote.Sum(x => x.TotalGeral);
            return Ok(ret);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstoqueExists(int id)
        {
            return db.Estoque.Count(e => e.Id == id) > 0;
        }
    }
}