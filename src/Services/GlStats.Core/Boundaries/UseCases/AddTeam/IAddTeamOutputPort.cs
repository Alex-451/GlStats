using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.AddTeam;

public interface IAddTeamOutputPort
{
    void NoDatabaseConnection();
    void Default(Team team);
}