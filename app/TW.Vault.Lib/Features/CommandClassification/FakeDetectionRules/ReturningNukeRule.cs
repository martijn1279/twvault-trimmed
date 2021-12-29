using System.Linq;

namespace TW.Vault.Features.CommandClassification.FakeDetectionRules
{
    public class ReturningNukeRule : IFakeDetectionRule
    {
        public FakeClassification Classify(ClassificationContext context)
        {
            //  TODO - Check that returning nuke is at least 80% of the owned
            //  or traveling army
            foreach (var returning in context.ReturningToSource.Where(c => c.Army != null))
            {
                if (returning.Army.IsOffensive())
                    return FakeClassification.Fake | FakeClassification.Definite;
            }

            return FakeClassification.None;
        }
    }
}
