using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetTeams;

public interface IGetTeamsOutputPort
{
    void NoDatabaseConnection();
    void Default(IEnumerable<Team> teams);
}