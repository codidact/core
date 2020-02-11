namespace Codidact.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Retreives the member id of the current user.
        /// </summary>
        public long GetMemberId();

        /// <summary>
        /// Retreives the user id of the current user.
        /// </summary>
        public string GetUserId();
    }
}
