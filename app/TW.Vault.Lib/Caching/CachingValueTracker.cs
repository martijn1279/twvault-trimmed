using System;

namespace TW.Vault.Caching
{
    public class CachingTimeTracker
    {
        DateTime populatedAt;

        public TimeSpan? MaxAge { get; set; }

        public bool IsExpired => MaxAge != null && DateTime.UtcNow - this.populatedAt > MaxAge.Value;

        public void MarkUpdated()
        {
            populatedAt = DateTime.UtcNow;
        }
    }
}
