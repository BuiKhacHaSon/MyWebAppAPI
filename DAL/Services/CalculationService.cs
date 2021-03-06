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
            if(nameA == "B??i Kh???c H?? S??n" || nameB == "B??i Kh???c H?? S??n")
            return -1;
            try
            {
                int a = nameA.Length * nameB.Length / 16;
                int b = GetNumber(dobA) + GetNumber(dobB) / 12;
                int c = favA.Length * favB.Length / 16;
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
            if(result == -1)
            {
                return "C??m ??n ???? quan t??m, nh??ng m??nh ch??? th??ch m??i b???n hoy <3";
            }

            if (result == 100)
                return "WOW! Xin ch??c m???ng, hai ng?????i qu?? l?? h???p lu??n <3";
            else if (result == 0)
                return "Chia bu???n c??ng b???n. T.T";
            else if (result > 0 && result <= 25)
                return "C??ng kh?? b???p b??nh ?????y nh??? :(";
            else if (result > 25 && result <= 50)
                return "C??n c?? th??? v???t v??t :|";
            else if (result > 50 && result <= 75)
                return "M???c trung b??nh c?? th??? ch???p nh???n ???????c :)";
            else if (result > 75 && result < 100)
                return "Th???t tuy???t v???i ????ng kh??ng ^^";
                else return "Kh??ng bi???t n??i g??!";
        }
        private int GetNumber(string date)
        {
            try 
            {
                int result = Int32.Parse(date.Trim().Replace("/", string.Empty).Replace(".",string.Empty).Replace("-", string.Empty));
                return result;            
            }
            catch
            {
                return 611;
            }
        }
    }
}