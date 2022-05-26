using GlStats.Core.Entities;

namespace GlStats.ApiWrapper.Entities.Requests;

public class ProjectQueryOptions
{ 
    public ProjectQueryOptions(ProjectSearchOptions options)
    {
        Before = options.Before;
        After = options.After;
        Search = options.Search;
    }

    public string Before { get; set; } = string.Empty;
    public string After { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;
}