using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementUI.Models
{
    public class BorrowHistory
    {
        [Key]
        public int BorrowHistoryId { get; set; }

        //Foreign keys
        public int BookId { get; set; }

        //Navigation properties
        public Book book { get; set; }

        //Foreign keys
        public int CustomerId { get; set; }

        //Navigation properties
        public Customer customer { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; } 
    }
}