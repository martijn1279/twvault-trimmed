using System;

namespace TW.Vault.Model.JSON
{
    public class PlayerInfo
    {
        public String PlayerName { get; set; }
        public long PlayerId { get; set; }
        public String TribeName { get; set; }
        public String TribeTag { get; set; }
        public long? TribeId { get; set; }
    }
}
