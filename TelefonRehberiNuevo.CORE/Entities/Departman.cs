using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.CORE.Entities
{
    public partial class Departman
    {
        public int DepartmanId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmanAdi { get; set; }

    }
}
