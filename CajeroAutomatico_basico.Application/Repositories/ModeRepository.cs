
using DispenstOfMoneyAutomatic_Basic.Application.Enums;
using DispenstOfMoneyAutomatic_Basic.Application.ViewModels;

namespace DispenstOfMoneyAutomatic_Basic.Application.Repositories
{
    public sealed class ModeRepository
    {
        private ModeRepository()
        {

        }

        public static ModeRepository Instatnce { get; } = new();

        public List<int> Factory(DispenseMode mode)
        {
            List<int> denominations = mode switch
            {
                DispenseMode.Only200And1000 => new List<int> { 1000, 200 },
                DispenseMode.Only100And500 => new List<int> { 500, 100 },
                _ => new List<int> { 1000, 500, 200, 100 }
            };

            return denominations;
        }
    }
}
