var reports = ReadInput("./input.txt");
Console.WriteLine($"Total reports: {reports.Count}");

var safeCount = 0;
foreach (var report in reports)
{
    var isSafe = ReportIsSafe(report);

    if (isSafe) safeCount++;
    else
    {
        // run all variants with an error buffer
        var isSafeB = false;
        for (int i = 0; i < report.Count; i++)
        {
            var bReport = report
                .Where((item, index) => index != i)
                .ToList();

            Console.Write('\t');
            isSafeB = ReportIsSafe(bReport);
            if (isSafeB) break;
        }

        if (isSafeB) safeCount++;
    }
}

Console.WriteLine($"Safe reports: {safeCount}");

static void PrintReport(List<int> report, bool isSafe)
{
    report.ForEach(x => Console.Write($"{x} "));

    Console.BackgroundColor = isSafe ? ConsoleColor.Green : ConsoleColor.Red;
    Console.Write(isSafe ? "Safe" : "Unsafe");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();
}

static bool ReportIsSafe(List<int> report)
{
    var isSafe = true;
    var isAsc = report[0] < report[1];
    var prevLevel = 0;
    foreach (var level in report)
    {
        if (prevLevel != 0)
        {
            if (!isAsc && prevLevel > level && ((prevLevel - level) <= 3)) // desc gradual
            {
            }
            else if (isAsc && prevLevel < level && ((level - prevLevel) <= 3)) // asc gradual
            {
            }
            else
            {
                isSafe = false;
            }
        }

        prevLevel = level;
    }
    PrintReport(report, isSafe);
    return isSafe;
}

static List<List<int>> ReadInput(string filename)
{
    List<List<int>> input = new List<List<int>>();
    String? line;
    try
    {
        StreamReader sr = new StreamReader(filename);
        line = sr.ReadLine();
        while (line != null)
        {
            var report = line.Split(' ').Select(Int32.Parse).ToList();
            input.Add(report);
            line = sr.ReadLine();
        }

        sr.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }

    return input;
}