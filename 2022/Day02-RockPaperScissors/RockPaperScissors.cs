using static Day02_RockPaperScissors.Constants;

namespace Day02_RockPaperScissors;

internal class RockPaperScissors
{
    public void PrintScoreForPartOne(string[] gameData)
    {
        int totalScore = 0;

        foreach (var game in gameData)
        {
            var choices = game.Split(" ");

            var opponentChoice = GetPlayerChoice(choices[0]);

            var challengerChoice = GetPlayerChoice(choices[1]);

            totalScore += GetScoreForChallengerChoice(challengerChoice);
            totalScore += CalculateScoreForGame(opponentChoice, challengerChoice);
        }

        Console.WriteLine(totalScore);
    }

    public void PrintScoreForPartTwo(string[] gameData)
    {
        int totalScore = 0;

        foreach (var game in gameData)
        {
            var choices = game.Split(" ");

            var opponentChoice = GetPlayerChoice(choices[0]);
            var challengerAction = GetChallengerAction(choices[1]);

            totalScore += CalculateScoreForPartTwoGame(opponentChoice, challengerAction);
        }

        Console.WriteLine(totalScore);
    }

    private Choice GetPlayerChoice(string choice)
    {
        return choice switch
        {
            A => Choice.Rock,
            X => Choice.Rock,

            B => Choice.Paper,
            Y => Choice.Paper,

            C => Choice.Scissors,
            Z => Choice.Scissors,
            _ => throw new NotImplementedException()
        };
    }

    private int GetScoreForChallengerChoice(Choice challengerChoice)
    {
        return challengerChoice switch
        {
            Choice.Rock => 1,
            Choice.Paper => 2,
            Choice.Scissors => 3,
            _ => throw new NotImplementedException(),
        };
    }

    private int CalculateScoreForGame(Choice opponentChoice, Choice playerChoice)
    {
        int score = 0;

        if (opponentChoice == playerChoice)
        {
            score += 3;
            return score;
        }

        switch (opponentChoice)
        {
            case Choice.Rock when playerChoice == Choice.Paper:
                score += 6;
                break;
            case Choice.Rock when playerChoice == Choice.Scissors:
                score += 0;
                break;
            case Choice.Paper when playerChoice == Choice.Scissors:
                score += 6;
                break;
            case Choice.Paper when playerChoice == Choice.Rock:
                score += 0;
                break;
            case Choice.Scissors when playerChoice == Choice.Rock:
                score += 6;
                break;
            case Choice.Scissors when playerChoice == Choice.Paper:
                score += 0;
                break;
        }

        return score;

    }

    private ChallengerAction GetChallengerAction(string action)
    {
        return action switch
        {
            X => ChallengerAction.LOSE,
            Y => ChallengerAction.DRAW,
            Z => ChallengerAction.WIN,
            _ => throw new NotImplementedException()
        };
    }

    private Choice BuildPlayerChoice(Choice opponentChoice, ChallengerAction challengerAction)
    {
        if (challengerAction == ChallengerAction.DRAW)
        {
            return opponentChoice;
        }

        Choice choice = Choice.Scissors;

        switch (opponentChoice)
        {
            case Choice.Rock when challengerAction == ChallengerAction.WIN: choice = Choice.Paper; break;
            case Choice.Rock when challengerAction == ChallengerAction.LOSE: choice = Choice.Scissors; break;
            case Choice.Paper when challengerAction == ChallengerAction.WIN: choice = Choice.Scissors; break;
            case Choice.Paper when challengerAction == ChallengerAction.LOSE: choice = Choice.Rock; break;
            case Choice.Scissors when challengerAction == ChallengerAction.WIN: choice = Choice.Rock; break;
            case Choice.Scissors when challengerAction == ChallengerAction.LOSE: choice = Choice.Paper; break;
        }

        return choice;
    }

    private int CalculateScoreForPartTwoGame(Choice opponentChoice, ChallengerAction ChallengerAction)
    {
        Choice challengerChoice = BuildPlayerChoice(opponentChoice, ChallengerAction);

        int gameScore = CalculateScoreForGame(opponentChoice, challengerChoice);
        int choiceScore = GetScoreForChallengerChoice(challengerChoice);

        return gameScore + choiceScore;
    }
}
