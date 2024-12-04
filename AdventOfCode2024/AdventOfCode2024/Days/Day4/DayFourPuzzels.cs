using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day4;

public static class DayFourPuzzels
{
	public static void HandlePuzzles()
	{

	}

	public static int PartOne(List<List<char>> crosswordPuzzels)
	{

		var x = 0;
		for (int i = 0; i < crosswordPuzzels.Count - 1; i++)
		{
			for (var j = 0; j < crosswordPuzzels[i].Count; j++)
			{
				var am = AmountOfXmas(crosswordPuzzels, i, j);
				x+= am;
			}
		}

		return x;
	}

	private static int AmountOfXmas(List<List<char>> crosswordPuzzles, int rowIndex, int columnIndex)
	{
		var center = crosswordPuzzles[rowIndex][columnIndex];
		if (center != 'X' && center != 'S') {
			return 0;
		}

		var xmasWest = HasXMASWest(crosswordPuzzles, rowIndex, columnIndex, center);
		var xmasNorth = HasXMASNorth(crosswordPuzzles, rowIndex, columnIndex, center);
		var xmasNorthEast = HasXMASNorthWest(crosswordPuzzles, rowIndex, columnIndex, center);
		var xmasSouthEast = HasXMASSouthWest(crosswordPuzzles, rowIndex, columnIndex, center);
	

		return xmasWest + xmasNorth + xmasNorthEast + xmasSouthEast;
	}

	private static int HasXMASSouthWest(List<List<char>> crosswordPuzzles, int rowIndex, int columnIndex, char center)
	{
		var HasAmountOfSpaceLeftNorth = rowIndex >= 3;
		var HasAmountOfSpaceLeftEast = columnIndex >= 3;
		if (!HasAmountOfSpaceLeftEast || !HasAmountOfSpaceLeftNorth)
		{
			return 0;
		}

		var secondLetter = crosswordPuzzles[rowIndex - 1][columnIndex - 1];
		var thirdLetter = crosswordPuzzles[rowIndex - 2][columnIndex - 2];
		var fouthLetter = crosswordPuzzles[rowIndex - 3][columnIndex - 3];



		if (center == 'X' & secondLetter == 'M' && thirdLetter == 'A' && fouthLetter == 'S')
		{
			return 1;
		}

		if (center == 'S' & secondLetter == 'A' && thirdLetter == 'M' && fouthLetter == 'X')
		{
			return 1;
		}

		return 0;
	}

	private static int HasXMASWest(List<List<char>> crosswordPuzzles, int rowIndex, int columnIndex, char center)
	{
		var HasAmountOfSpaceLeft = columnIndex >= 3;
		if (!HasAmountOfSpaceLeft) {
			return 0;
		}

		var secondLetter = crosswordPuzzles[rowIndex][columnIndex - 1];
		var thirdLetter = crosswordPuzzles[rowIndex][columnIndex - 2];
		var fouthLetter = crosswordPuzzles[rowIndex][columnIndex - 3];

		if (center == 'X' & secondLetter == 'M' && thirdLetter == 'A' && fouthLetter == 'S')
		{
			return 1;
		}

		if (center == 'S' & secondLetter == 'A' && thirdLetter == 'M' && fouthLetter == 'X')
		{
			return 1;
		}

		return 0;
	}

	private static int HasXMASNorth(List<List<char>> crosswordPuzzles, int rowIndex, int columnIndex, char center)
	{
		var HasAmountOfSpaceLeft = crosswordPuzzles.Count -1 > 3 + rowIndex;
		if (!HasAmountOfSpaceLeft)
		{
			return 0;
		}

		var secondLetter = crosswordPuzzles[rowIndex + 1][columnIndex];
		var thirdLetter = crosswordPuzzles[rowIndex + 2][columnIndex];
		var fouthLetter = crosswordPuzzles[rowIndex + 3][columnIndex];



		if (center == 'X' & secondLetter == 'M' && thirdLetter == 'A' && fouthLetter == 'S')
		{
			return 1;
		}

		if(center == 'S' & secondLetter == 'A' && thirdLetter == 'M' && fouthLetter == 'X')
		{
			return 1;
		}

		return 0;
	}
	private static int HasXMASNorthWest(List<List<char>> crosswordPuzzles, int rowIndex, int columnIndex, char center)
	{
		var HasAmountOfSpaceLeftNorth = crosswordPuzzles.Count - 1 > 3 + rowIndex;
		var HasAmountOfSpaceLeftEast = columnIndex >= 3;
		if (!HasAmountOfSpaceLeftEast || !HasAmountOfSpaceLeftNorth)
		{
			return 0;
		}

		var secondLetter = crosswordPuzzles[rowIndex + 1][columnIndex - 1];
		var thirdLetter = crosswordPuzzles[rowIndex + 2][columnIndex - 2];
		var fouthLetter = crosswordPuzzles[rowIndex + 3][columnIndex - 3];



		if (center == 'X' & secondLetter == 'M' && thirdLetter == 'A' && fouthLetter == 'S')
		{
			return 1;
		}

		if (center == 'S' & secondLetter == 'A' && thirdLetter == 'M' && fouthLetter == 'X')
		{
			return 1;
		}

		return 0;
	}
}
