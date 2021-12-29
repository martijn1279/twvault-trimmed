using System.Collections.Generic;

namespace TW.Vault.Model.JSON
{
    public class VillageCommandSet
    {
        public List<VillageCommand> CommandsToVillage { get; set; }
        public List<VillageCommand> CommandsFromVillage { get; set; }
    }
}
