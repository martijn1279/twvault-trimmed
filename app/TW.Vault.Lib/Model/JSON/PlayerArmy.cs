using System.Collections.Generic;

namespace TW.Vault.Model.JSON
{
    public class PlayerArmy
    {
        public int? PossibleNobles { get; set; }
        public List<VillageArmySet> TroopData { get; set; }
    }
}
