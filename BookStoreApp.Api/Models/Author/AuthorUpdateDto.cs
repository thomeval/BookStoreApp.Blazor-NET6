using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Data
{
    public partial class AuthorUpdateDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Bio { get; set; }
    }
}
