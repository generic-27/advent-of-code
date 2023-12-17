using Day05_IfYouGiveASeedAFertilizer;


void PartOne()
{
    var almanac = BuildAlmanac(true);

    long min = GetTheMinimumLocation(almanac);

    Console.WriteLine(min);
}

void PartTwo()
{
    var almanac = BuildAlmanac();

    long min = GetTheMinimumLocation(almanac);

    Console.WriteLine(min);
}

Almanac BuildAlmanac(bool isPartOne = false)
{
    var fileInfo = File.ReadAllText("input.txt");

    var almanacInfo = fileInfo.Split("\r\n\r");

    var almanac = new Almanac();

    if (isPartOne)
    {
        almanac.Seeds = GetSeedsForPartOne(almanacInfo[0]);
    }
    else
    {
        almanac.Seeds = GetSeedsForPartTwo(almanacInfo[0]);
    }


    var seedToSoilMapInfo = almanacInfo[1].Split("\r\n");
    almanac.SeedToSoilMap = GetSeedToLocatonMapList(seedToSoilMapInfo);

    var soilToFertilizerInfo = almanacInfo[2].Split("\r\n");
    almanac.SoilToFertilizerMap = GetSeedToLocatonMapList(soilToFertilizerInfo);

    var fertilizerToWaterInfo = almanacInfo[3].Split("\r\n");
    almanac.FertilizerToWaterMap = GetSeedToLocatonMapList(fertilizerToWaterInfo);

    var waterToLightInfo = almanacInfo[4].Split("\r\n");
    almanac.WaterToLightMap = GetSeedToLocatonMapList(waterToLightInfo);

    var lightToTemperatureInfo = almanacInfo[5].Split("\r\n");
    almanac.LightToTemperatureMap = GetSeedToLocatonMapList(lightToTemperatureInfo);

    var temperatureToHumidityInfo = almanacInfo[6].Split("\r\n");
    almanac.TemperatureToHumidityMap = GetSeedToLocatonMapList(temperatureToHumidityInfo);

    var humidityToLocationInfo = almanacInfo[7].Split("\r\n");
    almanac.HumidityToLocationMap = GetSeedToLocatonMapList(humidityToLocationInfo);

    return almanac;
}

List<long> GetSeedsForPartOne(string seedInformation)
{
    return seedInformation.Split(": ")[1].Split(" ").Select(c => long.Parse(c)).ToList();
}

List<long> GetSeedsForPartTwo(string seedInformation)
{

    var seedRanges = seedInformation.Split(": ")[1].Split(" ").Select(c => long.Parse(c)).ToList();

    var list = new List<long>();

    for (int i = 0; i < seedRanges.Count; i += 2)
    {

        var currentSeed = seedRanges[i];
        var rangeLength = seedRanges[i + 1];

        for (long j = currentSeed; j < currentSeed + rangeLength - 1; j++)
        {
            list.Add(j);
        }
    }

    return list;
}

long GetTheMinimumLocation(Almanac almanac)
{
    long min = long.MaxValue;

    foreach (var seed in almanac.Seeds)
    {
        long soilValue = GetTheNextAlmanacPropertyValue(almanac.SeedToSoilMap, seed);

        long fertilizerValue = GetTheNextAlmanacPropertyValue(almanac.SoilToFertilizerMap, soilValue);

        long waterValue = GetTheNextAlmanacPropertyValue(almanac.FertilizerToWaterMap, fertilizerValue);

        long lightValue = GetTheNextAlmanacPropertyValue(almanac.WaterToLightMap, waterValue);

        long temperatureValue = GetTheNextAlmanacPropertyValue(almanac.LightToTemperatureMap, lightValue);

        long humidityValue = GetTheNextAlmanacPropertyValue(almanac.TemperatureToHumidityMap, temperatureValue);

        long locationValue = GetTheNextAlmanacPropertyValue(almanac.HumidityToLocationMap, humidityValue);

        min = locationValue < min ? locationValue : min;
    }

    return min;
}

long GetTheNextAlmanacPropertyValue(List<SeedToLocationInfo> map, long currentValue)
{
    foreach (var almanacMapInfo in map)
    {
        var source = almanacMapInfo.Source;
        var lastSource = almanacMapInfo.Source + almanacMapInfo.RangeLength - 1;

        var destination = almanacMapInfo.Destination;
        var lastDestination = almanacMapInfo.Destination + almanacMapInfo.RangeLength;

        if (currentValue >= source && currentValue <= lastSource)
        {
            var difference = currentValue - source;

            return destination + difference;
        }
    }

    return currentValue;
}

List<SeedToLocationInfo> GetSeedToLocatonMapList(string[] mapData)
{
    var seedToLocationMapList = new List<SeedToLocationInfo>();

    for (int i = 1; i < mapData.Length; i++)
    {
        var almanacMap = new SeedToLocationInfo();

        var infoList = mapData[i].Split(" ").Select(c => long.Parse(c)).ToList();

        long source = infoList[1];
        long destination = infoList[0];
        long rangeLength = infoList[2];

        almanacMap.Source = source;
        almanacMap.Destination = destination;
        almanacMap.RangeLength = rangeLength;

        seedToLocationMapList.Add(almanacMap);
    }

    return seedToLocationMapList;
}

PartOne();
PartTwo();