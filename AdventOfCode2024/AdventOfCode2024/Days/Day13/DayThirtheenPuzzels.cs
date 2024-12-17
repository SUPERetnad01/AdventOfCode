using AdventOfCode2024.Utils;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day13;

public static class DayThirtheenPuzzels
{
	public static void HandlePuzzels() 
	{
		var fullInput = File.ReadAllText(ReadInputFile.GetPathToInput(13));
		var splitInput = Regex.Split(fullInput, "\r\n\r\n");

		var input = splitInput.Select(_ =>
		{
			var x = Regex.Matches(_, @"\d+");
			return (
				ax: long.Parse(x[0].Value), 
				ay: long.Parse(x[1].Value), 
				bx:	long.Parse(x[2].Value), 
				by: long.Parse(x[3].Value), 
				px: long.Parse(x[4].Value), 
				py: long.Parse(x[5].Value)
			);

		});

		var resultOne = PartOne(input);
		var resultTwo = PartTwo(input);
		Console.WriteLine($"Day 13 part one: {resultOne}");
		Console.WriteLine($"Day 13 part two: {resultTwo}");
	}

	public static long PartOne(IEnumerable<(long ax, long ay, long bx, long by, long px, long py)> inputs)
	{

		var totalTokens = inputs.Select(input =>
		{
			double amountOfTokensForButtonA = (input.px * input.by - input.py * input.bx) / (input.ax * input.by - input.ay * input.bx);

			double amountOfTokensForButtonB = (input.px - input.ax * amountOfTokensForButtonA) / input.bx;

			if (
				amountOfTokensForButtonA % 1 == 0 && 
				amountOfTokensForButtonB % 1 == 0 &&
				amountOfTokensForButtonB < 100 &&
				amountOfTokensForButtonA < 100 
			)
			{
				return (long)(amountOfTokensForButtonA * 3 + amountOfTokensForButtonB);
			}

			return 0;
		}).Sum();

		return totalTokens;
	}

	public static long PartTwo(IEnumerable<(long ax, long ay, long bx, long by, long px, long py)> inputs)
	{
		long total = 0;
		
		foreach(var input in inputs)
		{
			var py = input.py + 10000000000000;
			var px = input.px + 10000000000000;

			double amountOfTokensForButtonA = (py * (double)input.ax - px * (double)input.ay) / (input.by * (double)input.ax - input.bx * (double)input.ay);
			double amountOfTokensForButtonB = (px - amountOfTokensForButtonA * input.bx) / input.ax;

			if (amountOfTokensForButtonA % 1 != 0 || amountOfTokensForButtonB % 1 != 0)
			{
				continue;
			}

			total += (long)amountOfTokensForButtonA * 3 + (long)amountOfTokensForButtonB;
		}

		return total;
	}
}
