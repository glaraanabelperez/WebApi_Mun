using System.ComponentModel.DataAnnotations;

namespace WebApi_Mun.Controllers
{
    public class ItemBuy
    {
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string Title { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string CurrencyId { get; set; }

        public ItemBuy()
        {
            this.CurrencyId = "ARS";
        }
    }

    public class ClientApi
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string Name;

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string LastName;

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$")]
        public string DirectionName;

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [StringLength(10, ErrorMessage = "El número es demasiado largo")]
        public int DirectionNumber;

        [Required]
        [StringLength(100)]
        public string DirectionLocation;

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [StringLength(5, ErrorMessage = "El número es demasiado largo")]
        public int PhoneArea;

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [StringLength(15, ErrorMessage = "El número es demasiado largo")]
        public int PhoneNumber;

        [Required]
        [EmailAddress]
        public string Email;

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,5}$")]
        public string IdentificationType;

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [StringLength(20, ErrorMessage = "El número es demasiado largo")]
        public int IdentificationNumber;
    }
}