using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;

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

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var resultPartOne = PartOne(coordinates, 70, 1024);
		stopwatch.Stop();
		Console.WriteLine($"Day 18 part One: {resultPartOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var resultPartTwo = PartTwo(coordinates, 70);
		stopwatch.Stop();
		Console.WriteLine($"Day 18 part Two: {resultPartTwo.X},{resultPartTwo.Y}, {stopwatch.ElapsedMilliseconds} ms");

	}

	public int PartOne(List<Coordinate> fallenbites, int size, int bytesToTake)
	{
		var CordList = Enumerable.Repeat(Enumerable.Repeat('.', size + 1).ToList(), size + 1).ToList();
		var grid = new Grid<char>(CordList);
		var amountOfBytesToTake = fallenbites.Take(bytesToTake);
		var blockedCells = grid.Cells
			.Where(_ => amountOfBytesToTake.Contains(_.Coordinate));

		foreach (var blockedCell in blockedCells)
		{
			blockedCell.Value = '#';
		}

		var start = grid.GetCellByCoordinate(new() { X = 0, Y = 0 });
		var end = grid.GetCellByCoordinate(new() { X = size, Y = size });

		var shortestPath = FindShortestPath(grid, start, end);

		return shortestPath;
	}

	public Coordinate PartTwo(List<Coordinate> fallenBytes, int size)
	{
		var CordList = Enumerable.Repeat(Enumerable.Repeat('.', size + 1).ToList(), size + 1).ToList();
		var grid = new Grid<char>(CordList);

		var start = grid.GetCellByCoordinate(new() { X = 0, Y = 0 });
		var end = grid.GetCellByCoordinate(new() { X = size, Y = size });


		var blockedCells = grid.Cells
			.Where(_ => fallenBytes.Contains(_.Coordinate));

		foreach (var blockedCell in blockedCells)
		{
			blockedCell.Value = '#';
		}

		for (int i = 0; i < fallenBytes.Count; i++)
		{
			var currentCellToDrop = fallenBytes.Count - i - 1;
			var blockingSellToRemove = fallenBytes[currentCellToDrop];
			var blockedCell = grid.Cells.FirstOrDefault(_ => _.Coordinate == blockingSellToRemove);

			blockedCell.Value = '.';


			var shortestPath = FindShortestPath(grid, start, end);

			if (shortestPath != -1)
			{
				return fallenBytes[currentCellToDrop];
			}

		}

		return new Coordinate() { X = -1, Y = -1 };
	}


	public int FindShortestPath(Grid<char> grid, Cell<char> startingPosition, Cell<char>? target)
	{
		var priortyQueue = new PriorityQueue<Cell<char>, int>();
		priortyQueue.Enqueue(startingPosition, 0);

		var distances = new Dictionary<Cell<char>, int>
		{
			[startingPosition] = 0
		};

		var seen = new HashSet<Cell<char>>();

		while (priortyQueue.Count > 0)
		{
			priortyQueue.TryDequeue(out var cell, out int score);

			if (cell == target)
			{
				return score;
			}

			if (seen.Contains(cell))
			{
				continue;
			}

			seen.Add(cell);

			foreach (var neighbor in grid.GetCellsForEachDirection(cell))
			{
				if (neighbor.cell == null || neighbor.cell.Value == '#' || !DirectionHelper.UpDownLeftRight.Contains(neighbor.direction))
				{
					continue;
				}

				var newScore = score + 1;

				if (!distances.TryGetValue(neighbor.cell, out var currentDistance) || newScore < currentDistance)
				{
					distances[neighbor.cell] = newScore;
					priortyQueue.Enqueue(neighbor.cell, newScore);
				}
			}
		}

		return -1;
	}


}
