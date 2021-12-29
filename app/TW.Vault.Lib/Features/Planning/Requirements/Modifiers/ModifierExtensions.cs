﻿using System.Collections.Generic;
using System.Linq;

namespace TW.Vault.Features.Planning.Requirements.Modifiers
{
    public static class ModifierExtensions
    {
        public static ICommandRequirements LimitTroopType(this ICommandRequirements requirement, IEnumerable<Model.JSON.TroopType> troopTypes) =>
            new LimitTroopTypeModifier(requirement) { AllowedTypes = troopTypes.ToArray() };
    }
}
