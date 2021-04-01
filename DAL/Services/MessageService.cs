using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;

namespace DAL.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<IEnumerable<Message>> GetNewMessagesAsync();
        Task CreateMessageAsync(Message message);
        Task DeleteAsync(int id);
        Task CheckReadAsync(int id);

    }
    public class MessageService : IMessageService
    {
        protected DatabaseContext context;
        
        public MessageService(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            try
            {
            return context.Messages.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Message>> GetNewMessagesAsync()
        {
            return context.Messages.Where(m => m.IsRead == false).ToList<Message>();
        }
        public async Task CreateMessageAsync(Message message)
        {
            try
            {
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
            }

        }

        public async Task DeleteAsync(int id)
        {
            Message message = context.Messages.FirstOrDefault(m => m.Id == id);
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
        }
        public async Task CheckReadAsync(int id)
        {
            Message message = context.Messages.FirstOrDefault(m => m.Id == id);
            message.IsRead = true;
            context.Messages.Update(message);
            await context.SaveChangesAsync();
        }
    }
}