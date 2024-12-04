using AdventOfCode2024.Days.Day4;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Tests.Days;

public class DayFourPuzzelsTests
{
	[Fact]
	public void PartOne()
	{
		var crosswordPuzzels = new List<string>() {
			"MMMSXXMASM",
			"MSAMXMSMSA",
			"AMXSXMAAMM",
			"MSAMASMSMX",
			"XMASAMXAMM",
			"XXAMMXXAMA",
			"SMSMSASXSS",
			"SAXAMASAAA",
			"MAMMMXMMMM",
			"MXMXAXMASX",
		}
		.Select(_ => _.ToList())
		.ToList();

		var awnser = DayFourPuzzels.PartOne(crosswordPuzzels);

		Assert.Equal(18,awnser);
	}

	[Fact]
	public void PartTwo()
	{
		var crosswordPuzzels = new List<string>() {
			"MMMSXXMASM",
			"MSAMXMSMSA",
			"AMXSXMAAMM",
			"MSAMASMSMX",
			"XMASAMXAMM",
			"XXAMMXXAMA",
			"SMSMSASXSS",
			"SAXAMASAAA",
			"MAMMMXMMMM",
			"MXMXAXMASX",
		}
		.Select(_ => _.ToList())
		.ToList();

		var awnser = DayFourPuzzels.PartTwo(crosswordPuzzels);

		Assert.Equal(9, awnser);

	}
}
