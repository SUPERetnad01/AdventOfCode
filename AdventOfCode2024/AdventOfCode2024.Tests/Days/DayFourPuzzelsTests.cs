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
		}.Select(_ => _.ToList())
		.ToList();

		var grid = new Grid<char>(crosswordPuzzels);
		var x = grid.Cells
			.Where(_ => _.Value == 'X')
			.Select(_ => grid.GetCellsForEachDirection(_))
			.ToList();


		foreach (var l in x)
		{
			var isXmas = l.Where(_ => _.Item1.Value == 'M');
			var p = isXmas.Where(_ => grid.GetCellBasedOnDirection(_.Item1, _.Item2)?.Value == 'A');
			var r = p.Where(_ => grid.GetCellBasedOnDirection(_.Item1, _.Item2)?.Value == 'S');

		}





		//var awnser = DayFourPuzzels.PartOne(crosswordPuzzels);

		Assert.Equal(18, 1);
	}

	[Fact]
	public void PartTwo()
	{
	}
}
