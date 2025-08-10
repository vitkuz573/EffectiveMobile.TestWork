using EffectiveMobile.TestWork.API.Models;

namespace EffectiveMobile.TestWork.API.Abstractions;

public interface IDataParser
{
    Task<IEnumerable<AdvertisingSpace>> ParseAsync(string filePath);
}
