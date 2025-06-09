using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models.DTOS
{
    public class ReviewCreateDTO
    {
        public int AppintmentId { get; set; }
        public int RatingValue { get; set; }
        public string? ReviewComment { get; set; }
    }
}
