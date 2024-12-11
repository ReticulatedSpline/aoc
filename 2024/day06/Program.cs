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
    var nextLoc = loc;
    switch (dir) {
        case Direction.Up:
            nextLoc = (loc.X - 1, loc.Y);
            break;
        case Direction.Right:
            nextLoc = (loc.X, loc.Y + 1);
            break;
        case Direction.Down:
            nextLoc = (loc.X + 1, loc.Y);
            break;
        case Direction.Left:
            nextLoc = (loc.X, loc.Y - 1);
            break;
    }

    char nextChar = map[nextLoc.X, nextLoc.Y];

    if (nextChar == '#') {
        switch (dir) {
            case Direction.Up:
                dir = Direction.Right;
                break;
            case Direction.Right:
                dir = Direction.Down;
                break;
            case Direction.Down:
                dir = Direction.Left;
                break;
            case Direction.Left:
                dir = Direction.Up;
                break;
        }
    } else {
        loc = nextLoc;
    }
    return (dir, loc);
}

static uint countPositions(char[,] map) {
    uint count = 0;
    for(int i = 0; i < map.GetLength(0); i++) {
       for(int k = 0; k < map.GetLength(0); k++) {
            if (map[i,k] == 'X') count++;
        }
    }
    return count;
}

if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}

var (map, loc) = readFile(args[0]);
var dir = Direction.Up;

// dont make bully me for this
try {
    do {
        map[loc.X, loc.Y] = 'X';
        (dir, loc) = advancePosition(map, loc, dir);
        map[loc.X, loc.Y] = '^';
    } while (true);
} catch (System.IndexOutOfRangeException) {
    Console.WriteLine($"Part one: {countPositions(map)}");
}

enum Direction {
    Up,
    Down,
    Left,
    Right
}