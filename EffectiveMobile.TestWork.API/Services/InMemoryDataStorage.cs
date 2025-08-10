using EffectiveMobile.TestWork.API.Abstractions;
using EffectiveMobile.TestWork.API.Models;

namespace EffectiveMobile.TestWork.API.Services;

public class InMemoryDataStorage(ILogger<InMemoryDataStorage> logger) : IDataStorage
{
    private readonly List<AdvertisingSpace> _advertisingSpaces = [];

    public void AddRange(IEnumerable<AdvertisingSpace> advertisingSpaces)
    {
        _advertisingSpaces.AddRange(advertisingSpaces);
    }

    public IEnumerable<AdvertisingSpace> Get(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            logger.LogInformation("Location is null or empty.");

            return [];
        }

        var result = _advertisingSpaces
            .Where(@as => @as.Locations.Any(l => location.StartsWith(l, StringComparison.OrdinalIgnoreCase)));

        if (!result.Any())
        {
            logger.LogInformation("No advertising spaces found for location: {Location}", location);

            return [];
        }

        return result;
    }

    public void Clear()
    {
        _advertisingSpaces.Clear();
    }
}
