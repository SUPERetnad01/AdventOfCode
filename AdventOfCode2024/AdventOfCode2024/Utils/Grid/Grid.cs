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