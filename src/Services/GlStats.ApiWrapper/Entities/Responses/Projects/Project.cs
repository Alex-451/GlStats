using GlStats.ApiWrapper.Entities.Api;

namespace GlStats.ApiWrapper.Entities.Responses.Projects;

public class Project
{
    public IEnumerable<ProjectResponse> Nodes { get; set; }

}