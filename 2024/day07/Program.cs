using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

static List<(long, List<int>)> readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line;
        var inputModel = new List<(long, List<int>)>();
        while ((line = sr.ReadLine()) != null) {
            var parts = line.Split();
            long result = long.Parse(parts[0].Trim(':'));
            
            var operands = new List<int>();
            for(int i = 1; i<parts.Length; i++) {
                operands.Add(int.Parse(parts[i]));
            }

            inputModel.Add((result, operands));
        }

        return inputModel;
    }
}

static bool isPossibleEquation(long result, int currentSum, List<char> operators, List<int> operands) {
    
    Console.WriteLine($"Recursion level: {operands.Count} operands left.");
    if (operands.Count == 0) {
        Console.WriteLine($"Out of operands. Returning {currentSum} == {result}.");
        return currentSum == result;
    } else if (currentSum > result) {
        Console.WriteLine($"Current sum of {currentSum} has surpassed {result}.");
        return false;
    }

    foreach(var op in operators) {
        bool found = false;
        List<int> remainingOperands = operands.Skip(1).ToList();
        switch (op) {
            case '+':
                found = isPossibleEquation(result, currentSum + operands[0], operators, remainingOperands);
                break;
            case '*':
                found = isPossibleEquation(result, currentSum * operands[0], operators, remainingOperands);
                break;
            default:
                throw new InvalidOperationException("Unsupported operator");
        }

        if (found) {
            Console.WriteLine("Possible equation.");
            return true;
        }
    }

    return false;
}

if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}

var equations = readFile(args[0]);
long partOneSum = 0;

foreach ((long result, List<int> operands) in equations) {
    List<char> operators = ['*', '+'];

    Console.WriteLine();
    Console.Write($"Checking equation {result}: ");
    foreach(var operand in operands) {
        Console.Write($"{operand}, ");
    }
    Console.WriteLine();

    if (isPossibleEquation(result, 0, operators, operands)) {
        partOneSum += result;
    }
}

// 16,367,171,532 is too low
Console.WriteLine($"Part one: {partOneSum}");