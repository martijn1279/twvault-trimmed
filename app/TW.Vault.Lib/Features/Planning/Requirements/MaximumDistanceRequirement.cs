using TW.Vault.Model;
using TW.Vault.Model.JSON;

namespace TW.Vault.Features.Planning.Requirements
{
    public class MaximumDistanceRequirement : ICommandRequirements
    {
        public float MaximumDistance { get; set; }

        public bool MeetsRequirement(decimal worldSpeed, decimal travelSpeed, Coordinate source, Coordinate target, Army army)
        {
            return source.DistanceTo(target) <= MaximumDistance;
        }
    }
}
