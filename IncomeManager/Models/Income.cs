﻿using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Income
    {
        public int Id { get; set; } 
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string DateTime { get; set; } = "";
        public int UserId { get; set; }
        public string Investment { get; set; }
    }
}
