using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using ToDoListDomain.Entities;
using ToDoListDomain.RepositoryInterface;
using ToDoRestApi.Controllers;

namespace ToDoRestApi.Test
{
    public class ToDoModelsControllerTests
    {
        [Fact]
        public async void GetAllItems_ReturnManyItems()
        {
            var toDoItems = new List<ToDoModel>()
            { new ToDoModel { Id = 1, Name = "DoFirstThing"},
            new ToDoModel { Id = 2, Name = "DoSecondThing"},
            new ToDoModel { Id = 3, Name = "DoNextThing"}
            };
            var mockData = new Mock<IToDoRepository>();
            mockData.Setup(e => e.GetAllItems()).ReturnsAsync(toDoItems);

            //arrange


            //act
            var result = new ToDoModelsController(mockData.Object);

            //assert
            Assert.Equal(2, await result.GetAllItems().Result);

        }
        
    }
}