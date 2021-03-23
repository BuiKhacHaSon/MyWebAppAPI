using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using OrbintSoft.Yauaa.Analyzer;

namespace MyWebApp.Controllers
{
    [Route("mes")]
    public class MyWebAppController : Controller
    {
        public string RequestIP
        {
            get
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }

        }
        public string Agent
        {
            get
            {
                return HttpContext.Request.Headers["User-Agent"].ToString();
            }
        }

        public string Platform
        {
            get
            {
                #pragma warning disable 618
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var ua = YauaaSingleton.Analyzer.Parse(userAgent);
                return ua.GetValue(UserAgent.OPERATING_SYSTEM_NAME_VERSION);
                #pragma warning restore 618
            }
        }
        public string Browser
        {
            get
            {   
                #pragma warning disable 618
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var ua = YauaaSingleton.Analyzer.Parse(userAgent);
                return ua.GetValue(UserAgent.AGENT_NAME_VERSION);
                #pragma warning restore 618
            }
        }

        public string Device
        {
            get
            {
                #pragma warning disable 618
                var userAgent = HttpContext.Request.Headers["User-Agent"];
                var ua = YauaaSingleton.Analyzer.Parse(userAgent);
                return ua.GetValue(UserAgent.OPERATING_SYSTEM_CLASS);
                #pragma warning restore 618
            }
        }
        protected MessageService messageService;
        public MyWebAppController(MessageService messageService)
        {
            this.messageService = messageService;
        }
        [Route("mes")]
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
                message.Agent = Agent + "/ ip = " + message.Agent;
                message.Platform = Platform;
                message.Browser = Browser;
                message.Device = Device;
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