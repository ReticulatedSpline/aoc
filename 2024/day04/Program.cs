static char[,] readFile(string filePath) {
    using (StreamReader sr = new StreamReader(filePath)) {
        string? line;
        line = sr.ReadLine();
        if (line is null) {
            return new char[0,0];
        }

        int index = 0;
        char[,] matrix = new char[line.Length,line.Length];
        while (line != null)
        {
            for (int i = 0; i < line.Length; i++) {
                matrix[index, i] = line[i];
            }
            line = sr.ReadLine();
            index++;
        }
        return matrix;
    }
}

bool isXmas(char[,] matrix, int row, int column, Direction direction) {
    int dim = matrix.GetLength(0);
    switch (direction) {
        case Direction.Up:
            return column >= 3 ? 
                    (matrix[row, column-1] == 'M' && matrix[row, column-2] == 'A' && matrix[row, column-3] == 'S')
                    : false;
        case Direction.Down:
            return column < dim - 3 ?
                    (matrix[row, column+1] == 'M' && matrix[row, column+2] == 'A' && matrix[row, column+3] == 'S')
                    : false;
        case Direction.Left:
            return row >= 3 ?
                    (matrix[row-1, column] == 'M' && matrix[row-2, column] == 'A' && matrix[row-3, column] == 'S')
                    : false;
        case Direction.Right:
            return (row < dim - 3) ?
                    (matrix[row+1, column] == 'M' && matrix[row+2, column] == 'A' && matrix[row+3, column] == 'S')
                    : false;
        case Direction.UpRight:
            return column >= 3 && row < dim - 3 ?
                        (matrix[row+1, column-1] == 'M' && matrix[row+2, column-2] == 'A' && matrix[row+3, column-3] == 'S')
                    : false;
        case Direction.DownRight:
            return column < dim - 3 && row < dim - 3 ?
                    (matrix[row+1, column+1] == 'M' && matrix[row+2, column+2] == 'A' && matrix[row+3, column+3] == 'S')
                    : false;
        case Direction.UpLeft:
            return row >= 3 && column >= 3 ?
                    (matrix[row-1, column-1] == 'M' && matrix[row-2, column-2] == 'A' && matrix[row-3, column-3] == 'S')
                    : false;
        case Direction.DownLeft:
            return row >= 3 && column < dim - 3?
                    (matrix[row-1, column+1] == 'M' && matrix[row-2, column+2] == 'A' && matrix[row-3, column+3] == 'S')
                    : false;
        default:
            return false;
    }
}

int findMasX(char[,] matrix, int row, int column) {
    int results = 0;
    int dim = matrix.GetLength(0);
    
    if (row < 1 || row >= dim - 1 ||
        column < 1 || column >= dim - 1) {
        return 0;
    }

    if ((matrix[row+1, column-1] == 'M' && matrix[row-1, column+1] == 'S' ||
         matrix[row+1, column-1] == 'S' && matrix[row-1, column+1] == 'M') &&
        (matrix[row-1, column-1] == 'M' && matrix[row+1, column+1] == 'S' ||
         matrix[row-1, column-1] == 'S' && matrix[row+1, column+1] == 'M'))
            results++;

    return results;
}

int findTargets(char[,] matrix, int row, int column) {
    int targetsFound = 0;
    foreach(Direction direction in Enum.GetValues(typeof(Direction))) {
            if (isXmas(matrix, row, column, direction)) targetsFound++;
    }

    return targetsFound;
}

int partOneSum = 0;
int partTwoSum = 0;
if (args.Length == 0) {
    Console.WriteLine("Missing input file argument!");
    return;
}
var matrix = readFile(args[0]);
int dimension = matrix.GetLength(0);

for (int row = 0; row < dimension; row++) {
    for (int column = 0; column < dimension; column++) {
        if (matrix[row, column] == 'X') {
            partOneSum += findTargets(matrix, row, column);
        }
        if (matrix[row, column] == 'A') {
            partTwoSum += findMasX(matrix, row, column);
        }
    }
}

// 2517
Console.WriteLine($"Part One: {partOneSum}");
Console.WriteLine($"Part Two: {partTwoSum}");

enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpRight,
    DownRight,
    UpLeft,
    DownLeft
}