using DispenstOfMoneyAutomatic_Basic.Application.Enums;
using DispenstOfMoneyAutomatic_Basic.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispenstOfMoneyAutomatic_Basic.Application.Services
{
    public class ATMService
    {
        public Dictionary<int, int>? Dispense(int amount, DispenseMode mode)
        {
            List<int> denominations = ModeRepository.Instatnce.Factory(mode);

            var result = new Dictionary<int, int>();
            int remaining = amount;

            foreach (var bill in denominations.OrderByDescending(x => x))
            {
                int count = remaining / bill;
                if (count > 0)
                {
                    result[bill] = count;
                    remaining -= count * bill;
                }
            }

            return remaining == 0 ? result : null;
        }

        public List<string> CalculateBills(int amount, DispenseMode mode)
        {
            var result = new List<string>();
            int[] denominations = mode switch
            {
                DispenseMode.Only100And500 => new[] { 500, 100 },
                DispenseMode.Only200And1000 => new[] { 1000, 200 },
                _ => new[] { 1000, 500, 200, 100 }
            };

            var copy = amount;
            foreach (var bill in denominations)
            {
                int count = copy / bill;
                if (count > 0)
                {
                    result.Add($"{count} papeleta(s) de {bill}");
                    copy -= count * bill;
                }
            }

            return copy == 0 ? result : new List<string>();
        }
    }

}
