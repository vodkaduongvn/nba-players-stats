using System.ComponentModel.DataAnnotations;

namespace NBA.Players.Charts.PlayerDbContext.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }

    public class BaseObject : BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public string? CreatedById { get; set; }
        public string? CreatedByUserName { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedById { get; set; }
        public string? ModifiedByUserName { get; set; }
        public bool IsActive { get; set; } = true;

        public BaseObject()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
