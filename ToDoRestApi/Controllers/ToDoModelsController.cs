using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListDomain.Entities;
using ToDoListDomain.RepositoryInterface;
using ToDoRestAPI.Infrastructure.Repositories;
using ToDoRestAPI.Infrastructure.ToDoContext;

namespace ToDoRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoModelsController : Controller
    {

        private readonly IToDoRepository _repository;

        public ToDoModelsController(IToDoRepository repository)
        {
            _repository = repository;
        }

        // GET: ToDoModels
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await _repository.GetAllItems();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // GET: ToDoModels/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> Details(int id)
        {
            var toDo = await _repository.GetById(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }


        [HttpPost]
        public async Task<ActionResult<ToDoModel>> CreateToDo(ToDoModel addItem)
        {
            var toAdd = _repository.CreateToDo(addItem);
            return await toAdd;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoModel>> DeleteToDo(int? id)
        {

            var result = await _repository.DeleteToDo(id);

            if (result == null)
            {
                return NotFound($"ToDo with Id = {id} not found.");
            }
            else
            {
                return Ok($"ToDo with Id = {id} deleted.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<ToDoModel>> UpdateToDo(ToDoModel toDo)
        {

            var toDoToUpdate = await _repository.UpdateToDo(toDo);

            if (toDoToUpdate == null)
            {
                return NotFound($"ToDo with Id = {toDo.Id} not found");
            }
            else
            {
                return Ok($"ToDo with Id = {toDo.Id} updated.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoModel>> UpdateStatusToDo(int id, bool isCompleted)
        {

            var toDoToUpdate = await _repository.UpdateStatusToDo(id, isCompleted);

            if (toDoToUpdate == null)
            {
                return NotFound($"ToDo with Id = {id} not found");
            }
            else
            {
                return Ok($"Status ToDo with Id = {id} updated to {isCompleted}.");
            }
        }
    }
}
