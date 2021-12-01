using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Category : EntityBase
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Category(int id, string name)
        {
            ValidateId(id);
            Validate(name);
        }

        public Category(string name)
        {
            Validate(name);
        }
        private void Validate(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name is required");
            DomainExceptionValidation.When(name.Length < 3, "Invalid Name size, minimun length is 3 characters");
            Name = name;
        }

        private void ValidateId(int id)
        {
            DomainExceptionValidation.When(id <= 0, $"Invalid Id value: {id}");
            Id = Id;
        }

    }
}
