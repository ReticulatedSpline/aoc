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
    
    static bool isUnsafeNumber (int currentValue, int previousValue, bool isIncreasing) {
        return (isIncreasing && currentValue < previousValue) ||
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
            int reportIndex = 0;
            int previousValue = 0;
            int currentValue = 0;
            bool? isIncreasing  = null;
            bool isSafeReport = true;
            Console.WriteLine("\nReading new report...");
            foreach (int value in report) {
                if (reportIndex == 0) {
                    previousValue = value;
                    reportIndex++;
                    continue;
                } 
                
                if (reportIndex == 1) {
                    currentValue = value;
                    reportIndex++;
                    continue;
                }

                // need to handle the case where isIncreasing isn't set until later (equal first two+ ints)
                if (isIncreasing ==)

                if (isUnsafeNumber(currentValue, previousValue, isIncreasing)) {
                    isSafeReport = false;
                    break;
                }

                Console.WriteLine($"current {currentValue} previous {previousValue}");
                
                previousValue = currentValue;
                currentValue = value;
            }

            if (isUnsafeNumber(currentValue, previousValue, isIncreasing)) {
                isSafeReport = false;
            }

            if (isSafeReport) {
                part1++;
            }
        }

        Console.WriteLine($"Part 1: {part1}");
    }
}