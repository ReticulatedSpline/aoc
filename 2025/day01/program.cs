using System.IO;

int movePosition(int currentPos, int vector) {
	int vectorSum = currentPos + vector;
	switch (vectorSum) {
		case >= 99:
			Console.WriteLine("Overflow.");
			return vectorSum % 100;
		case < 0:
			Console.WriteLine("Underflow.");
			return (vectorSum + 100) % 100;
		default:
			return vectorSum;
	}
}

int zeroCount = 0;
int currentPos = 50;
foreach (string line in File.ReadLines("./input.dat")) {
	Console.WriteLine($"Dial position: {currentPos}");

	char direction = line[0];
	int scalar = int.Parse(line.Substring(1));
	Console.WriteLine($"Moving {direction} {scalar}");
	switch (direction) {
		case 'L':
			currentPos = movePosition(currentPos, -1 * scalar);
			break; 
		case 'R':
			currentPos = movePosition(currentPos, scalar);
			break;
		default:
			throw new InvalidOperationException("Invalid line: " + line);
	}

	if (currentPos == 0) {
		zeroCount++;
	}
}

Console.WriteLine($"Zero count: {zeroCount}");
