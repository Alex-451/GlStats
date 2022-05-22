using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.SearchUsers;

public interface ISearchUsersOutputPort
{
    void NoConnection();
    void Default(IEnumerable<User> users);
    void InvalidConfig();
}