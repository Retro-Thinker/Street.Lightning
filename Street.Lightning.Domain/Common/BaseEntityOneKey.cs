using System.ComponentModel.DataAnnotations;

namespace Street.Lightning.Domain.Common;

public class BaseEntityOneKey : BaseEntity
{
    [Key]
    public int Id { get; set; }
}