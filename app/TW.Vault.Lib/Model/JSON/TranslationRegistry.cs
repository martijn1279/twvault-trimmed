﻿using System;
using System.Collections.Generic;

namespace TW.Vault.Model.JSON
{
    public class TranslationRegistry
    {
        public short Id { get; set; }
        public short LanguageId { get; set; }
        public String Name { get; set; }
        public String Language { get; set; }
        public Dictionary<String, String> Entries { get; set; }
    }
}
