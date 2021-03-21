using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("mes")]
    public class MyWebAppController : Controller
    {
        protected MessageService messageService;
        public MyWebAppController(MessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessage()
        {
            var result = await messageService.GetMessagesAsync();
            return new OkObjectResult(result);
        }

        [Route("new")]
        [HttpGet]
        public async Task<IActionResult> GetAllNewMessage()
        {
            IEnumerable<Message> result = await messageService.GetNewMessagesAsync();
            return new OkObjectResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            try
            {
                message.CreatedAt = DateTime.UtcNow;
                await messageService.CreateMessageAsync(message);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await messageService.DeleteAsync(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }

        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> CheckRead(int id)
        {
            try
            {
                await messageService.CheckReadAsync(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
        }


    }
}