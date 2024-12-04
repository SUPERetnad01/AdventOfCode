namespace AdventOfCode2024.Utils.Grid;

public class Grid<T>
{
	public List<Cell<T>> Cells { get; set; } = [];

	public Grid(List<List<T>> grid)
	{
		for (int x = 0; x < grid.Count; x++)
		{
			for (var y = 0; y < grid.First().Count; y++)
			{
				var cell = new Cell<T>()
				{
					Coordinate = new Coordinate
					{
						X = x,
						Y = y
					},
					Value = grid[x][y]
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