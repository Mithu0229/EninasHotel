using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Domain.Entities
{
    public class Amount
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
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Char? Status { get; set; }
    }

    public class AmountListViewModel
    {
        public List<Amount> Amounts { get; set; }
        public decimal TotalCashIn { get; set; }
        public decimal TotalCashOut { get; set; }
        public decimal TotalCharge { get; set; }
        public decimal TotalCommission { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
