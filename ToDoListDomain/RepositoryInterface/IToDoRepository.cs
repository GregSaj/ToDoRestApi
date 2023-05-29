using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListDomain.Entities;

namespace ToDoListDomain.RepositoryInterface
{
    public interface IToDoRepository
    {
        Task<List<ToDoModel>> GetAllItems();
        Task<ToDoModel> GetById(int id);
        Task<ToDoModel> CreateToDo(ToDoModel addItem);
        Task<ToDoModel> DeleteToDo(int? id);
        Task<ToDoModel> UpdateToDo(ToDoModel toDo);
        Task<ToDoModel> UpdateStatusToDo(int id, bool isCompleted);
    }
}
