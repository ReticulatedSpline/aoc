using System.Text.RegularExpressions;

static (List<(int,int)>, List<List<int>>) readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line;
        List<(int, int)> ruleList = new List<(int, int)>();
        List<List<int>> pageList = new List<List<int>>();

        line = sr.ReadLine();
        while (line != "")
        {
            MatchCollection numbers = Regex.Matches(line, @"\d+");
            ruleList.Add(numbers[0].Value, numbers[1].Value);
            line = sr.ReadLine();
        }

        return (ruleList, pageList);
    }
}

int partOneSum = 0;
int partTwoSum = 0;
if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}
var rules, pages = readFile(args[0]);

Console.WriteLine($"Part One: {partOneSum}");
Console.WriteLine($"Part Two: {partTwoSum}");