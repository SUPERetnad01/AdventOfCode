using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Days.Day8;

public static class DayEightPuzzels
{

	public static void HandlePuzzels() { 
	
	}

	public static int PartOne(Grid<char> grid) {

		var DiffrentAttenas = grid.Cells
			.Where(_ => _.Value != '.')
			.GroupBy(_ => _.Value);

		return 1;
	}
}
