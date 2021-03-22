using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Calculation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(60)]
        public string NameA { get; set; }
        [MaxLength(16)]
        public string DoBA { get; set; }
        [MaxLength(160)]
        public string FavoriteA { get; set; }
        public string NameB { get; set; }
        [MaxLength(16)]
        public string DoBB { get; set; }
        [MaxLength(160)]
        public string FavoriteB { get; set; }
        public string Agent { get; set; }
        public bool IsCalculated { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public Calculation(){}
    }
}