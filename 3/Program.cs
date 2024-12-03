using System.Text.RegularExpressions;

var input = File.ReadAllText("./input.txt");

Regex r1 = new Regex(@"(mul\(\d+,\d+\)|do\(\)|don't\(\))", RegexOptions.IgnoreCase);
Regex r2 = new Regex(@"mul\((\d+),(\d+)\)",RegexOptions.IgnoreCase);

var sumEnabled = true;
var sum = 0;
foreach (Match match in r1.Matches(input))
{
    var line = match.Groups[1].Value;

    if (!line.Contains("mul"))
    {
        sumEnabled = line == "do()";
    }
    else if (sumEnabled)
    {
        var vals = r2.Matches(line)[0];
        var output = Int32.Parse(vals.Groups[1].Value)*Int32.Parse(vals.Groups[2].Value);
        Console.WriteLine($"{line}={vals.Groups[1].Value}*{vals.Groups[2].Value}={output}");

        sum += output;
    }


}

Console.WriteLine("Sum: {0}", sum);