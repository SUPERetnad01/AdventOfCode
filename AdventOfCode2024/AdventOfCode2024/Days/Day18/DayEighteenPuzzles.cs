using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;

namespace AdventOfCode2024.Days.Day18;

public class DayEighteenPuzzles
{
	public void HandlePuzzels()
	{
		var coordinates = File.ReadLines(ReadInputFile.GetPathToInput(18))
		.Select(_ =>
		{
			var splitcord = _.Split(",");
			return new Coordinate() { X = int.Parse(splitcord[0]), Y = int.Parse(splitcord[1]) };
		}).ToList();

		var resultPartOne = PartOne(coordinates, 70,1024);
		Console.WriteLine($"Day 18 part One: {resultPartOne}");

	}

	public int PartOne(List<Coordinate> fallenbites,int size,int bytesToTake)
	{
		var CordList = Enumerable.Repeat(Enumerable.Repeat('.', size + 1).ToList(),size + 1).ToList();
		var grid = new Grid<char>(CordList);
		var amountOfBytesToTake = fallenbites.Take(bytesToTake);
		var blockedCells = grid.Cells
			.Where(_ => amountOfBytesToTake.Contains(_.Coordinate));

		foreach (var blockedCell in blockedCells)
		{
			blockedCell.Value = '#';
			//grid.PrintGrid();
		}

		var start = grid.GetCellByCoordinate(new() { X = 0, Y = 0 });
		var end = grid.GetCellByCoordinate(new() { X = size , Y = size });

		start.Value = 'S';
		end.Value = 'E';

		grid.PrintGrid();

		var shortestPath = FindShortestPath(grid, start,end );

		return shortestPath;
	}

	public int FindShortestPath(Grid<char> grid, Cell<char> startingPosition, Cell<char>? target)
	{
		var priortyQueue = new PriorityQueue<Cell<char>, int>();
		priortyQueue.Enqueue(startingPosition, 0);

		var seen = new HashSet<Cell<char>>();


		while (priortyQueue.Count > 0)
		{
			var canDeque = priortyQueue.TryDequeue(out var cell, out int score);

			if (cell.Coordinate == target.Coordinate)
			{
				return score;
			}

			seen.Add(cell);

			var getValidNodes = grid.GetCellsForEachDirection(cell)
				.Where(_ =>
					cell != null &&
					_.cell.Value != '#' &&
					DirectionHelper.UpDownLeftRight.Contains(_.direction) &&
					!seen.Contains(_.cell))
				.ToList();


			var distances = getValidNodes
				.Select(_ => (score: score + 1, cellAndDirection: _))
				.ToList();

			foreach (var distance in distances)
			{
				priortyQueue.Enqueue(distance.cellAndDirection.cell, distance.score);
			}

		}



		return -1;
	}

	public int CalculateScoreForPath(Cell<char> currentCell, DIRECTION? currentDirection, Cell<char> cellToConsider, DIRECTION directionbasedOnCurrentPosition)
	{
		if (currentDirection == directionbasedOnCurrentPosition || currentDirection == null)
		{
			return 1;
		}

		return 1;
	}

}
