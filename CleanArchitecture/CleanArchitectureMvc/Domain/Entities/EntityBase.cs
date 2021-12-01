using System;

namespace Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }

        DateTime CreatedDate { get; set; }

        DateTime UpdateDate { get; set; }

        string CreateUser { get; set; }

        string UpdateUser { get; set; }
    }
}
