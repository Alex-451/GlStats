namespace GlStats.Core.Boundaries.UseCases.AddTeam;

public interface IAddTeamOutputPort
{
    void NoDatabaseConnection();
    void Default(int teamId);
}