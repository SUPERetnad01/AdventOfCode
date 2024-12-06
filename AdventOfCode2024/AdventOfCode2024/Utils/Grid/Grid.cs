namespace AdventOfCode2024.Utils.Grid;

public class Grid<T>
{
	public List<Cell<T>> Cells { get; set; } = [];

	public Grid(List<List<T>> grid)
	{
		for (int y = 0; y < grid.Count; y++)
		{
			for (var x = 0; x < grid.First().Count; x++)
			{
				var cell = new Cell<T>()
				{
					Coordinate = new Coordinate
					{
						Y = y,
						X = x
					},
					Value = grid[y][x]
				};
				Cells.Add(cell);
			}
		}
	}

	public Cell<T>? GetCellByCoordinate(Coordinate cords)
	{
		var cell = Cells.FirstOrDefault(_ => _.Coordinate.X == cords.X && _.Coordinate.Y == cords.Y);
		return cell;
	}

	public List<(Cell<T>, DIRECTION)>? GetCellsForEachDirection(Cell<T> cell)
	{
		var x = (DIRECTION[])Enum.GetValues(typeof(DIRECTION));
		var cells = x.Select(_ => (GetCellBasedOnDirection(cell, _), _))
			.Where(_ => _.Item1 != null)
			.ToList();
		return cells;
	}

	public Cell<T>? GetCellBasedOnDirection(Cell<T> cell, DIRECTION direction)
	{
		var coordinate = cell.Coordinate.GetCellBasedOnDirection(direction);
		var cellBasedOnDirection = Cells.FirstOrDefault(_ => _.Coordinate.X == coordinate.X && _.Coordinate.Y == coordinate.Y);
		return cellBasedOnDirection;
	}

	public List<Cell<T>> GetAllCellsInSpecificDirection(Cell<T> cell, DIRECTION direction)
	{
		var cells = direction switch
		{
			DIRECTION.NORTH => Cells.Where(_ => _.Coordinate.X == cell.Coordinate.X && _.Coordinate.Y < cell.Coordinate.Y),
			DIRECTION.SOUTH => Cells.Where(_ => _.Coordinate.X == cell.Coordinate.X && _.Coordinate.Y > cell.Coordinate.Y),
			DIRECTION.EAST => Cells.Where(_ => _.Coordinate.X > cell.Coordinate.X && _.Coordinate.Y == cell.Coordinate.Y),
			DIRECTION.WEST => Cells.Where(_ => _.Coordinate.X < cell.Coordinate.X && _.Coordinate.Y == cell.Coordinate.Y),
			_ => throw new NotImplementedException(),
		};

		return cells.ToList();
	}

	public void PrintGrid() 
	{
		int maxX = Cells.Max(c => c.Coordinate.X);
		int maxY = Cells.Max(c => c.Coordinate.Y);

		T[,] grid = new T[maxY + 1, maxX + 1];

		foreach (var cell in Cells)
		{
			grid[cell.Coordinate.Y, cell.Coordinate.X] = cell.Value;
		}

		for (int y = 0; y <= maxY; y++)
		{
			for (int x = 0; x <= maxX; x++)
			{
				Console.Write(grid[y, x]?.ToString() ?? " " + "\t");
			}
			Console.WriteLine();
		}
	}
}

public enum DIRECTION
{
	NORTH,
	SOUTH,
	EAST,
	WEST,
	NORTHEAST,
	NORTHWEST,
	SOUTHWEST,
	SOUTHEAST
}
public static class DirectionHelper {

	public static bool IsOppisiteCorner(this DIRECTION direction,DIRECTION possibleNeighbour) 
	{
		var isOtherCorner = direction switch
		{
			DIRECTION.NORTHWEST => possibleNeighbour == DIRECTION.SOUTHEAST,
			DIRECTION.NORTHEAST => possibleNeighbour == DIRECTION.SOUTHWEST,
			DIRECTION.SOUTHEAST => possibleNeighbour == DIRECTION.NORTHWEST,
			DIRECTION.SOUTHWEST => possibleNeighbour == DIRECTION.NORTHEAST,
		
			_ => throw new NotImplementedException(),
		};
		return isOtherCorner;
	}

	public static List<DIRECTION> Corners { get {
			return new List<DIRECTION>() { DIRECTION.NORTHEAST, DIRECTION.NORTHWEST, DIRECTION.SOUTHEAST, DIRECTION.SOUTHWEST };

		}
	}
}