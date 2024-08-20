using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SueldoSemanal
{
    [Table("resultado")]
    public class Resultado
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }


        [Column("horasT")]
        public string HorasTra { get; set; }

        [Column("pagoH")]
        public string PagoHo { get; set; }

        [Column("sueldo")]
        public string Sueldo { get; set; }
    }
}
