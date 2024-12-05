using System.Text.RegularExpressions;

public class DayThree
{
    
    private string[] _input;

    public DayThree(string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath, nameof(filePath));

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist.", filePath);
        }

        _input = File.ReadAllLines(filePath);

        if (_input == null || _input.Length == 0)
        {
            throw new InvalidOperationException("The input file is empty.");
        }
    }
    public int PartOne()
    {
        /*
            It seems like the goal of the program is just to multiply some numbers. It does that with instructions like `mul(X,Y)`, where X and Y are each 1-3 digit numbers. For instance, `mul(44,46)` multiplies 44 by 46 to get a result of 2024. Similarly, `mul(123,4)` would multiply 123 by 4.

            However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction. Sequences like `mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 )` do nothing.

            For example, consider the following section of corrupted memory:

            `xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))`
            
            Only the four highlighted sections are real mul instructions. Adding up the result of each instruction produces 161 (2*4 + 5*5 + 11*8 + 8*5).

            Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?
        */

        List<int[]> instructions = new List<int[]>();
        int sum = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            string line = _input[i];
            MatchCollection matches = Regex.Matches(line, @"mul\((\d{1,3}),(\d{1,3})\)");

            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                instructions.Add([x, y]);
            }
        }

        sum = instructions.Sum(instruction => Multiply(instruction[0], instruction[1]));



        return sum;
    }

    public int PartTwo()
    {
        /*
            There are two new instructions you'll need to handle:

            The do() instruction enables future mul instructions.
            The don't() instruction disables future mul instructions.
            Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.

            For example:

            xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
            This corrupted memory is similar to the example from before, but this time the mul(5,5) and mul(11,8) instructions are disabled because there is a don't() instruction before them. The other mul instructions function normally, including the one at the end that gets re-enabled by a do() instruction.

            This time, the sum of the results is 48 (2*4 + 8*5).
        */

        List<int[]> instructions = new List<int[]>();
        int sum = 0;
        bool mulEnabled = true;

        for (int i = 0; i < _input.Length; i++)
        {
            string line = _input[i];
            MatchCollection matches = Regex.Matches(line, @"(do\(\)|don't\(\))|mul\((\d{1,3}),(\d{1,3})\)");

            foreach (Match match in matches)
            {
                if (match.Value.Contains("do()"))
                {
                    mulEnabled = true;
                }
                else if (match.Value.Contains("don't()"))
                {
                    mulEnabled = false;
                }
                else if (mulEnabled)
                {
                    int x = int.Parse(match.Groups[2].Value);
                    int y = int.Parse(match.Groups[3].Value);
                    instructions.Add([x, y]);
                }
            }
        }

        sum = instructions.Sum(instruction => Multiply(instruction[0], instruction[1]));

        return sum;
    }

    public int Multiply(int x, int y)
    {
        return x * y;
    }
}