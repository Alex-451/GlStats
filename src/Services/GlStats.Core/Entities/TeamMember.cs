namespace GlStats.Core.Entities;

public class TeamMember
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string MemberId { get; set; } = string.Empty;
}