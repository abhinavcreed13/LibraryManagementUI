﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementUICore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        //Navigation properties
        public ICollection<BorrowHistory> BorrowHistories { get; set; }
    }
}
