using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovoprojetoApi.Entidades;
using NovoprojetoApi.percistencia;

namespace NovoprojetoApi.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class EventeController : ControllerBase
    {
        private readonly DevEventDbContext _context;
        public EventeController(DevEventDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Obter todos os eventos
        /// </summary>
        /// <returns>Coleção de eventos</returns>
        /// <response code = "200"></response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll() 
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }
        /// <summary>
        /// obter um Evento
        /// </summary>
        /// <param name="id">Identificador do Evento</param>
        /// <returns>Dados do evento</returns>
        /// <response code = "200">Sucesso</response>
        /// <responde code = "404">Não encotrado</responde>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents
                .Include(de => de.Palestrante)
                .SingleOrDefault(d => d.Id == id); 
            if (devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }

        /// <summary>
        /// Cadastrar um Evento
        /// </summary>
        /// <remarks>
        ///  Objeto json {"titulo": string, "descricao": string, "dataInicial": 2023-05-04T20:00:57.630Z, "dataFinal": 2023-05-04T20:00:57.630Z}
        /// </remarks>
        /// <param name="devEvent">Dados do Evento</param>
        /// <returns>Objeto recem-criado</returns>
       // <reponse code = "201">Sucesso</reponse>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = devEvent.Id}, devEvent);

        }
        /// <summary>
        ///  Atualiza um evento
        /// </summary>
        /// <remarks>
        ///  Objeto json {"titulo": string, "descricao": string, "dataInicial": 2023-05-04T20:00:57.630Z, "dataFinal": 2023-05-04T20:00:57.630}
        /// </remarks>
        /// <param name="id">Identifacor do evento</param>
        /// <param name="input">Dados do evento</param>
        /// <returns> Nada </returns>
        /// <responde code = "404">Não encontrado</responde>
        /// <responde code = "204">Sucesso</responde>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update(Guid id, DevEvent input)  
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Titulo ,input.Descricao, input.DataInicial, input.DataFinal);
            

            _context.DevEvents.Update(devEvent);  
            _context.SaveChanges();
            return NoContent();
        
        }

        /// <summary>
        /// Deletar um evento
        /// </summary>
        /// <param name="id">Identificador de um Evento</param>
        /// <returns>Nada</returns>
        /// <response code = "404">Não encontardo</response>
        /// <response code = "204">Sucesso</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(Guid id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();
            _context.SaveChanges();
            return NoContent();

        }

        /// <summary>
        /// Cadastrar palestrante
        /// </summary>
        /// <remarks>
        /// objeto Json { "nome": string,  "tituloPalestra": string,"tiuloDescricao": string, "perfilLinkedin": string,}
        /// </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="palestrante">Dados do Palestrante</param>
        /// <returns>Nada</returns>
        /// <resonse code ="404">Evento Nao encontrado</resonse>
        /// <response code = "204">Sucesso</response>
        [HttpPost("{id}/speakers")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult PostPalestrnte(Guid id, DevEventSpeaker palestrante)
        {
            palestrante.DevEventId = id;

            var devEvent = _context.DevEvents.Any(d => d.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }
           _context.DevEventsSpeaker.Add(palestrante);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
