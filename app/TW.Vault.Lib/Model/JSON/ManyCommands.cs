using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TW.Vault.Model.JSON
{
    public class ManyCommands
    {
        [Required]
        public bool? IsOwnCommands { get; set; }

        [Required]
        public List<Command> Commands { get; set; }
    }
}
