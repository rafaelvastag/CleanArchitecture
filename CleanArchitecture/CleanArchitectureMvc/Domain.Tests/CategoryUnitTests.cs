using Xunit;
using FluentAssertions;
using System;
using Domain.Entities;
using Domain.Validation;

namespace Domain.Tests
{
    public class CategoryUnitTests
    {
        [Fact(DisplayName = "Create Category with valid state")]
        [Trait("Package", "Category")]
        public void CreateCategory_WithValidParameters_ResultObjectWithValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with negative Id value")]
        [Trait("Package", "Category")]
        public void CreateCategory_WithNegativeIdValue_DomainExceptionValidation()
        {
            int id = -1;
            Action action = () => new Category(id, "Category Name");
            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage($"Invalid Id value: {id}");
        }

        [Fact(DisplayName = "Create Category with less than 3 characteres Name value")]
        [Trait("Package", "Category")]
        public void CreateCategory_WithShorterNameValue_ResultObjectWithValidState()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Invalid Name size, minimun length is 3 characters");
        }

        [Fact(DisplayName = "Create Category with empty Name value")]
        [Trait("Package", "Category")]
        public void CreateCategory_MissingNameValue_ResultObjectWithValidState()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Invalid Name is required");
        }

        [Fact(DisplayName = "Create Category with null Name value")]
        [Trait("Package", "Category")]
        public void CreateCategory_WithNullNameValue_ResultObjectWithValidState()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>().WithMessage("Invalid Name is required");
        }
    }
}
