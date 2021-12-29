using System.ComponentModel.DataAnnotations;

namespace TW.Vault.Model.JSON
{
    public class VillageArmySet
    {
        [Required]
        public long? VillageId { get; set; }

        [Required]
        public Army Stationed { get; set; }
        [Required]
        public Army Traveling { get; set; }
        [Required]
        public Army Supporting { get; set; }
        [Required]
        public Army AtHome { get; set; }

        public bool IsEmpty => Stationed != null || Traveling != null || Supporting != null || AtHome != null;
    }
}
