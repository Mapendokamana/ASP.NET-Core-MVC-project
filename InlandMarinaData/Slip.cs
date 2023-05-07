using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class Slip
    {
        [Display(Name = "Slip ID")]
        public int ID { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        [Display(Name = "Dock Number")]
        public int DockID { get; set; }

        // navigation properties
        public virtual Dock Dock { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
    }
}
