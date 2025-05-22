using System.ComponentModel.DataAnnotations;

namespace OnlineOrderManagement.Application
{

    public record CustomerCreateDto(
        [Required]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name cannot contain digits.")]
        string Name,
        [Required]
        [EmailAddress]
        string Email,
        [Required]
        string Address,
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid Egyptian mobile number.")]
        string Phone
    );

    public record CustomerUpdateDto(
        [Required]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name cannot contain digits.")]
        string Name,
        [Required]
        [EmailAddress]
        string Email,
        [Required]
        string Address,
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid Egyptian mobile number.")]
        string Phone
    );


    public record CustomerDto(
        int Id,
        string Name,
        string Email,
        string Address,
        string Phone
    );

    public record ProductCreateDto(
       [Required]
       string Name,
       [Required]
       string Description,
       [Required]
       [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive.")]
       decimal Price,
       [Required]
       [Range(1, int.MaxValue, ErrorMessage = "StockQuantity must be at least 1.")]
        int StockQuantity,
       [RegularExpression(@"^[A-Z0-9]{8}$", ErrorMessage = "SerialNumber must be 8 uppercase letters or digits.")]
       string SerialNumber
    );

    public record ProductUpdateDto(
        [Required]
        string Name,

        [Required]
        string Description,

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive.")]
        decimal Price,

        [Range(1, int.MaxValue, ErrorMessage = "StockQuantity must be at least 1.")]
        int StockQuantity,

         [RegularExpression(@"^[A-Z0-9]{8}$", ErrorMessage = "SerialNumber must be 8 uppercase letters or digits.")]
         string SerialNumber
             );
    public record ProductDto(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity,
     string SerialNumber
    );

    public record OrderItemDto(int ProductId, int Quantity);

    public record CreateOrderDto([Required] int CustomerId,
    [Required]
    [MinLength(1, ErrorMessage = "You must add at least one item.")]
    List<OrderItemDto> Items);

    public record OrderItemDetailDto(int ProductId, string ProductName, int Quantity, decimal Subtotal);

    public record OrderDto(int Id, CustomerDto Customer, DateTime OrderDate, string Status, List<OrderItemDetailDto> Items);


}
