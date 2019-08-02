using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.CORE.Entities
{
    public partial class Rol
    {
        public int RolId { get; set; }

        [Required]
        [StringLength(50)]
        public string RolAdi { get; set; }

        [StringLength(50)]
        public string Sifre { get; set; }

    }
}
