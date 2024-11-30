class Program
{
    private static char[,] readFile(string inputFile) {
        string? line;
        char[,]? engineSchematic;
        try {
            using (StreamReader sr = new StreamReader(inputFile)) {
                line = sr.ReadLine();
                if (line == null) {
                    return new char[0,0];
                }

                int lineNum = 0;
                engineSchematic = new char[line.Length,line.Length];
                while (line != null && !string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < line.Length; i++) {
                        engineSchematic[lineNum, i] = line[i];
                    }
                    line = sr.ReadLine();
                    lineNum++;
                }
            }
            return engineSchematic;
        } catch(Exception e) {
            Console.WriteLine($"Exception reading file '{inputFile}': {e}");
        }
        return new char[0,0];
    }

    static bool isSymbol(char input) {
        return input != '.' && !char.IsDigit(input);
    }
    static bool isPartNumber(char[,] array, int startCol, int endCol, int row, int dimensions) {
        int colSearchStart = (startCol > 0) ? startCol - 1 : 0;
        int colSearchEnd = (endCol < dimensions - 1) ? endCol + 1 : dimensions - 1;
        int rowSearchStart = (row > 0) ? row - 1 : 0;
        int rowSearchEnd = (row < dimensions - 1) ? row + 1 : dimensions - 1;
        
        for (int x = colSearchStart; x <= colSearchEnd; x++) {
            for (int y = rowSearchStart; y <= rowSearchEnd; y++) {
                if (isSymbol(array[y, x])) {
                    return true;
                }
            } 
        }
        return false;
    }

    static int calculateProgramSum(char[,] input) {
        int dimension = input.GetLength(0);
        int programSum = 0;
        int startY = 0;
        for (int x = 0; x < dimension; x++) {
            string numString = "";
            bool readingNum = false;
            
            // consume each line by char
            for (int y = 0; y < dimension; y++) {
                
                // if char is num, add to potential number string
                if (char.IsDigit(input[x,y])) {
                    numString += input[x,y];
                    if (!readingNum) {
                        startY = y;
                    } 
                    readingNum = true;
                }
                
                // if we have a number substring and it ends, reset vars, parse number string
                if (!string.IsNullOrEmpty(numString) && ((y == dimension - 1) ||!char.IsDigit(input[x,y]))) {
                    int possiblePartNum = int.Parse(numString);
                    
                    // check for adjacent symbols
                    if (isPartNumber(input, startY, y-1, x, dimension)) {
                        // if symbol exists, add to program sum
                        programSum += possiblePartNum;
                        
                    }
                    startY = 0;
                    numString = "";
                    readingNum = false;
                }
            }
        }
        return programSum;
    }

    static int getGearRatio(int x, int y, char[,] input) {
        int dimension = input.GetLength(0);
        int searchStartColumn = (y > 1) ? y - 1 : 0;
        int searchEndColumn = (y < dimension - 1) ? y + 1 : dimension;
        int searchStartRow = (x > 0) ? x - 1 : 0;
        int searchEndRow = (x < dimension - 1) ? x + 1 : dimension;
        
        string currentNumber = "";
        bool numberRead = false;
        int numbersRead = 0;
        int firstNumber = 0;
        int secondNumber = 0;
        for (int i = searchStartRow; i <= searchEndRow; i++) {
            for (int j = searchStartColumn; j <= searchEndColumn; j++) {
                
                if (numbersRead > 2) {
                    Console.WriteLine("No ratio found -- too many numbers!");
                    break;
                }

                int numStartIndex = j;
                char currentChar = input[i, j];
                while (char.IsDigit(currentChar) && numStartIndex > 0) {
                    numStartIndex--;
                    currentChar = input[i, numStartIndex];
                    numberRead = true;
                }

                if(!char.IsDigit(currentChar)) {
                    numStartIndex++;
                }

                if (numberRead) {
                    int numIndex = numStartIndex;
                    currentChar = input[i, numIndex];
                    while (char.IsDigit(currentChar) && numIndex < dimension-1) {
                        currentNumber += currentChar;
                        numIndex++;
                        currentChar = input[i, numIndex];
                    }

                    j = numIndex;

                    if (firstNumber == 0) {
                        firstNumber = int.Parse(currentNumber);
                    } else {
                        secondNumber = int.Parse(currentNumber);
                    }
                    numbersRead++;
                    numberRead = false;
                    currentNumber = "";
                }
            }
        }

        if (numbersRead < 2) {
            Console.WriteLine("No ratio found.");
            return 0;
        }

        int ratio = firstNumber * secondNumber;
        Console.WriteLine($"Found ratios {firstNumber} and {secondNumber} for a ratio of {ratio}!");
        return ratio;
    }

    static int calculateGearRatioSum(char[,] input) {
        int dimension = input.GetLength(0);
        int gearRatioSum = 0;
        for (int x = 0; x < dimension; x++) {
            for (int y = 0; y < dimension; y++) {
                if (input[x,y] == '*') {
                    Console.WriteLine($"\nFound asterisk at {x}, {y}");
                    gearRatioSum += getGearRatio(x, y, input);
                    Console.WriteLine($"Current ratio sum: {gearRatioSum}");
                }
            }
        }
        return gearRatioSum;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        char[,] input = readFile("input.dat");
        int programSum = calculateProgramSum(input);
        Console.WriteLine($"Part sum: {programSum}");

        int gearRatioSum = calculateGearRatioSum(input);
        Console.WriteLine($"Gear ratio sum: {gearRatioSum}");
    }
}