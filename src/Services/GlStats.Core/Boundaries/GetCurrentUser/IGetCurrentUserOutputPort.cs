using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.GetCurrentUser;

public interface IGetCurrentUserOutputPort
{
    void NoConnection();
    void Default(CurrentUser currentUser);
    void InvalidConfig();
}