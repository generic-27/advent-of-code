using System.Text;
using System.Text.RegularExpressions;

var instructions = File.ReadAllText("input.txt");

void PartOne()
{
    const string pattern = @"mul\([0-9]{0,3},[0-9]{0,3}\)";
    
    Regex regex = new Regex(pattern);
    
    MatchCollection matches = regex.Matches(instructions);

    long sum = 0;
    for (var i = 0; i < matches.Count; i++)
    {
        var multipliedValues = GetMultipliedValue(matches[i].Value);
        sum += multipliedValues;
    }
    
    Console.WriteLine(sum);
}

void PartTwo()
{
    const string mulPattern = @"mul\([0-9]{0,3},[0-9]{0,3}\)";
    const string doPattern = @"do\(\)";
    const string dontPattern = @"don\'t\(\)";
    
    Regex mulRegex = new Regex(mulPattern);
    MatchCollection mulMatches = mulRegex.Matches(instructions);
    
    Regex doRegex = new Regex(doPattern);
    MatchCollection doMatches = doRegex.Matches(instructions);
    
    Regex dontRegex = new Regex(dontPattern);
    MatchCollection dontMatches = dontRegex.Matches(instructions);

    var matchSortedDictionary = new SortedDictionary<int, string>();

    for (var i = 0; i < mulMatches.Count; i++)
    {
        matchSortedDictionary.Add(mulMatches[i].Index, mulMatches[i].Value);
    }

    for (var i = 0; i < doMatches.Count; i++)
    {
        matchSortedDictionary.Add(doMatches[i].Index, doMatches[i].Value);
    }

    for (var i = 0; i < dontMatches.Count; i++)
    {
        matchSortedDictionary.Add(dontMatches[i].Index, dontMatches[i].Value);
    }

    var stack = new Stack<string>();
    stack.Push("do()");

    long sum = 0;
    foreach (var value in matchSortedDictionary.Values)
    {
        if (value.StartsWith("mul(") && stack.Peek() != "don't()")
        {
            var multipliedValue = GetMultipliedValue(value);
            sum += multipliedValue;
        }
        else if (value == "do()" || value == "don't()")
        {
            stack.Pop();
            stack.Push(value);
        }
    }
    
    Console.WriteLine(sum);
}

int GetMultipliedValue(string input)
{
    const string pattern = @"[0-9]{0,3},[0-9]{0,3}";
    
    Regex regex = new Regex(pattern);
    MatchCollection matches = regex.Matches(input);
    
    var values = matches.First().Value.Split(',');
    return int.Parse(values[0]) * int.Parse(values[1]);
}

PartOne();
PartTwo();