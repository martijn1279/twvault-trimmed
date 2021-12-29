using System;

namespace TW.Vault.Scaffold
{
    public partial class TranslationParameter
    {
        public short Id { get; set; }
        public String Name { get; set; }
        public short KeyId { get; set; }

        public TranslationKey Key { get; set; }
    }
}
