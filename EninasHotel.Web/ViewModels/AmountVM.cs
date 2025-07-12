using System.ComponentModel;

namespace EninasHotel.Web.ViewModels
{
    public class AmountVM
    {
        public Guid Id { get; set; }
        [DisplayName("Cash In")]
        public decimal CashIn { get; set; } = 0.0M;
        [DisplayName("Cash Out")]
        public decimal CashOut { get; set; } = 0.0M;
        public decimal Charge { get; set; } = 0.0M;
        public decimal Commission { get; set; } = 0.0M;
        [DisplayName("Transaction Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [DisplayName("Transaction Type")]
        public int TransactionType { get; set; }
        public string? Notes { get; set; }
    }
}
