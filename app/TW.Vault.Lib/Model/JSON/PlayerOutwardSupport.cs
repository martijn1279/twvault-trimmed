using System.Collections.Generic;

namespace TW.Vault.Model.JSON
{
    public class PlayerOutwardSupport
    {
        public long SourceVillageId { get; set; }
        public List<SupportedVillage> SupportedVillages { get; set; }


        public class SupportedVillage
        {
            public long Id { get; set; }
            public Army TroopCounts { get; set; }
        }
    }
}
