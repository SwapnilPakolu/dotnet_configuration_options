using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

[ApiController]
[Route("api/feature")]
public class FeatureController : ControllerBase
{
    private readonly IFeatureManager _featureManager;
    private readonly IFeatureManagerSnapshot _featureManagerSnapshot;

    public FeatureController(IFeatureManager featureManager, IFeatureManagerSnapshot featureManagerSnapshot)
    {
        _featureManager = featureManager;
        _featureManagerSnapshot = featureManagerSnapshot;
    }

    [HttpGet("{featureName}")]
    public async Task<IActionResult> GetFeatureStatus(string featureName)
    {
        // Get all available feature flags
        var allFeatureFlags = await _featureManagerSnapshot.GetFeatureNamesAsync();

        // Check if the requested feature flag actually exists
        if (!allFeatureFlags.Contains(featureName))
        {
            return NotFound(new { Message = $"Feature flag '{featureName}' does not exist in Azure App Configuration." });
        }

        // Now safely check if the feature is enabled
        bool isEnabled = await _featureManager.IsEnabledAsync(featureName);

        return Ok(new { Feature = featureName, Enabled = isEnabled });

    }
}
