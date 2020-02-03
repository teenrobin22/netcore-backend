 using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController: ControllerBase
     {
          private readonly IActividadRepository _repository;
     public ActividadesController(IActividadRepository repository)
        {

            _repository = repository;
        }

        [HttpGet("GetActividades/{id}")]
        public async Task<IActionResult> GetActividades(int id)
        {
            var activity = await _repository.GetActividad(id);
            return Ok(activity);
        }
       
        [HttpGet("GetActivity/{id}")]
        public async Task<IActionResult> GetActivity(int id)
        {
            var activity = await _repository.GetActivity(id);
            return Ok(activity);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Actividad activityForRegister)
        {
            var createdUser = await _repository.Register(activityForRegister);
            return Ok(createdUser);
        }


        [HttpGet]
        public async Task<IActionResult> Getpaginado([FromQuery]UserParams userParams)
        {  
            var users = await _repository.GetActividadesPaginado(userParams);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpDelete("DeleteActivity/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            
            var actividadBe = await _repository.GetActivity(id);

            if (actividadBe == null)
            { return NotFound(); }


            _repository.Delete(actividadBe);
            

            if (await _repository.SaveAll())
                return Ok();

            return BadRequest("Failed to process the elimination");


        }

        [HttpPut("UpdateActivity")]
        public async Task<IActionResult> UpdateActivity(Actividad activity)
        {
            _repository.Update(activity);

            if (await _repository.SaveAll())
                return NoContent();

            throw new Exception($"Error to process");
        }
    }
}