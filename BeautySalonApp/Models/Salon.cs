using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("salons")]
    public class Salon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("salon_name")]
        [MaxLength(100)]
        public required string SalonName { get; set; }

        [Required]
        [Column("address_id")]
        public int AddressId { get; set; }

        [Required]
        [Column("phone")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public required string Phone { get; set; }
    }
}
