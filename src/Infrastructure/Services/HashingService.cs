using AluguelIdeal.Application.Services;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Services
{
    public class HashingService : IHashingService
    {
        private readonly SHA256Managed hasher;

        public HashingService()
        {
            this.hasher = new SHA256Managed();
        }

        public Task<string> HashAsync(string password, CancellationToken cancellationToken = default)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = hasher.ComputeHash(passwordBytes);
            foreach (byte x in hash)
                stringBuilder.AppendFormat("{0:x2}", x);

            return Task.FromResult(stringBuilder.ToString().ToUpper());
        }
    }
}