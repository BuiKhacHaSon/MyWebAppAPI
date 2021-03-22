using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(60)]
        public string Name { get; set; }
        [Required]
        [MaxLength(666)]
        public string BodyMessage { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string Agent { get; set; }
        public string Platform { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
        public Message() { }

    }
}