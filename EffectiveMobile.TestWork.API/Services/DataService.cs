using EffectiveMobile.TestWork.API.Abstractions;
using EffectiveMobile.TestWork.API.Models;

namespace EffectiveMobile.TestWork.API.Services;

public class DataService(IDataParser dataParser, ILogger<DataService> logger) : IDataService
{
    private readonly List<AdvertisingSpace> _advertisingSpaces = [];

    public async Task<bool> LoadAdvertisingSpacesAsync(string filePath)
    {
        var parseResult = await dataParser.ParseAsync(filePath);

        if (parseResult is null || !parseResult.Any())
        {
            logger.LogWarning("Failed to load advertising spaces from {FilePath}. Parser returned no items.", filePath);
            
            return false;
        }

        _advertisingSpaces.Clear();
        _advertisingSpaces.AddRange(parseResult);

        logger.LogInformation("Loaded {Count} advertising spaces from {FilePath}.", _advertisingSpaces.Count, filePath);

        return true;
    }

    public IEnumerable<string> GetAdvertisingSpaceNames(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            logger.LogInformation("Location is null or empty.");

            return [];
        }

        var result = _advertisingSpaces
            .Where(@as => @as.Locations.Any(l => location.StartsWith(l, StringComparison.OrdinalIgnoreCase)))
            .Select(@as => @as.Name)
            .ToList();

        if (result.Count == 0)
        {
            logger.LogInformation("No advertising spaces found for location: {Location}", location);
        }

        return result;
    }
}
