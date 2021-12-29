namespace TW.Vault.Features.CommandClassification.FakeDetectionRules
{
    interface IFakeDetectionRule
    {
        FakeClassification Classify(ClassificationContext context);
    }
}
