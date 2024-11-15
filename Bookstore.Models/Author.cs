using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Author Name")]
        public string Name { get; set; }

        
    }
}
