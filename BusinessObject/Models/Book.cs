using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Book
    {
        [Required]
        [MaxLength(12,ErrorMessage = "Max length = 12"), MinLength(6, ErrorMessage = "Min length = 6")]
        public string BookId { get; set; }
        public string BookName { get; set; }
        public int? Quantity { get; set; }
        public string AuthorName { get; set; }
        public string PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
