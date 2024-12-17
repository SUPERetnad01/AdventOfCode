using AdventOfCode2024.Days.Day13;
using AdventOfCode2024.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Tests.Days;

public class Day13PuzzelsTests
{
	[Fact]
	public void PartOne() {

		var fullInput = File.ReadAllText(ReadInputFile.GetPathToTestInput(13));
		var splitInput = Regex.Split(fullInput, "\r\n\r\n");

		var input = splitInput.Select(_ =>
		{
			var x = Regex.Matches(_, @"\d+");
			return (
				ax: long.Parse(x[0].Value),
				ay: long.Parse(x[1].Value),
				bx: long.Parse(x[2].Value),
				by: long.Parse(x[3].Value),
				px: long.Parse(x[4].Value),
				py: long.Parse(x[5].Value)
			);

		});

		var partOne = DayThirtheenPuzzels.PartOne(input);
		Assert.Equal(480, partOne);

	}

	[Fact]
	public void PartTwo()
	{

		var fullInput = File.ReadAllText(ReadInputFile.GetPathToTestInput(13));
		var splitInput = Regex.Split(fullInput, "\r\n\r\n");

		var input = splitInput.Select(_ =>
		{
			var x = Regex.Matches(_, @"\d+");
			return (
				ax: long.Parse(x[0].Value),
				ay: long.Parse(x[1].Value),
				bx: long.Parse(x[2].Value),
				by: long.Parse(x[3].Value),
				px: long.Parse(x[4].Value),
				py: long.Parse(x[5].Value)
			);

		});

		var partOne = DayThirtheenPuzzels.PartTwo(input);
		Assert.Equal(875318608908, partOne);

	}
}
