namespace GlStats.Core.Boundaries.UseCases.DeleteTeam;

public interface IDeleteTeamOutputPort
{
    void NoDatabaseConnection();
    void Default(bool deleted);
}