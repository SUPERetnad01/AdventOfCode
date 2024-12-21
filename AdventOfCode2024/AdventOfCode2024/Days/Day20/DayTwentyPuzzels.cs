using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;
using System.Diagnostics;

namespace AdventOfCode2024.Days.Day20;

public class DayTwentyPuzzels
{
	public static void HandlePuzzels()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(20));
		var grid = new Grid<char>(rawGrid);

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var result = PartOne(grid, 100);
		stopwatch.Stop();
		Console.WriteLine($"Day 20 part one: {result} time ms: {stopwatch.ElapsedMilliseconds}");

		stopwatch.Start();
		var partTwo = PartTwo(grid, 100);
		stopwatch.Stop();
		Console.WriteLine($"Day 20 part two: {partTwo} time ms: {stopwatch.ElapsedMilliseconds}");
	}

	public static int PartOne(Grid<char> grid,int minimumTimedSaved)
	{
		var startingPoint = grid.Cells.FirstOrDefault(_ => _.Value == 'S');
		var endingPoint = grid.Cells.FirstOrDefault(_ => _.Value == 'E');

		var path = GetPath(startingPoint.Coordinate, grid);

		var seenCheats = new HashSet<(Coordinate,Coordinate)>();

		foreach(var point in path)
		{
			var InRange = path.Where(
				_ => {
					var distances = point.Key.ManhatanDistance(_.Key);

					var timeSaved = Math.Abs(_.Value - point.Value);

					return distances == 2 && point.Value < _.Value && timeSaved >= minimumTimedSaved + 2; 
				});


			foreach(var pointInRange in InRange) {
				seenCheats.Add((point.Key, pointInRange.Key));
			}
		}
		
		return seenCheats.Count();
	}

	public static int PartTwo(Grid<char> grid, int minimumTimedSaved)
	{
		var startingPoint = grid.Cells.FirstOrDefault(_ => _.Value == 'S');
		var endingPoint = grid.Cells.FirstOrDefault(_ => _.Value == 'E');

		var path = GetPath(startingPoint.Coordinate,grid);

		var seenCheats = new HashSet<(Coordinate, Coordinate)>();

		foreach (var point in path)
		{
			var inRange = path.Where(
				_ =>
				{
					var distance = point.Key.ManhatanDistance(_.Key);

					var timeSaved = Math.Abs(_.Value - point.Value);

					var isInDistanceRange = distance >= 2 && distance <= 20;

					return isInDistanceRange &&
					timeSaved - distance >= minimumTimedSaved &&
					point.Value > _.Value;
				});


			if (inRange.Any())
			{
				var containsEndPoint = inRange.FirstOrDefault(_ => _.Key == endingPoint.Coordinate);
				if (containsEndPoint.Key != null)
				{
					seenCheats.Add((point.Key, containsEndPoint.Key));
					continue;
				}
			}

			foreach (var pointInRange in inRange)
			{
				seenCheats.Add((point.Key, pointInRange.Key));
			}
		}

		return seenCheats
			.Distinct()
			.Count();
	}

	public static Dictionary<Coordinate,int> GetPath(Coordinate startingPoint,Grid<char> grid)
	{
		var seenPath = new Dictionary<Coordinate, int>
		{
			{ startingPoint, 0 }
		};

		var que = new Queue<Coordinate>();
		que.Enqueue(startingPoint);


		var score = 1;

		while (que.Count > 0)
		{
			var current = que.Dequeue();

			foreach (var direction in DirectionHelper.UpDownLeftRight)
			{

				var neighbouringCell = grid.GetCellBasedOnDirection(current, direction);

				if (
					neighbouringCell.Value == '#' ||
					seenPath.ContainsKey(neighbouringCell.Coordinate)
				)
				{
					continue;
				}
				que.Enqueue(neighbouringCell.Coordinate);
				seenPath[neighbouringCell.Coordinate] = score++;
			}
		}

		return seenPath;
	}
}
