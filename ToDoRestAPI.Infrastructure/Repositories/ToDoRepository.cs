using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListDomain.Entities;
using ToDoListDomain.RepositoryInterface;
using ToDoRestAPI.Infrastructure.ToDoContext;

namespace ToDoRestAPI.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly TodoContext _context;
        

        public ToDoRepository(TodoContext context)
        {
            _context = context;
            
        }

        public Task<List<ToDoModel>> GetAllItems()
        {
            return _context.TodoItems.ToListAsync();
        }

        public Task<ToDoModel> GetById(int id)
        {
            if (id == null || _context.TodoItems == null)
            {
                return null;
            }

            var toDoModel = _context.TodoItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoModel == null)
            {
                return null;
            }

            return toDoModel;
        }

        public async Task<ToDoModel> CreateToDo(ToDoModel addItem)
        {
            var toAdd = await _context.TodoItems.AddAsync(addItem);
            await _context.SaveChangesAsync();
            return toAdd.Entity;
        }

        public async Task<ToDoModel> DeleteToDo(int? id)
        {
            var result = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

            if (result == null)
            {
                return null;
            }
            else
            {
                _context.TodoItems.Remove(result);
                _context.SaveChanges();
                return result;
            }
        }

        public async Task<ToDoModel> UpdateToDo(ToDoModel toDo)
        {

            var toDoToUpdate = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == toDo.Id);

            if (toDoToUpdate == null)
            {
                return null;
            }
            else
            {
                _context.TodoItems.Update(toDo);

                await _context.SaveChangesAsync();

                return toDoToUpdate;
            }
        }

        public async Task<ToDoModel> UpdateStatusToDo(int id, bool isCompleted)
        {

            var toDoToUpdate = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id);

            if (toDoToUpdate == null)
            {
                return null;
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
