class Program {
    static string? readFile(string filePath) {
        try
        {
            using StreamReader reader = new(filePath);
            return reader.ReadToEnd();
        }
        catch (IOException e)
        {
            Console.WriteLine("Input could not be read:");
            Console.WriteLine(e.Message);
            return null;
        }
    }

    static List<List<int>> parseInput(string fileContents) {
        List<List<int>> reports = new List<List<int>>();

        StringReader sr = new StringReader(fileContents);
        string? line = sr.ReadLine();
        
        while (line != null && !string.IsNullOrEmpty(line))
        {
            List<int> report = new List<int>();
            string[] splitStrings = line.Split(' ');
            foreach (string str in splitStrings) {
                report.Add(int.Parse(str));
            }
            reports.Add(report);
            line = sr.ReadLine();
        }

        return reports;
    }
    
    static bool isUnsafeNumber(int currentValue, int previousValue, bool isIncreasing) {
        return currentValue == previousValue ||
              (isIncreasing && currentValue < previousValue) ||
             (!isIncreasing && currentValue > previousValue) ||
                Math.Abs(currentValue - previousValue) > 3;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        string? fileContents = readFile("input.dat");
        
        if (fileContents == null) {
            return;
        }

        List<List<int>> reports = parseInput(fileContents);

        int part1 = 0;
        foreach (List<int> report in reports) {
            int previousValue = report[0];
            int currentValue = report[1];
            bool isIncreasing = currentValue > previousValue;
            bool isSafeReport = true;
            Console.WriteLine("\nReading new report...");
            for (int index = 2; index <= report.Count; index++) {

                Console.WriteLine($"current {currentValue} previous {previousValue}");

                if (isUnsafeNumber(currentValue, previousValue, isIncreasing)) {
                    Console.WriteLine($"Unsafe!");
                    isSafeReport = false;
                    break;
                }

                if (index < report.Count) {
                    previousValue = currentValue;
                    currentValue = report[index];
                }
            }

            if (isSafeReport) {
                part1++;
            }
        }
        // Less than 570
        Console.WriteLine($"Part 1: {part1}");
    }
}