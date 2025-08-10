using EffectiveMobile.TestWork.API.Abstractions;
using EffectiveMobile.TestWork.API.Models;
using System.IO.Abstractions;

namespace EffectiveMobile.TestWork.API.Services;

public class CustomFormatDataParser(IFileSystem fileSystem, ILogger<CustomFormatDataParser> logger) : IDataParser
{
    public async Task<IEnumerable<AdvertisingSpace>> ParseAsync(string filePath)
    {
        var advertisingSpaces = new List<AdvertisingSpace>();

        string[] lines;

        try
        {
            lines = await fileSystem.File.ReadAllLinesAsync(filePath);
        }
        catch (Exception ex)
        {
            logger.LogInformation("Error reading file: {FilePath}. Exception: {ExceptionMessage}", filePath, ex.Message);

            return [];
        }

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

            advertisingSpaces.Add(advertisingSpace);
        }

        return advertisingSpaces;
    }
}
