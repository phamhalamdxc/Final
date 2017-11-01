using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Please give the CategoryName")]
        public string CategoryName { get; set; }
    }
}
