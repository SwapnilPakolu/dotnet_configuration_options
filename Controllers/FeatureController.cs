using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

[ApiController]
[Route("api/feature")]
public class FeatureController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public FeatureController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet("{featureName}")]
    public async Task<IActionResult> GetFeatureStatus(string featureName)
    {
        bool isEnabled = await _featureManager.IsEnabledAsync(featureName);
        return Ok(new { Feature = featureName, Enabled = isEnabled });
    }
}
