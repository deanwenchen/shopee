using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("operation_logs")]
public class OperationLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public int AdminId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Action { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Module { get; set; }

    [MaxLength(4000)]
    public string? Details { get; set; }

    [MaxLength(50)]
    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
