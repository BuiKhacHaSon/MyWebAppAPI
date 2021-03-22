using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;

namespace DAL.Services
{
    public class CalculationService
    {
        protected DatabaseContext context;

        public CalculationService(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Calculation>> GetCalculationsAsync()
        {
            try
            {
                return context.Calculations.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Calculation>> GetNewCalculationsAsync()
        {
            return context.Calculations.Where(m => m.IsRead == false).ToList<Calculation>();
        }
        public async Task CreateCalculationAsync(Calculation calculation)
        {
            try
            {
                await context.Calculations.AddAsync(calculation);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }

        }

        public async Task DeleteAsync(int id)
        {
            Calculation calculation = context.Calculations.FirstOrDefault(m => m.Id == id);
            context.Calculations.Remove(calculation);
            await context.SaveChangesAsync();
        }
        public async Task CheckReadAsync(int id)
        {
            Calculation calculation = context.Calculations.FirstOrDefault(m => m.Id == id);
            calculation.IsRead = true;
            context.Calculations.Update(calculation);
            await context.SaveChangesAsync();
        }

        public async Task<Calculation> Calculate(Calculation calculation)
        {

            calculation.Result = CalculateResult(calculation.NameA, calculation.NameB, calculation.DoBA, calculation.DoBB, calculation.FavoriteA, calculation.FavoriteB);
            calculation.Message = GenerateMessage(calculation.Result);
            return calculation;
        }

        private int CalculateResult(string nameA, string nameB, string dobA, string dobB, string favA, string favB)
        {
            int result = 0;
            try
            {
                int a = (nameA.Length / nameB.Length) * 100;
                int b = (dobA.Length / dobB.Length) * 100;
                int c = (favA.Length / favB.Length) * 100;
                result = (a + b + c) / 3;
                result = result * 1601;
                result = result % 101;
                return result;
            }
            catch
            {
                return 0;
            }
        }
        private string GenerateMessage(int result)
        {
            if (result == 100)
                return "WOW! Xin chúc mừng, hai người quá là hợp luôn <3";
            else if (result == 0)
                return "Chia buồn cùng bạn. T.T";
            else if (result > 0 && result <= 25)
                return "Cũng khá bấp bênh đấy nhỉ :(";
            else if (result > 25 && result <= 50)
                return "Còn có thể vớt vát :|";
            else if (result > 50 && result <= 75)
                return "Mức trung bình có thể chấp nhận được :)";
            else if (result > 75 && result < 100)
                return "Thật tuyệt với đúng không ^^";
                else return "Không biết nói gì!";
        }
    }
}