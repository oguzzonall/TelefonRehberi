using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.DTO.EEntity
{
    public class EKisiDetayDTO
    {
        public int KisiId { get; set; }

        [StringLength(50)]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [StringLength(50)]
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }

        [StringLength(14)]
        public string Telefon { get; set; }

        public int DepartmanID { get; set; }

        public int Yoneticisi { get; set; }

        public int RolID { get; set; }

        public bool Aktif { get; set; }

        public string Rol { get; set; }

        [DisplayName("Yönetici Adı")]
        public string YoneticiAdi { get; set; }

        [DisplayName("Departman Adı")]
        public string DepartmanAdi { get; set; }
    }
}
