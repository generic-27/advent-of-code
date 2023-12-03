using Day02_CubeConundrum;

var gameData = File.ReadAllLines("input.txt");

Dictionary<int, List<Game>> GetGameIdDictionary()
{
    var gameIdDictionary = new Dictionary<int, List<Game>>();

    foreach (var data in gameData)
    {

        var idAndGameData = data.Split(":");

        var id = int.Parse(idAndGameData[0].Split(" ")[1]);

        var gamesData = idAndGameData[1].Split(";").Select(c => c.Trim()).ToList();

        var gameList = new List<Game>();

        foreach (var gameData in gamesData)
        {
            var gameArray = gameData.Split(",").Select(c => c.Trim()).ToList();

            var game = new Game();

            foreach (var cube in gameArray)
            {
                var colorAndNumberCubeArray = cube.Split(" ");

                var numberOfCubes = int.Parse(colorAndNumberCubeArray[0]);

                game.AddGameToDictionary(colorAndNumberCubeArray[1], numberOfCubes);
            }

            gameList.Add(game);
        }

        gameIdDictionary.Add(id, gameList);
    }

    return gameIdDictionary;
}

void PartOne()
{
    var cubeLimitDictionary = new Dictionary<string, int>
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    int total = 0;

    Dictionary<int, List<Game>> gameIdDictionary = GetGameIdDictionary();

    foreach (var kvp in gameIdDictionary)
    {
        var listOfGames = kvp.Value;

        var gameWorks = true;

        foreach (var game in listOfGames)
        {
            var gameDictionary = game.GetGameDictionary();

            foreach (var keyValue in gameDictionary)
            {
                var cubeCount = keyValue.Value;
                var cubeColor = keyValue.Key;

                if (cubeLimitDictionary[cubeColor] < cubeCount)
                {
                    gameWorks = false;
                    break;
                }
            }

            if (!gameWorks)
            {
                break;
            }
        }

        if (gameWorks)
        {
            total += kvp.Key;
        }
    }

    Console.WriteLine(total);
}

PartOne();

void PartTwo()
{
    int total = 0;

    var gameIdDictionary = GetGameIdDictionary();

    foreach (var kvp in gameIdDictionary)
    {
        var listOfGames = kvp.Value;

        int power = 0;

        int maxRed = 0;
        int maxBlue = 0;
        int maxGreen = 0;

        foreach (var game in listOfGames)
        {
            var gameDictionary = game.GetGameDictionary();

            gameDictionary.TryGetValue("red", out int red);
            gameDictionary.TryGetValue("blue", out int blue);
            gameDictionary.TryGetValue("green", out int green);

            maxRed = red > maxRed ? red : maxRed;
            maxBlue = blue > maxBlue ? blue : maxBlue;
            maxGreen = green > maxGreen ? green : maxGreen;
        }

        power = maxRed * maxBlue * maxGreen;

        total += power;
    }

    Console.WriteLine(total);
}

PartTwo();