namespace GlStats.Core.Boundaries.UseCases.RemoveTeamMember;

public interface IRemoveTeamMemberOutputPort
{
    void NoDatabaseConnection();
    void Default(bool removed);
}