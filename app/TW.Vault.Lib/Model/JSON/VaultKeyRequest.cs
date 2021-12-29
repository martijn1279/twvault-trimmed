using System;

namespace TW.Vault.Model.JSON
{
    public class VaultKeyRequest
    {
        public long? PlayerId { get; set; }
        public String PlayerName { get; set; }
        public bool NewUserIsAdmin { get; set; }
    }
}
