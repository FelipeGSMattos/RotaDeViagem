using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RotaDeViagem.API.ViewModels;
using RotaDeViagem.Application.Interface;
using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ControllerBase
    {
        private readonly IRotaAppService _rotaApp;
        private readonly IMapper _mapper;
        public RotaController(IRotaAppService rotaApp, IMapper mapper)
        {
            _rotaApp = rotaApp;
            _mapper = mapper;
        }

        // GET: api/<RotaController>
        [HttpGet]
        public async Task<IEnumerable<RotaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<RotaViewModel>>(await _rotaApp.GetAllAsync());
        }

        // GET api/<RotaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotaViewModel>> Get(Guid id)
        {
            var rota = _mapper.Map<RotaViewModel>(await _rotaApp.GetByIdAsync(id));

            if (rota == null)
            {
                return NotFound();
            }

            return rota;
        }

        // POST api/<RotaController>
        [HttpPost]
        public async Task<ActionResult<RotaViewModel>> AddRota(RotaViewModel rotaViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _rotaApp.Add(_mapper.Map<Rota>(rotaViewModel));

            return Ok(new
            {
                success = true,
                data = rotaViewModel
            });
        }

        // PUT api/<RotaController>/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<RotaViewModel>> Put(Guid id, RotaViewModel rotaViewModel)
        {
            if(id != rotaViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return BadRequest();

            await _rotaApp.Update(_mapper.Map<Rota>(rotaViewModel));

            return Ok(new
            {
                success = true,
                data = rotaViewModel
            });
        }

        // DELETE api/<RotaController>/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var rotaViewModel = _mapper.Map<RotaViewModel>(await _rotaApp.GetByIdAsync(id));

            if (rotaViewModel == null) return NotFound();

            await _rotaApp.Remove(id);

            return Ok("A Rota foi removida");
            
        }

        //protected ActionResult CustomResponse(object result = null)
        //{
        //    if (OperacaoValida())
        //    {
        //        return Ok(new
        //        {
        //            success = true,
        //            data = result
        //        });
        //    }

        //    return BadRequest(new
        //    {
        //        success = false,
        //        errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
        //    });
        //}
    }
}
