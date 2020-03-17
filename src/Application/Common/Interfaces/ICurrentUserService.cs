namespace Codidact.Core.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Retreives the member id of the current user.
        /// </summary>
        long GetMemberId();

        /// <summary>
        /// Retreives the user id of the current user.
        /// </summary>
        string GetUserId();
    }
}
