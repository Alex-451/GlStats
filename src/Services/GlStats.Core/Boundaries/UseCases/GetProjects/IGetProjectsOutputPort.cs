using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetProjects;

public interface IGetProjectsOutputPort
{
    void NoConnection();
    void Default(IEnumerable<Project> projects);
    void InvalidConfig();
}