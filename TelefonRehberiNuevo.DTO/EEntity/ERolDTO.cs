using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.DTO.EEntity
{
    public class ERolDTO
    {
        public int RolId { get; set; }

        [Required]
        [DisplayName("Rol Adı")]
        [StringLength(50)]
        public string RolAdi { get; set; }

        [StringLength(50)]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
    }
}
