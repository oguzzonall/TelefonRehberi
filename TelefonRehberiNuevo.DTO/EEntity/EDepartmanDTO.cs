using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.DTO.EEntity
{
    public class EDepartmanDTO
    {
        public int DepartmanId { get; set; }

        [Required(ErrorMessage = "Departman Adı Boş Geçilemez")]
        [StringLength(100,ErrorMessage = "Departman Uzunluğu En Fazla 100 Karakter olabilir.")]
        [DisplayName("Departman Adı")]
        public string DepartmanAdi { get; set; }
    }
}
