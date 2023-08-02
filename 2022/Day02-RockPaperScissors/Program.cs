using Day02_RockPaperScissors;

var gameData = File.ReadAllLines("input.txt");

var rockPaperScissors = new RockPaperScissors();

rockPaperScissors.PrintScoreForPartOne(gameData);
rockPaperScissors.PrintScoreForPartTwo(gameData);
