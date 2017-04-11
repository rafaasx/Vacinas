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
    public class PacoteController : ApiController
    {
        private Vacinas.Models.Context db = new Vacinas.Models.Context();


        [HttpPost]
        [ActionName("CalcularTotais")]
        [ResponseType(typeof(PacoteDTO))]
        public IHttpActionResult PostCalcularTotais(PacoteDTO model)
        {
            model.TotalCriancas = model.QuantidadeCriancas * 4;
            model.TotalAdultos = model.QuantidadeAdultos * 3;
            model.TotalIdosos = model.QuantidadeIdosos * 2;
            model.TotalGeral = model.TotalCriancas + model.TotalAdultos + model.TotalIdosos;
            return Ok(model);
        }

        [ActionName("DefaultAction")]
        public IQueryable<PacoteDTO> GetPacotes(int pageSize = 10)
        {
            var model = db.Pacote.AsQueryable();
            return model.Select(PacoteDTO.SELECT).Take(pageSize);
        }

        [ActionName("DefaultAction")]
        [ResponseType(typeof(PacoteDTO))]
        public async Task<IHttpActionResult> GetPacote(int id)
        {
            var model = await db.Pacote.Select(PacoteDTO.SELECT).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPut]
        [ActionName("DefaultAction")]
        public async Task<IHttpActionResult> PutPacote(int id, Pacote model)
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
                if (!PacoteExists(id))
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

        [HttpPost]
        [ActionName("DefaultAction")]
        [ResponseType(typeof(PacoteDTO))]
        public async Task<IHttpActionResult> PostPacote(Pacote model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacote.Add(model);
            await db.SaveChangesAsync();
            var ret = await db.Pacote.Select(PacoteDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }
        [HttpDelete]
        [ActionName("DefaultAction")]
        [ResponseType(typeof(PacoteDTO))]
        public async Task<IHttpActionResult> DeletePacote(int id)
        {
            Pacote model = await db.Pacote.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Pacote.Remove(model);
            await db.SaveChangesAsync();
            var ret = await db.Pacote.Select(PacoteDTO.SELECT).FirstOrDefaultAsync(x => x.Id == model.Id);
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

        private bool PacoteExists(int id)
        {
            return db.Pacote.Count(e => e.Id == id) > 0;
        }
    }
}