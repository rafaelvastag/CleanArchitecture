using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        public CategoryDTO()
        {}

        public CategoryDTO(int id)
        {
            Id = id;
        }
    }
}
