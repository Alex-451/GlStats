namespace GlStats.Core.Boundaries.UseCases.UpdateTeam;

public interface IUpdateTeamOutputPort
{
    void NoDatabaseConnection();
    void Default(bool updated);
}