using System.Diagnostics.Contracts;
using System.Threading.Tasks.Dataflow;

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

    static bool isSafeReport(List<int> report) {
        int previousValue = report[0];
        int currentValue = report[1];
        bool isIncreasing = currentValue > previousValue;
        for (int index = 2; index < report.Count; index++) {

            Console.WriteLine($"Current: {currentValue}, Previous: {previousValue}");
            if (isUnsafeNumber(currentValue, previousValue, isIncreasing)) {
                Console.WriteLine("Unsafe.");
                return false;
            }

            previousValue = currentValue;
            currentValue = report[index];
        }
        return true;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        string? fileContents = readFile("example.dat");
        
        if (fileContents == null) {
            return;
        }

        List<List<int>> reports = parseInput(fileContents);

        int sum = 0;
        foreach (List<int> report in reports) {
            Console.WriteLine("\nReading new report...");
            bool safe = true;
            bool damped = false;

            if (isSafeReport(report)) {
                Console.WriteLine("Undamped report is safe.");
                sum++;
            } else {
                for (int removedIndex = 0; removedIndex < report.Count; removedIndex++) {
                    Console.WriteLine($"Damping index {removedIndex}...");
                    List<int> dampedReport;
                    dampedReport = new List<int>(report);
                    dampedReport.RemoveAt(removedIndex);
                    safe = isSafeReport(report);

                    if (!safe && !damped) {
                        safe = true;
                        damped = true;
                    } else if (safe) {
                        sum++;
                        Console.WriteLine("Safe.");
                        break;
                    }
                }
            }
        }
        // more than 588
        Console.WriteLine($"Part 2: {sum}");
    }
}