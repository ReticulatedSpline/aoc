using System.Text.RegularExpressions;

static List<(uint, List<uint>)> readFile(string filePath) {
    
    string resultPattern = @"(\d+):";
    string operandPattern = @"(?:\s(\d+))+";

    using (StreamReader sr = new StreamReader(filePath)) {
        string? line = sr.ReadLine();
        var inputModel = new List<(uint, List<uint>)>();
        while ((line = sr.ReadLine()) != null) {
    
            Match resultMatch = Regex.Match(line, resultPattern);
            uint result = uint.Parse(resultMatch.Value);
            
            MatchCollection operandMatches = Regex.Matches(line, operandPattern);
            var operands = new List<uint>();
    
            foreach(Match match in operandMatches) {
                operands.Add(uint.Parse(match.Value));
            }

            inputModel.Add((result, operands));
        }

        return inputModel;
    }
}

static bool isPossibleEquation(uint result, List<uint> operands) {
    if (operands.Count < 2) {
        return false;
    } else if (operands.Count == 2) {
        return operands[0] * operands[1] == result ||
               operands[0] + operands[1] == result;
    }

    // WIP: General case
}

if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}

var equations = readFile(args[0]);
uint partOneSum = 0;

foreach ((uint result, List<uint> operands) in equations) {
    if (isPossibleEquation(result, operands)) {
        partOneSum++;
    }
}

Console.WriteLine($"Part one: {partOneSum}");