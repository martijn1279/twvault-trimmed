using System;

namespace TW.Vault.Model.JSON
{
    public class User
    {
        public String Key { get; set; }
        public String PlayerName { get; set; }
        public String TribeName { get; set; }
        public long PlayerId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
