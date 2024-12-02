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

    static (List<int>, List<int>) parseInput(string fileContents) {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        
        StringReader sr = new StringReader(fileContents);
        string? line = sr.ReadLine();
        
        while (line != null && !string.IsNullOrEmpty(line))
        {
            string[] splitString = line.Split(' ', StringSplitOptions.TrimEntries);
            list1.Add(int.Parse(splitString[0]));
            list2.Add(int.Parse(splitString[3]));
            line = sr.ReadLine();
        }

        return (list1, list2);
    }
    
    static void Main(string[] args)
    {
        Console.WriteLine("Running...");
        string? fileContents = readFile("input.dat");
        
        if (fileContents == null) {
            return;
        }

        List<int> list1;
        List<int> list2;
        (list1, list2) = parseInput(fileContents);
        list1.Sort();
        list2.Sort();
        int sum = 0;
        for (int x = 0; x < list1.Count; x++) {
            sum += Math.Abs(list1[x] - list2[x]);
        }
        Console.WriteLine($"Sum: {sum}");
    }
}

