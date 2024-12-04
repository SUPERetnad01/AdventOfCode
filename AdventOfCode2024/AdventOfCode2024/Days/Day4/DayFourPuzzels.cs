using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day4;

public static class DayFourPuzzels 
{
	public static void HandleQuestions()
	{
		var grid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(4));

		var awnserOne = PartOne(grid);
		Console.WriteLine($"Day 4 part one: {awnserOne}");

		var awnserTwo = PartTwo(grid);
		Console.WriteLine($"Day 4 part two: {awnserTwo}");
	}

	public static int PartOne(List<List<char>> crosswordPuzzels)
	{
		var grid = new Grid<char>(crosswordPuzzels);

		var amountOfXmas = grid.Cells
			.Where(_ => _.Value == 'X')
			.SelectMany(_ => grid.GetCellsForEachDirection(_)?.Where(_ => _.Item1.Value == 'M'))
			.Where(_ => grid.GetCellBasedOnDirection(_.Item1, _.Item2)?.Value == 'A')
			.Select(_ => (grid.GetCellBasedOnDirection(_.Item1, _.Item2), _.Item2))
			.Where(_ => grid.GetCellBasedOnDirection(_.Item1, _.Item2)?.Value == 'S')
			.Select(_ => (grid.GetCellBasedOnDirection(_.Item1, _.Item2), _.Item2))
			.Count();

		return amountOfXmas;
	}

	public static int PartTwo(List<List<char>> crosswordPuzzels)
	{
		
		var grid = new Grid<char>(crosswordPuzzels);

		var amountOfAss = grid.Cells
			.Where(_ => _.Value == 'A')
			.Select(_ => grid.GetCellsForEachDirection(_)).ToList();

		var amountOfTrueXmas = amountOfAss.Where(CheckTrueXMAS);

		return amountOfTrueXmas.Count();
	}

	private static bool CheckTrueXMAS(List<(Cell<char>, DIRECTION)> _) {
		var corners = DirectionHelper.Corners;
		var allMCorners = _.Where(_ => _.Item1.Value == 'M' && corners.Contains(_.Item2)).ToList();
		var allSCorners = _.Where(_ => _.Item1.Value == 'S' && corners.Contains(_.Item2)).ToList();

		if (allMCorners.Count() != 2 || allSCorners.Count() != 2)
		{
			return false;
		}

		var checkM = DirectionHelper.IsOppisiteCorner(allMCorners.First().Item2, allMCorners[1].Item2);
		var checkS = DirectionHelper.IsOppisiteCorner(allSCorners.First().Item2, allSCorners[1].Item2);

		if (checkM || checkS)
		{
			return false;
		}

		return true;
	}

}
