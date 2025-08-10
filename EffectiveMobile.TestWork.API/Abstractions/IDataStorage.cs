using EffectiveMobile.TestWork.API.Models;

namespace EffectiveMobile.TestWork.API.Abstractions;

public interface IDataStorage
{
    void AddRange(IEnumerable<AdvertisingSpace> advertisingSpaces);

    IEnumerable<AdvertisingSpace> Get(string location);

    void Clear();
}
