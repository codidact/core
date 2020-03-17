using System.Threading.Tasks;

namespace Codidact.Core.Application.Common.Interfaces
{
    /// <summary>
    /// Provides secret info
    /// </summary>
    public interface ISecretsService
    {
        Task<string> Get(string key);
    }
}
