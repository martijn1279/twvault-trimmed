using System.Collections.Generic;

namespace TW.Vault.Scaffold
{
    public partial class TranslationLanguage
    {
        public short Id { get; set; }
        public string Name { get; set; }

        public ICollection<TranslationRegistry> Translations { get; set; }
    }
}
