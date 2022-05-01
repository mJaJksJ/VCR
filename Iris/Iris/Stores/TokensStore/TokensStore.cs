using System.Collections.Concurrent;

namespace Iris.Stores.TokensStore
{
    /// <inheritdoc cref="ITokensStore"/>
    public class TokensStore : ITokensStore
    {
        private readonly ConcurrentDictionary<string, string> _tokens = new();

        /// <inheritdoc/>
        public void AddOrUpdate(string userId, string token)
        {
            _tokens.AddOrUpdate(userId, token, (_, old) => token);
        }

        /// <inheritdoc/>
        public bool Remove(string userId)
        {
            return _tokens.TryRemove(userId, out var _);
        }

        /// <inheritdoc/>
        public bool Exists(string token)
        {
            return _tokens.Any(t => t.Value == token);
        }
    }
}
