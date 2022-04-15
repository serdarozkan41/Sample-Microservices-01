using System.ComponentModel.DataAnnotations;

namespace SM01.Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset? UpdatedDateTime { get; set; }
    }
}
