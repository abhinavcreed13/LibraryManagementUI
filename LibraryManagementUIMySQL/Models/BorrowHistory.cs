using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementUIMySQL.Models
{
    public class BorrowHistory
    {
        [Key]
        public int BorrowHistoryId { get; set; }

        //Foreign keys
        public int BookId { get; set; }

        //Book title
        public string Title { get; set; }

        //Foreign keys
        public int CustomerId { get; set; }

        //Customer Name
        public string Name { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}