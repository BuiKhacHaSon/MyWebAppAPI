using System;
using DAL.Models;
using DAL.Services;
using MyWebApp.Controllers;
using Xunit;
using Moq;
using System.Collections.Generic;
using DAL.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var context = new Mock<IMessageService>();
            context.Setup(aa => aa.GetMessagesAsync()).Returns(GetMessages());

            var controller = new MyWebAppController(context.Object);
            var listTest = controller.GetAllMessage();
            var test11 = (OkObjectResult)listTest.Result; 
            var test222 = (List<Message>)test11.Value;   
            Assert.True(listTest.IsCompletedSuccessfully == true);
            Assert.True(test222.Count == 2);
        }

        private async Task<IEnumerable<Message>> GetMessages()
        {
            List<Message> listMessage = new List<Message>(){
                new Message(){
                     Id = 1,
                Name = "Son Dep Trai",
                BodyMessage = "Ok it works ^^",
                CreatedAt = DateTime.UtcNow
                },
                new Message(){
                     Id = 2,
                Name = "Son Dep Trai 2",
                BodyMessage = "Ok it works ^^ 2",
                CreatedAt = DateTime.UtcNow
                }
            };
            return listMessage;
        }
    }
}
