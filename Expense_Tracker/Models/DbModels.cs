using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Expense_Category
    {
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string? CategoryName { get; set; }
    }
    public class Expenses
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [ForeignKey("Expense_Category")]
        public int Id { get; set; }

        public virtual Expense_Category? Expense_Category { get; set; }
    }
}
