using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetTeamById;

public interface IGetTeamByIdOutputPort
{
    void NoDatabaseConnection();
    void Default(Team team);
}