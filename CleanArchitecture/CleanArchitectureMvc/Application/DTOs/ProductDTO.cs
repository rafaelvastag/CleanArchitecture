using Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(1,9999)]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; set; }

        //Navigation props
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ProductDTO()
        {}

        public ProductDTO(int id)
        {
            Id = id;
        }

        public ProductDTO(int id, string name, string description, decimal price, int stock, string image, int categoryId, Category category) : this(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
            Category = category;
        }
    }
}
