using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Udemy_Calculator
{
    [Table("chunk")]
    public class ChunkTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public string ChunkDate { get; set; }

        [ForeignKey(typeof(FormulaTable))]
        [Column("formula_id")]
        public int Formula_Id { get; set; }

        [Column("chunk_text")]
        public string ChunkText { get; set; }

        [Column("result")]
        public string ChunkResult { get; set; }
    }
}
