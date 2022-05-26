namespace GlStats.Core.Boundaries.UseCases.GetProjects;

public class GetProjectsInput
{
    public string Before { get; set; } = string.Empty;
    public string After { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;
}