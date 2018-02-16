using System.ComponentModel.DataAnnotations;

namespace DotNetApiSample.Domain
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "Zip code must be 7 digits")]
        public string ZipCode { get; set; }
    }
}
