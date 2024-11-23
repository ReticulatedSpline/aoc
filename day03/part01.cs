using System.IO;
using System.Security.Cryptography.X509Certificates;

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
        Console.WriteLine($"Checking columns {colSearchStart}-{colSearchEnd}, rows {rowSearchStart}-{rowSearchEnd} for symbols...");
        for (int x = colSearchStart; x <= colSearchEnd; x++) {
            for (int y = rowSearchStart; y <= rowSearchEnd; y++) {
                if (isSymbol(array[y, x])) {
                    Console.WriteLine($"Found symbol '{array[y, x]}' at adjacent location {y}, {x}");
                    return true;
                }
            } 
        }
        Console.WriteLine("Not considering this a part number.\n");
        return false;
    }

    static void Main(string[] args)
    {
        // read file into 2d array
        char[,] input = readFile("example.dat");
        int dimension = input.GetLength(0);
        int programSum = 0;
        int startY = 0;
        for (int x = 0; x < dimension; x++) {
            // consume each line by char
            string numString = "";
            bool readingNum = false;
            for (int y = 0; y < dimension; y++) {
                int possiblePartNum;
                // if char is num, add to potential number string
                if (char.IsDigit(input[x,y])) {
                    numString += input[x,y];
                    if (!readingNum) {
                        startY = y;
                    } 
                    readingNum = true;
                // if char is period, reset vars, parse number string
                } else if (!char.IsDigit(input[x,y]) && !string.IsNullOrEmpty(numString)) {
                    possiblePartNum = int.Parse(numString);
                    Console.WriteLine($"Found number string '{numString}' in row {x}, columns {startY}-{y-1}");
                    // check for adjacent symbols
                    if (isPartNumber(input, startY, y-1, x, dimension)) {
                        // if symbol exists, add to program sum
                        Console.WriteLine("Considering this a part number!");
                        programSum += possiblePartNum;
                        Console.WriteLine($"Running sum: {programSum}\n");
                    }
                    startY = 0;
                    numString = "";
                    readingNum = false;
                }
            }
        }

        // print sum
        Console.WriteLine($"Total: {programSum}");
    }
}