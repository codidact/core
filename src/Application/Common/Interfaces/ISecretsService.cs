using System.Threading.Tasks;

namespace Codidact.Application.Common.Interfaces
{
    /// <summary>
    /// Provides secret info
    /// </summary>
    public interface ISecretsService
    {
        public Task<string> Get(string key);
    }
}
