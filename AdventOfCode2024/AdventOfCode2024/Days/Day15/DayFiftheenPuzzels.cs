using AdventOfCode2024.Utils;
using AdventOfCode2024.Utils.Grid;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days.Day15;

public class DayFiftheenPuzzels
{
	public void HandlePuzzels() 
	{
		var input = File.ReadAllText(ReadInputFile.GetPathToInput(15));

		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var resultOne = PartOne(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 15 part one: {resultOne}, {stopwatch.ElapsedMilliseconds} ms");

		stopwatch.Restart();
		var resultTwo = PartTwo(input);
		stopwatch.Stop();
		Console.WriteLine($"Day 15 part two: {resultTwo}, {stopwatch.ElapsedMilliseconds} ms");
	}

	public int PartOne(string input)
	{
		var splitGridAndMovements = Regex
			.Split(input, "\r\n\r\n");

		var gridString = Regex.Split(splitGridAndMovements[0],"\r\n")
			.Select(_ => 
				_.Select(_ => _)
				.ToList()
			).ToList();

		var grid = new Grid<char>(gridString);
		var moves = splitGridAndMovements[1].Replace("\r\n", string.Empty);

		var currentPos = grid.Cells.FirstOrDefault(_ => _.Value == '@');

		foreach (var move in moves)
		{

			var direction = ConvertToDirection(move);

			var possibleMove = grid.GetCellBasedOnDirection(currentPos, direction);

			if(possibleMove.Value == '.')
			{
				currentPos.Value = '.';
				currentPos = possibleMove;
				currentPos.Value = '@';
			}

			if(possibleMove.Value == '#')
			{
				continue;
			}

			if(possibleMove.Value == 'O')
			{
				var canMoveBox = CanMoveBox(direction, possibleMove, grid);
				if (canMoveBox)
				{
					currentPos.Value = '.';
					currentPos = possibleMove;
					currentPos.Value = '@';
				}
			}
		}

		var gpsTotal = grid.Cells
			.Where(_ => _.Value == 'O')
			.Select(_ => _.Coordinate.X + _.Coordinate.Y * 100)
			.Sum();

		return gpsTotal;
	}

	public int PartTwo(string input)
	{
		var splitGridAndMovements = Regex
			.Split(input, "\r\n\r\n");

		var gridstring = Regex.Split(splitGridAndMovements[0], "\r\n")
			.Select(_ =>
			{
				var enhancedString = _.Replace("#", "##")
					.Replace("O", "[]")
					.Replace(".", "..")
					.Replace("@", "@.");

				var s = enhancedString.Select(_ => _).ToList();
				return s;
			}).ToList();

		var grid = new Grid<char>(gridstring);

		var moves = splitGridAndMovements[1].Replace("\r\n", string.Empty);

		var currentPos = grid.Cells.FirstOrDefault(_ => _.Value == '@');

		foreach (var move in moves)
		{
			var direction = ConvertToDirection(move);

			var possibleMove = grid.GetCellBasedOnDirection(currentPos, direction);

			if (possibleMove.Value == '.')
			{
				currentPos.Value = '.';
				currentPos = possibleMove;
				currentPos.Value = '@';
			}

			if (possibleMove.Value == '#')
			{
				continue;
			}

			if (possibleMove.Value == '[' || possibleMove.Value == ']')
			{

				if(CanMoveLargeBoxHorizontal(direction,possibleMove,grid))
				{
					currentPos.Value = '.';
					currentPos = possibleMove;
					currentPos.Value = '@';
					continue;
				}
				var posibleBoxes = CanMoveLargeBoxVerticale(direction, possibleMove, grid);

				if (posibleBoxes.Count > 1)
				{
					var newPosOfBoxes = new List<(Coordinate,char)>();

					foreach(var box in posibleBoxes)
					{
				
						var newPos = box.Coordinate.GetCellBasedOnDirection(direction);
						newPosOfBoxes.Add((newPos, box.Value));

					}

					foreach(var b in posibleBoxes)
					{
						var gr = grid.GetCellByCoordinate(b.Coordinate);
						gr.Value = '.';
					}

	
					foreach (var (coord,val) in newPosOfBoxes)
					{
						grid.GetCellByCoordinate(coord).Value = val;
					}

					currentPos.Value = '.';
					currentPos = possibleMove;
					currentPos.Value = '@';
				}
			}
		}

		var gpsTotal = grid.Cells
			.Where(_ => _.Value == '[')
			.Select(_ => _.Coordinate.X + _.Coordinate.Y * 100)
			.Sum();

		return gpsTotal;

	}

	private DIRECTION ConvertToDirection(char character) => character switch
	{
		'<' => DIRECTION.WEST,
		'^' => DIRECTION.NORTH,
		'>' => DIRECTION.EAST,
		'v' => DIRECTION.SOUTH,
		_ => throw new NotImplementedException()
	};
		

	private bool CanMoveBox(DIRECTION direction,Cell<char> cell,Grid<char> grid)
	{
		var cellInDirection = grid.GetCellBasedOnDirection(cell, direction);

		if(cellInDirection.Value == '#')
		{
			return false;
		}

		if(cellInDirection.Value == '.')
		{
			cellInDirection.Value = 'O';
			return true;
		}

		if(cellInDirection.Value == 'O')
		{
			if (CanMoveBox(direction, cellInDirection, grid))
			{
				cellInDirection.Value = cell.Value;
				return true;
			}
		}


		return false;
	}


	private bool CanMoveLargeBoxHorizontal(DIRECTION direction, Cell<char> cell, Grid<char> grid)
	{
		if(direction != DIRECTION.WEST && direction != DIRECTION.EAST)
		{
			return false;
		}

		var cellInDirection = grid.GetCellBasedOnDirection(cell, direction);

		var nextCell = grid.GetCellBasedOnDirection(cellInDirection, direction);

		var valueToCheck = direction == DIRECTION.WEST ? ']': '[';

        if (nextCell.Value == valueToCheck && CanMoveLargeBoxHorizontal(direction, nextCell, grid))
        {
			nextCell.Value = cellInDirection.Value;
			cellInDirection.Value = cell.Value;
			return true;				
        }

		if(nextCell.Value == '.')
		{
			nextCell.Value = cellInDirection.Value;
			cellInDirection.Value = cell.Value;
			return true;
		}

		if (nextCell.Value == '#')
		{
			return false;
		}

		return false;
	}


	private List<Cell<char>> CanMoveLargeBoxVerticale(DIRECTION direction, Cell<char> cell, Grid<char> grid,Cell<char>? previousCell = null)
	{
		if (direction != DIRECTION.NORTH && direction != DIRECTION.SOUTH)
		{
			return [];
		}

		if (cell.Value == '.')
		{
			return [previousCell];
		}
		if(cell.Value == '#')
		{
			return [];
		}


		if (cell.Value == '[' || cell.Value == ']')
		{
			var directionOfOtherBox = cell.Value == ']' ? DIRECTION.WEST : DIRECTION.EAST;

			var getOtherSideOfBox = grid.GetCellBasedOnDirection(cell, directionOfOtherBox);

			var getPositionNextToBoxOtherside = grid.GetCellBasedOnDirection(getOtherSideOfBox, direction);
			var getPositionNextToBox = grid.GetCellBasedOnDirection(cell, direction);

			var nextBox = CanMoveLargeBoxVerticale(direction, getPositionNextToBoxOtherside, grid, getOtherSideOfBox);
			var nextBox2 = CanMoveLargeBoxVerticale(direction, getPositionNextToBox, grid, cell);

			if(nextBox.Count() > 0 && nextBox2.Count() > 0)
			{
				//getPositionNextToBox.Value = cell.Value;
				//getPositionNextToBox.Value = getOtherSideOfBox.Value;
				return nextBox2.Union(nextBox).Union([getOtherSideOfBox,cell]).ToList();
			}

			return [];

		}

		return [];
	}
}
