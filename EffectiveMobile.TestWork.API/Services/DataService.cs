using EffectiveMobile.TestWork.API.Abstractions;

namespace EffectiveMobile.TestWork.API.Services;

public class DataService(IDataStorage dataStorage, IDataParser dataParser, ILogger<DataService> logger) : IDataService
{
    public async Task<bool> LoadAdvertisingSpacesAsync(string filePath)
    {
        var parseResult = await dataParser.ParseAsync(filePath);

        if (parseResult is null || !parseResult.Any())
        {
            logger.LogWarning("Failed to load advertising spaces from {FilePath}. Parser returned no items.", filePath);
            
            return false;
        }

        dataStorage.Clear();
        dataStorage.AddRange(parseResult);

        logger.LogInformation("Loaded {Count} advertising spaces from {FilePath}.", parseResult.Count(), filePath);

        return true;
    }

    public IEnumerable<string> GetAdvertisingSpaceNames(string location)
    {
        var result = dataStorage.Get(location);

        return result.Select(@as => @as.Name);
    }
}
