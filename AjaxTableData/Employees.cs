using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjaxTableData
{
    [Table("Employees")]
    public class Employees
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        public string Name { get; set; }
        
        public double Salary { get; set; }

        [Required] 
        [Column("CreateDate")]      
        public DateTime CreateDate { get; set; }

        public bool Status { set; get; }
    }
}
