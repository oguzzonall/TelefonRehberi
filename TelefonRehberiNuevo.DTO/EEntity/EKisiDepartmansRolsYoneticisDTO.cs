using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberiNuevo.DTO.EEntity
{
    public class EKisiDepartmansRolsYoneticisDTO
    {
        public EKisilerDTO eKisilerDto { get; set; }
        public List<EDepartmanDTO> EDepartmanDtos { get; set; }
        public List<ERolDTO> ERolDtos { get; set; }
        public List<EKisilerDTO> EYoneticisDtos { get; set; }
    }
}
