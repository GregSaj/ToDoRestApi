using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListDomain.Entities;
using ToDoRestApi.Models;

namespace ToDoRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoModelsController : Controller
    {
        private readonly TodoContext _context;

        public ToDoModelsController(TodoContext context)
        {
            _context = context;
        }

        // GET: ToDoModels
        [HttpGet]
        public async Task<ActionResult<List<ToDoModel>>> Index()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: ToDoModels/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> Details(int? id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return toDoModel;
        }


        [HttpPost]
        public async Task<ActionResult<ToDoModel>> CreateToDo(ToDoModel addItem)
        {
            var toAdd = await _context.TodoItems.AddAsync(addItem);
            await _context.SaveChangesAsync();
            return toAdd.Entity;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoModel>> DeleteToDo(int? id)
        {

            var result = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

            if (result == null)
            {
                return NotFound($"ToDo with Id = {id} not found");
            }
            else
            {
                _context.TodoItems.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
        }

        [HttpPut]
        public async Task<ActionResult<ToDoModel>> UpdateToDo(ToDoModel toDo)
        {

            var toDoToUpdate = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == toDo.Id);

            if (toDoToUpdate == null)
            {
                return NotFound($"ToDo with Id = {toDo.Id} not found");
            }
            else
            {               
                _context.TodoItems.Update(toDo);

                await _context.SaveChangesAsync();

                return toDoToUpdate;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoModel>> UpdateStatusToDo(int id, bool isCompleted)
        {

            var toDoToUpdate = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

            if (toDoToUpdate == null)
            {
                return NotFound($"ToDo with Id = {id} not found");
            }
            else
            {
                toDoToUpdate.Status = isCompleted;

                await _context.SaveChangesAsync();

                return toDoToUpdate;
            }
        }
    }
}
