using System.Diagnostics.CodeAnalysis;
using System.Numerics;

static List<(Int64, List<int>)> readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line;
        var inputModel = new List<(Int64, List<int>)>();
        while ((line = sr.ReadLine()) != null) {
            var parts = line.Split();
            Int64 result = Int64.Parse(parts[0].Trim(':'));
            
            var operands = new List<int>();
            for(int i = 1; i<parts.Length; i++) {
                operands.Add(int.Parse(parts[i]));
            }

            inputModel.Add((result, operands));
        }

        return inputModel;
    }
}

static bool isPossibleEquation(Int64 result, int currentSum, List<char> operators, List<int> operands) {
    if (operands.Count == 0) {
        return currentSum == result;
    }

    for (int i = 0; i < operators.Count; i++) {
        char op = operators[i];
        int currentOperand = operands[0];
        List<int> remainingOperands = operands.Skip(1).ToList();
        bool found = false;
        switch (op) {
            case '+':
                found = isPossibleEquation(result, currentSum + currentOperand, operators, remainingOperands);
                break;
            case '*':
                found = isPossibleEquation(result, currentSum * currentOperand, operators, remainingOperands);
                break;
            default:
                throw new InvalidOperationException("Unsupported operator");
        }

        if (found) {
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
int partOneSum = 0;

foreach ((Int64 result, List<int> operands) in equations) {
    List<char> operators = ['*', '+'];
    
    if (isPossibleEquation(result, 0, operators, operands)) {
        partOneSum++;
    }
}

// 182 is too low
Console.WriteLine($"Part one: {partOneSum}");