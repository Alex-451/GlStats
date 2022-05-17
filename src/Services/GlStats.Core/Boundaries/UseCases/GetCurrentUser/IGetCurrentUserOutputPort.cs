using GlStats.Core.Entities;

namespace GlStats.Core.Boundaries.UseCases.GetCurrentUser;

public interface IGetCurrentUserOutputPort
{
    void NoConnection();
    void Default(CurrentUser currentUser);
    void InvalidConfig();
}