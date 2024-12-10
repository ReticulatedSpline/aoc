using System.Data;

static (char[,], (uint X, uint Y)) readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line = sr.ReadLine();
        var loc = (0u,0u);

        if (line is  null) {
            return (new char[0,0], loc);
        }

        var map = new char[line.Length,line.Length];
        uint row = 0;
        do {
            char[] lineChars = line.ToCharArray();
            for (uint col = 0; col < line.Length; col++){
                map[row, col] = lineChars[col];
                if (map[row,col] == '^') {
                    loc = (row, col);
                }
            }
            row++;
        } while ((line = sr.ReadLine()) != null);

        return (map, loc);
    }
}

static (Direction, (uint X, uint Y)) advancePosition(char[,] map, (uint X, uint Y) loc, Direction dir) {
    if (map[loc.X, loc.Y] != '#') {
        switch (dir) {
            case Direction.Up:
                loc = (loc.X, loc.Y - 1);
                break;
            case Direction.Right:
                loc = (loc.X + 1, loc.Y);
                break;
            case Direction.Down:
                loc = (loc.X, loc.Y + 1);
                break;
            case Direction.Left:
                loc = (loc.X - 1, loc.Y);
                break;
        }
    } else {
        switch (dir) {
            case Direction.Up:
                dir = Direction.Right;
                loc = (loc.X + 1, loc.Y);
                break;
            case Direction.Right:
                dir = Direction.Down;
                loc = (loc.X, loc.Y + 1);
                break;
            case Direction.Down:
                dir = Direction.Left;
                loc = (loc.X - 1, loc.Y);
                break;
            case Direction.Left:
                dir = Direction.Up;
                loc = (loc.X, loc.Y - 1);
                break;
        }
    }

    return (dir, loc);
}

int partOneSum = 0;
//int partTwoSum = 0;

if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}

var (map, loc) = readFile(args[0]);
var dim = map.GetLength(0);
var dir = Direction.Up;
do {
    (dir, loc) = advancePosition(map, loc, dir);
    partOneSum++;
} while (loc.X >= 0 && loc.Y >= 0 &&
         loc.X < dim && loc.Y < dim);

Console.WriteLine($"Part one: {partOneSum}");

enum Direction {
    Up,
    Down,
    Left,
    Right
}