using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.UTILITIES.SessionOperations
{
    public class SessionContext
    {
        public int KisiId { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string Telefon { get; set; }

        public int DepartmanID { get; set; }

        public int Yoneticisi { get; set; }

        public int RolID { get; set; }

        public bool Aktif { get; set; }

        public string Rol { get; set; }

        public string YoneticiAdi { get; set; }

        public string DepartmanAdi { get; set; }
    }
}
