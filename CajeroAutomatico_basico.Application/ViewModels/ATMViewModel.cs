using DispenstOfMoneyAutomatic_Basic.Application.Enums;

namespace DispenstOfMoneyAutomatic_Basic.Application.ViewModels
{
    public class ATMViewModel
    {
        public int? AmountToWithdraw { get; set; }
        public DispenseMode CurrentMode { get; set; } = DispenseMode.Efficient;
        public Dictionary<int, int>? BillsDispensed { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
