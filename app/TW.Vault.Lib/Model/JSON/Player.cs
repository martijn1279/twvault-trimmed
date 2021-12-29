namespace TW.Vault.Model.JSON
{
    public class Player
    {
        public long PlayerId { get; set; }
        public string PlayerName { get; set; }
        public long? TribeId { get; set; }
        public int? Villages { get; set; }
        public int? Points { get; set; }
        public int? PlayerRank { get; set; }
    }
}
