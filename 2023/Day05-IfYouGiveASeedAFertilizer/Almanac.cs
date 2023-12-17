namespace Day05_IfYouGiveASeedAFertilizer;

internal class Almanac
{
    public List<long> Seeds;

    public List<SeedToLocationInfo> SeedToSoilMap;

    public List<SeedToLocationInfo> SoilToFertilizerMap;

    public List<SeedToLocationInfo> FertilizerToWaterMap;

    public List<SeedToLocationInfo> WaterToLightMap;

    public List<SeedToLocationInfo> LightToTemperatureMap;

    public List<SeedToLocationInfo> TemperatureToHumidityMap;

    public List<SeedToLocationInfo> HumidityToLocationMap;

    public Almanac()
    {
        Seeds = new List<long>();
        SeedToSoilMap = new List<SeedToLocationInfo>();
        SoilToFertilizerMap = new List<SeedToLocationInfo>();
        FertilizerToWaterMap = new List<SeedToLocationInfo>();
        WaterToLightMap = new List<SeedToLocationInfo>();
        LightToTemperatureMap = new List<SeedToLocationInfo>();
        TemperatureToHumidityMap = new List<SeedToLocationInfo>();
        HumidityToLocationMap = new List<SeedToLocationInfo>();
    }
}
