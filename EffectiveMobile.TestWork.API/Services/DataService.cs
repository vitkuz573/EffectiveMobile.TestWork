using EffectiveMobile.TestWork.API.Abstractions;
using EffectiveMobile.TestWork.API.Models;

namespace EffectiveMobile.TestWork.API.Services;

public class DataService(ILogger<DataService> logger) : IDataService
{
    private readonly List<AdvertisingSpace> _advertisingSpaces = [];

    public async Task<bool> LoadAdvertisingSpacesAsync(string filePath)
    {
        string[] lines;

        try
        {
            lines = await File.ReadAllLinesAsync(filePath);
        }
        catch (Exception ex)
        {
            logger.LogInformation("Error reading file: {FilePath}. Exception: {ExceptionMessage}", filePath, ex.Message);

            return false;
        }

        _advertisingSpaces.Clear();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(':', 2);

            if (parts.Length < 2)
            {
                logger.LogWarning("Invalid line format (missing ':'): {Line}", line);
                
                continue;
            }

            var advertisingSpaceName = parts[0].Trim();

            if (string.IsNullOrWhiteSpace(advertisingSpaceName))
            {
                logger.LogWarning("Empty advertising space name in line: {Line}", line);
                
                continue;
            }

            List<string> advertisingSpaceLocations;

            try
            {
                advertisingSpaceLocations = [.. parts[1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())];
            }
            catch (Exception ex)
            {
                logger.LogError("Error while parsing locations in line: {Line}. Exception: {ExceptionMessage}", line, ex.Message);
                
                advertisingSpaceLocations = [];
            }

            var advertisingSpace = new AdvertisingSpace(advertisingSpaceName, advertisingSpaceLocations);
            
            _advertisingSpaces.Add(advertisingSpace);
        }

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
