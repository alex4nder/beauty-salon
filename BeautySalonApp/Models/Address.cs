using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonApp.Models
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("address_line")]
        [MaxLength(100)]
        public required string AddressLine { get; set; }

        [Required]
        [Column("porstal_code")]
        [MaxLength(10)]
        public required string PostalCode { get; set; }

        [Required]
        [Column("city")]
        [MaxLength(50)]
        public required string City { get; set; }

        [Required]
        [Column("state")]
        [MaxLength(50)]
        public required string State { get; set; }
    }

}
