namespace AdventOfCode2024.Utils.Grid;

public class Cell<T>
{
	public Coordinate Coordinate { get; set; }
	public T Value { get; set; }

}

public class Coordinate
{
	public int Y { get; set; }
	public int X { get; set; }


	public Coordinate GetCellBasedOnDirection(DIRECTION direction)
	{
		var cord = direction switch
		{
			DIRECTION.NORTH => new Coordinate() { Y = Y - 1, X = X },
			DIRECTION.SOUTH => new Coordinate() { Y = Y + 1, X = X },
			DIRECTION.EAST => new Coordinate() { Y = Y, X = X + 1 },
			DIRECTION.WEST => new Coordinate() { Y = Y, X = X - 1 },
			DIRECTION.NORTHEAST => new Coordinate() { Y = Y - 1, X = X + 1 },
			DIRECTION.NORTHWEST => new Coordinate() { Y = Y - 1, X = X - 1 },
			DIRECTION.SOUTHWEST => new Coordinate() { Y = Y + 1, X = X - 1 },
			DIRECTION.SOUTHEAST => new Coordinate() { Y = Y + 1, X = X + 1 },
			_ => throw new NotImplementedException(),
		};

		return cord;
	}
}
