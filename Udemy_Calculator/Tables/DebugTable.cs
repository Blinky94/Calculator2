using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Udemy_Calculator
{
    [Table("debug")]
    public class LogDebugTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public string DetailDate { get; set; }

        [Column("debug_text")]
        public string DetailText { get; set; }

        [Column("category")]
        public int DetailCategory { get; set; }

        [ForeignKey(typeof(FormulaTable))]
        [Column("formula_id")]
        public int Formula_Id { get; set; }
    }
}
