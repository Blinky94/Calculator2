using SQLite;

namespace Udemy_Calculator
{
    [Table("formula")]
    public class FormulaTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public string FormulaDate { get; set; }

        [Column("formula_text")]
        public string FormulaText { get; set; }

        [Column("result")]
        public string FormulaResult { get; set; }

    }
}
