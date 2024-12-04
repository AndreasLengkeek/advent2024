var search = ReadInput("./input.txt");

var xMAS = 0;
var XMAS = 0;
for (var r = 0; r < search.Count(); r++)
{
    var row = search[r];
    for (var c = 0; c < row.Count(); c++)
    {
        var ch = row[c];
        if (ch == 'A')
        {
            // find adjacent X-MAS
            var adjNum = FindAdjacentMAS(search, r, c);
            Console.WriteLine("finding for ({0},{1})\t Found {2}", r+1, c+1, adjNum);
            xMAS += adjNum;
        }
        if (ch == 'X')
        {
            // find adjacent X-MAS
            var adjNum = FindAdjacentXMAS(search, r, c);
            Console.WriteLine("finding for ({0},{1})\t Found {2}", r+1, c+1, adjNum);
            XMAS += adjNum;
        }
    }
}

Console.WriteLine("Total XMAS: {0}", XMAS);
Console.WriteLine("Total X-MAS: {0}", xMAS);


static int FindAdjacentMAS(char[][] search, int r, int c)
{
    const string TERM = "MAS";
    var count = 0;

    var height = search.Count();
    var width = search[r].Count();

    if (r > 0 && c > 0 && r < height-1 && c < width-1) {
        var s1 = $"{search[r-1][c-1]}{search[r][c]}{search[r+1][c+1]}";
        var s2 = Reverse(s1);
        var s3 = $"{search[r-1][c+1]}{search[r][c]}{search[r+1][c-1]}";
        var s4 = Reverse(s3);
        if (
            (s1 == TERM || s2 == TERM) && (s3 == TERM || s4 == TERM)
        ) {
            count++;
        }
    }

    return count;
}

static string Reverse( string s )
{
    char[] charArray = s.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

static int FindAdjacentXMAS(char[][] search, int r, int c)
{
    const string TERM = "XMAS";
    var count = 0;

    // check 8 directions
    // east
    if (c+3 < search[r].Count() )
    {
        var east = $"{search[r][c]}{search[r][c+1]}{search[r][c+2]}{search[r][c+3]}";
        Console.WriteLine($"\tEast: {east}");
        if ($"{search[r][c]}{search[r][c+1]}{search[r][c+2]}{search[r][c+3]}" == "XMAS")count++;
    }
    // west
    if (c-3 >= 0) {
        var west = $"{search[r][c]}{search[r][c-1]}{search[r][c-2]}{search[r][c-3]}";
        Console.WriteLine($"\twest: {west}");
        if ($"{search[r][c]}{search[r][c-1]}{search[r][c-2]}{search[r][c-3]}" == TERM) count++;
    }
    // north
    if (r-3 >= 0) {
        var north = $"{search[r][c]}{search[r-1][c]}{search[r-2][c]}{search[r-3][c]}";
        Console.WriteLine($"\tnorth: {north}");
        if ($"{search[r][c]}{search[r-1][c]}{search[r-2][c]}{search[r-3][c]}" == TERM) count++;
    }
    // south
    if (r+3 < search.Count()) {
        var south = $"{search[r][c]}{search[r+1][c]}{search[r+2][c]}{search[r+3][c]}";
        Console.WriteLine($"\tsouth: {south}");
        if ($"{search[r][c]}{search[r+1][c]}{search[r+2][c]}{search[r+3][c]}" == TERM) count++;
    }

    // north-east
    if (c+3 < search[r].Count() && r-3 >= 0) {
        var northEast = $"{search[r][c]}{search[r-1][c+1]}{search[r-2][c+2]}{search[r-3][c+3]}";
        Console.WriteLine($"\tnorthEast: {northEast}");
        if ($"{search[r][c]}{search[r-1][c+1]}{search[r-2][c+2]}{search[r-3][c+3]}" == TERM) count++;
    }
    // north-west
    if (c-3 >= 0 && r-3 >= 0) {
        var northWest = $"{search[r][c]}{search[r-1][c-1]}{search[r-2][c-2]}{search[r-3][c-3]}";
        Console.WriteLine($"\tnorthWest: {northWest}");
        if ($"{search[r][c]}{search[r-1][c-1]}{search[r-2][c-2]}{search[r-3][c-3]}" == TERM) count++;
    }
    // south-east
    if (c+3 < search[r].Count() && r+3 < search.Count()){
        var southEast = $"{search[r][c]}{search[r+1][c+1]}{search[r+2][c+2]}{search[r+3][c+3]}";
        Console.WriteLine($"\tsouthEast: {southEast}");
        if ($"{search[r][c]}{search[r+1][c+1]}{search[r+2][c+2]}{search[r+3][c+3]}" == TERM) count++;
    }
    // south-west
    if (c-3 >= 0 && r+3 < search.Count()) {
        var southWest = $"{search[r][c]}{search[r+1][c-1]}{search[r+2][c-2]}{search[r+3][c-3]}";
        Console.WriteLine($"\tsouthWest: {southWest}");
        if ($"{search[r][c]}{search[r+1][c-1]}{search[r+2][c-2]}{search[r+3][c-3]}" == TERM) count++;
    };

    return count;
}


static char[][] ReadInput(string filename)
{
    List<char[]> input = [];
    String? line;
    try
    {
        StreamReader sr = new StreamReader(filename);
        line = sr.ReadLine();
        while (line != null)
        {
            var report = line.ToCharArray();
            input.Add(report);
            line = sr.ReadLine();
        }

        sr.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }

    return input.ToArray();
}