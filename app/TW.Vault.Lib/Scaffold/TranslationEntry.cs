﻿namespace TW.Vault.Scaffold
{
    public partial class TranslationEntry
    {
        public short TranslationId { get; set; }
        public short KeyId { get; set; }
        public string Value { get; set; }

        public TranslationRegistry Translation { get; set; }
        public TranslationKey Key { get; set; }
    }
}
