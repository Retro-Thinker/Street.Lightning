using System.ComponentModel.DataAnnotations;

namespace Street.Lightning.Domain.Common;

public abstract class BaseEntity
{
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}