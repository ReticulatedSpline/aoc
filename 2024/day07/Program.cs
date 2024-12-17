using System.Text.RegularExpressions;

static List<(int, List<int>)> readFile(string filePath) {
    
    string resultPattern = @"(\d+):";
    string operandPattern = @"(?:\s(\d+))+";

    using (StreamReader sr = new StreamReader(filePath)) {
        string? line = sr.ReadLine();
        var inputModel = new List<(int, List<int>)>();
        while ((line = sr.ReadLine()) != null) {
    
            Match resultMatch = Regex.Match(line, resultPattern);
            int result = int.Parse(resultMatch.Value);
            
            MatchCollection operandMatches = Regex.Matches(line, operandPattern);
            var operands = new List<int>();
    
            foreach(Match match in operandMatches) {
                operands.Add(int.Parse(match.Value));
            }

            inputModel.Add((result, operands));
        }

        return inputModel;
    }
}

// iterative version of Heap's Algorithm adapted from Wikipedia
// https://en.wikipedia.org/wiki/Heap%27s_algorithm
static List<List<char>> generatePermutations(int n, List<char> operators) {
    
    var result = new List<List<char>>();
    
    // c is an encoding of the stack state
    // c[k] encodes the for-loop counter for when generate(k - 1, A) is called
    // initialized to zeroed list of length n 
    var c = new int[n];
    
    // add initial state to results list
    result.Add(new List<char>(operators));
    
    // i acts like a stack pointer here
    int i = 1;
    char temp;
    while (i < n) {
        if (c[i] < i) {
            if (i % 2 == 0) {
                temp = operators[0];
                operators[0] = operators[i];
                operators[i] = temp;
            } else {
                temp = operators[c[i]];
                operators[c[i]] = operators[i];
                operators[i] = temp;
            }
        
            result.Add(new List<char>(operators));
            
            // swap has occurred ending the while-loop
            // simulate the increment of the while-loop counter
            c[i]++;
            
            // simulate recursive call reaching the base case by
            // bringing the pointer to the base case analog in the array
            i = 1;
        } else {
            // calling generate(i+1, A) has ended as the while-loop terminated
            // reset the state and simulate popping the stack by incrementing the pointer.
            c[i] = 0;
            i++;
        }
    }

    return result;
}

static bool isPossibleEquation(int result, List<int> operands) {
    List<char> operators = ['+', '*'];
    foreach(List<char> operatorList in generatePermutations(operands.Count - 1, operators)) {
        bool calculationComplete = false;
        int sum = operands[0];
        operands.RemoveAt(0);
        while (!calculationComplete) {
            var currentOperator = operatorList[0];
            var currentOperand = operands[0];
            switch (currentOperator) {
                case '+':
                    sum += currentOperand;
                    break;
                case '*':
                    sum *= currentOperand;
                    break;
            }
            operands.RemoveAt(0);
            operators.RemoveAt(0);
            calculationComplete = operators.Count > 0 && operands.Count > 0 && sum <= result;
        }

        if (sum == result) {
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

foreach ((int result, List<int> operands) in equations) {
    if (isPossibleEquation(result, operands)) {
        partOneSum++;
    }
}

Console.WriteLine($"Part one: {partOneSum}");