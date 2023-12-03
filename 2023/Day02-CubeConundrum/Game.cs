namespace Day02_CubeConundrum;

internal class Game
{
    private Dictionary<string, int> _gameColorDictionary;

    public Game()
    {
        _gameColorDictionary = new Dictionary<string, int>();
    }

    public void AddGameToDictionary(string color, int numberOfcubes)
    {
        if (_gameColorDictionary.ContainsKey(color))
        {
            _gameColorDictionary[color] = numberOfcubes;
        }
        else
        {
            _gameColorDictionary.Add(color, numberOfcubes);
        }
    }

    public Dictionary<string, int> GetGameDictionary()
    {
        return _gameColorDictionary;
    }
}
