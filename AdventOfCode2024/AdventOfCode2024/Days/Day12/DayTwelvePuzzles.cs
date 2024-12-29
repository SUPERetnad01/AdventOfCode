using AdventOfCode2024.Days.Day21;
using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System;

namespace AdventOfCode2024.Days.Day12;

public class DayTwelvePuzzles
{
	public void HandlePuzzles()
	{
		var rawGrid = ReadInputFile.GetGridChar(ReadInputFile.GetPathToInput(12));

		var grid = new Grid<char>(rawGrid);
		var partOne = PartOne(grid);

		Console.WriteLine($"Day 12 part one: {partOne}");

		var parttwo = PartTwo(grid);

		Console.WriteLine($"Day 12 part one: {parttwo}");

	}


	public int PartOne(Grid<char> grid)
	{
		var totalPrice = 0;

		var seenCells = new HashSet<Coordinate>();
		foreach (var cell in grid.Cells)
		{
			if (seenCells.Contains(cell.Coordinate))
			{
				continue;
			}

			var (seenCoords, seenEdges) = FindAmountOfEges(cell, grid);
			seenCells.UnionWith(seenCoords);
			totalPrice += seenCoords.Count() * seenEdges;
		}

		return totalPrice;
	}

	public int PartTwo(Grid<char> grid)
	{
		var totalPrice = 0;

		var seenCells = new HashSet<Coordinate>();
		foreach (var cell in grid.Cells)
		{
			if (seenCells.Contains(cell.Coordinate))
			{
				continue;
			}

			var (seenCoords, totalSides) = FindDiscountPrice(cell, grid);
			seenCells.UnionWith(seenCoords);
			totalPrice += seenCoords.Count * totalSides;
		}

		return totalPrice;
	}

	private (HashSet<Coordinate> seenCoords, int seenEdges) FindAmountOfEges(Cell<char> sartingPoint, Grid<char> grid)
	{
		var seenEdges = 0;

		var seenCoords = new HashSet<Coordinate>() {
			sartingPoint.Coordinate
		};

		var queue = new Queue<Coordinate>();

		queue.Enqueue(sartingPoint.Coordinate);

		while (queue.Count > 0)
		{
			var current = queue.Dequeue();

			foreach (var direction in DirectionHelper.UpDownLeftRight)
			{
				var neighbouringCell = grid.GetCellBasedOnDirection(current, direction);

				if (neighbouringCell == null || neighbouringCell.Value != sartingPoint.Value)
				{
					seenEdges++;
					continue;
				}

				if (seenCoords.Contains(neighbouringCell.Coordinate))
				{
					continue;
				}

				seenCoords.Add(neighbouringCell.Coordinate);
				queue.Enqueue(neighbouringCell.Coordinate);
			}
		}


		return (seenCoords, seenEdges);
	}


	private (HashSet<Coordinate> seenCoords, int totalSides) FindDiscountPrice(Cell<char> sartingPoint, Grid<char> grid)
	{
		var seenEdges = 0;

		var region = new HashSet<Coordinate>() {
			sartingPoint.Coordinate
		};

		var queue = new Queue<Coordinate>();

		queue.Enqueue(sartingPoint.Coordinate);

		while (queue.Count > 0)
		{
			var current = queue.Dequeue();

			foreach (var direction in DirectionHelper.UpDownLeftRight)
			{
				var neighbouringCell = grid.GetCellBasedOnDirection(current, direction);

				if (neighbouringCell == null || neighbouringCell.Value != sartingPoint.Value)
				{
					continue;
				}

				if (region.Contains(neighbouringCell.Coordinate))
				{
					continue;
				}

				region.Add(neighbouringCell.Coordinate);
				queue.Enqueue(neighbouringCell.Coordinate);
			}
		}


		var cornerCanidates = new HashSet<(double, double)>();

		foreach (var cell in region)
		{
			var coornerCoords = new List<(double, double)>() {
				(cell.Y - 0.5, cell.X - 0.5),
				(cell.Y + 0.5, cell.X - 0.5),
				(cell.Y + 0.5, cell.X + 0.5),
				(cell.Y - 0.5, cell.X + 0.5),
			};
			foreach (var corenerCoord in coornerCoords)
			{
				cornerCanidates.Add(corenerCoord);
			}
		}

		var corners = 0;
		foreach (var (canY,canX) in cornerCanidates)
		{
			var cornercoords = new List<(double, double)>() {
				(canY - 0.5, canX - 0.5),
				(canY + 0.5, canX - 0.5),
				(canY + 0.5, canX + 0.5),
				(canY - 0.5, canX + 0.5),
			};

			var x = cornercoords.Select(cornercord =>
			{
				var result = region.Any(_ => _ == new Coordinate()
				{
					Y = (int)cornercord.Item1,
					X = (int)cornercord.Item2,

				});
				return result;
			}
				 
			).ToList();

			var amountOf = x.Where(_ => _).Count();

			if(amountOf == 1 || amountOf == 3)
			{
				corners++;
			}
			
			if(amountOf == 2)
			{
				if (x.SequenceEqual([true,false,true,false]) || x.SequenceEqual([false, true, false, true]))
				{
					corners += 2;
				}
			}
		}





		return (region, corners);
	}

}
