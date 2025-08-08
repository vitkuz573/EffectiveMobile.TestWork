namespace EffectiveMobile.TestWork.API.Abstractions;

public interface IDataService
{
    Task<bool> LoadAdvertisingSpacesAsync(string filePath);

    IEnumerable<string> GetAdvertisingSpaceNames(string location);
}
