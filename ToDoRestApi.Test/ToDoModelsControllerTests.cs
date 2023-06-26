using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Moq;
using System.Net;
using ToDoListDomain.Entities;
using ToDoListDomain.RepositoryInterface;
using ToDoRestApi.Controllers;


namespace ToDoRestApi.Test
{
    public class ToDoModelsControllerTests
    {
        public async Task GetAllItems_ReturnManyItems()
        {
            // arrange
            var repository = new Mock<IToDoRepository>();
            repository.Setup(x => x.GetAllItems()).ReturnsAsync(GetTodoModelData);
            var toDoController = new ToDoModelsController(repository.Object);

            // act
            var getTodoResult = (OkObjectResult)await toDoController.GetAllItems();

            /// Assert
            getTodoResult.StatusCode.Should().Be(200);

        }

        private List<ToDoModel> GetTodoModelData()
        {
            List<ToDoModel> toDoModels = new List<ToDoModel>();
            {
                new ToDoModel
                {
                    Id = 1,
                    Name = "DoFirstThing"
                };
                new ToDoModel
                {
                    Id = 1,
                    Name = "DoNextThing"
                };
                new ToDoModel
                {
                    Id = 1,
                    Name = "DoSecondThing"
                };
            }

            return toDoModels;

        }
    }
}