using System.ComponentModel.DataAnnotations;

namespace TelefonRehberiNuevo.CORE
{
    public partial class Kisiler
    {
        [Key]
        public int KisiId { get; set; }

        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(50)]
        public string Soyadi { get; set; }

        [StringLength(14)]
        public string Telefon { get; set; }

        public int DepartmanID { get; set; }

        public int Yoneticisi { get; set; }

        public int RolID { get; set; }

        public bool Aktif { get; set; }
    }
}
