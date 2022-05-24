using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetUsersById;

public interface IGetUsersByIdsOutputPort
{
    void NoConnection();
    void Default(IEnumerable<User> users);
    void InvalidConfig();
}