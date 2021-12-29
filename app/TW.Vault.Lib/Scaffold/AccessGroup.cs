﻿using System;
using System.Collections.Generic;

namespace TW.Vault.Scaffold
{
    public partial class AccessGroup
    {
        public int Id { get; set; }
        public String Label { get; set; }
        public int WorldId { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
