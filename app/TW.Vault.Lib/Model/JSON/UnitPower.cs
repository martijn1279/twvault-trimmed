namespace TW.Vault.Model.JSON
{
    public class UnitPower
    {
        public int Infantry { get; set; }
        public int Cavalry { get; set; }
        public int Archer { get; set; }

        public int Total => Infantry + Cavalry + Archer;
    }
}
