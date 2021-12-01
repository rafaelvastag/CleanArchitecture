using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Product : EntityBase
    {

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }

        public string Image { get; private set; }

         //Navigation props
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            ValidateId(id);
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid Name size, minimun length is 3 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid Description is required");
            DomainExceptionValidation.When(description.Length > 250, "Invalid image name size, maximum length is 250 characters");
            DomainExceptionValidation.When(image.Length < 3, "Invalid Name size, minimun length is 3 characters");
            DomainExceptionValidation.When(price < 0, "Invalid price value");
            DomainExceptionValidation.When(stock < 0, "Invalid price value");
            AtributeDomainValues(name, description, price, stock, image);
        }

        private void AtributeDomainValues(string name, string description, decimal price, int stock, string image)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
        private void ValidateId(int id)
        {
            DomainExceptionValidation.When(id <= 0, $"Invalid Id value: {id}");
            Id = Id;
        }
    }
}
