Console.WriteLine("Reading input >>>");
var x = ReadInput("./input.txt");

// GetDifference(x.l1, x.l2);
GetSimilarity(x.l1, x.l2);


static void GetSimilarity(List<int> l1, List<int> l2)
{
    var rightDict = new Dictionary<int, int>();
    foreach (int l in l2)
    {
        if (rightDict.ContainsKey(l)) rightDict[l] += 1;
        else rightDict.Add(l, 1);
    }
    Console.WriteLine($"Total unique nums: {rightDict.Keys.Count}");

    var sum = 0;
    foreach (int l in l1)
    {
        var count = rightDict.ContainsKey(l) ? rightDict[l] : 0;
        sum += l * count;
    }

    Console.WriteLine($"Similarity score: {sum}");
}
static void GetDifference(List<int> l1, List<int> l2)
{
    var orderedLeft = l1.Order().ToArray();
    var orderedRight = l2.Order().ToArray();

    var sum = 0;
    for (int i = 0; i < orderedLeft.Length; i++)
    {
        sum += Math.Abs(orderedLeft[i] - orderedRight[i]);
    }

    Console.WriteLine($"total diff: {sum}");
}

static (List<int> l1, List<int> l2) ReadInput(string input)
{
    List<int> l1 = new List<int>();
    List<int> l2 = new List<int>();
    String? line;
    try
    {
        StreamReader sr = new StreamReader(input);
        line = sr.ReadLine();
        while (line != null)
        {
            var items = line.Split("  ");
            l1.Add(Int32.Parse(items[0]));
            l2.Add(Int32.Parse(items[1]));

            line = sr.ReadLine();
        }

        sr.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }

    return (l1, l2);
}