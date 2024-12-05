using System.Text.RegularExpressions;

static string readFile(string filePath) {
    try
    {
        using StreamReader reader = new(filePath);
        return reader.ReadToEnd();
    }
    catch (IOException e)
    {
        Console.WriteLine("Input could not be read:");
        Console.WriteLine(e.Message);
        return "";
    }
}

string pattern = @"mul\((\d+),(\d+)\)";
string input = readFile("input.dat");

int partOneSum = 0;
int partTwoSum = 0;

// for part 1, just sum
foreach (Match match in Regex.Matches(input, pattern)) {
    partOneSum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
}

// for part two, trim no-ops...
string partTwoInput = "";
bool currentlyOmitting = false;
for (int index = 0; index < input.Length; index++) {
    try {
        if (input.Substring(index, 7).Equals("don't()")) {
            currentlyOmitting = true;
        } else if (input.Substring(index, 4).Equals("do()")) {
            currentlyOmitting = false;
        }
    } catch (System.ArgumentOutOfRangeException) {
        // no-op
    };

    if (!currentlyOmitting) {
        partTwoInput += input[index];
    }
}

// then sum
foreach (Match match in Regex.Matches(partTwoInput, pattern)) {
    partTwoSum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
}

Console.WriteLine($"Part 1: {partOneSum}");
Console.WriteLine($"Part 2: {partTwoSum}");