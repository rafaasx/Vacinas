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
    public class RegiaoController : ApiController
    {
        private Vacinas.Models.Context db = new Vacinas.Models.Context();

        [ActionName("DefaultAction")]
        public IQueryable<RegiaoDTO> GetRegiaos(int pageSize = 10)
        {
            var model = db.Regiao.AsQueryable();

            return model.Select(RegiaoDTO.Select).Take(pageSize);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(RegiaoDTO))]
        public async Task<IHttpActionResult> GetRegiao(int id)
        {
            var model = await db.Regiao.Select(RegiaoDTO.Select).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [ActionName("DefaultAction")]
        public async Task<IHttpActionResult> PutRegiao(int id, Regiao model)
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
                if (!RegiaoExists(id))
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
        [ResponseType(typeof(RegiaoDTO))]
        public async Task<IHttpActionResult> PostRegiao(Regiao model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regiao.Add(model);
            await db.SaveChangesAsync();
            var ret = await db.Regiao.Select(RegiaoDTO.Select).FirstOrDefaultAsync(x => x.Id == model.Id);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(RegiaoDTO))]
        public async Task<IHttpActionResult> DeleteRegiao(int id)
        {
            Regiao model = await db.Regiao.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Regiao.Remove(model);
            await db.SaveChangesAsync();
            var ret = await db.Regiao.Select(RegiaoDTO.Select).FirstOrDefaultAsync(x => x.Id == model.Id);
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

        private bool RegiaoExists(int id)
        {
            return db.Regiao.Count(e => e.Id == id) > 0;
        }
    }
}