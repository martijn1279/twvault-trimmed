using System;

namespace TW.Vault.Model.JSON
{
    public class UserLog
    {
        public String AdminUserName { get; set; }
        public String EventDescription { get; set; }
        public DateTime? OccurredAt { get; set; }
    }
}
