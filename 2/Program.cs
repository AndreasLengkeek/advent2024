var reports = ReadInput("./input.txt");
Console.WriteLine($"Total reports: {reports.Count}");

var safeCount = 0;
foreach (var report in reports)
{
    report.ForEach(x => Console.Write($"{x} "));

    var isSafe = ReportIsSafe(report);
    PrintReportStatus(isSafe);

    if (isSafe)
    {
        safeCount++;
    }
    else
    {
        var isSafeB = false;
        for (int i = 0; i < report.Count; i++)
        {
            var substituteReport = report.Where((item, index) => index != i).Select(x => x).ToList();
            Console.Write('\t');
            substituteReport.ForEach(x => Console.Write($"{x} "));
            isSafeB = ReportIsSafe(substituteReport);
            PrintReportStatus(isSafeB);
            if (isSafeB) break;
        }

        if (isSafeB) safeCount++;
    }
}

Console.WriteLine($"Safe reports: {safeCount}");

static void PrintReportStatus(bool isSafe)
{
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