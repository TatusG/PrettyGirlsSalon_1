using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class DetailedReviewDTO
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Servicio { get; set; }
        public string Estilista { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set; }
        public DateTime FechaValoracion { get; set; }  
        public string Comentario {  get; set; }
    }
}
