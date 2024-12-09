namespace AdventOfCode2024.Utils;

public class ReadInputFile
{
	public static string GetPathToInput(int dayNumber) 
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToQuestion = Path.Combine(rootPath, "Days", $"Day{dayNumber}", "input.txt");
		return pathToQuestion;
	}

	public static List<List<int>> GetGrid(string path) {
		var input = File.ReadAllLines(path);
		var grid = input
			.Select(_ => 
				_.Split(" ")
				 .Select(_ => int.Parse(_))
				 .ToList()).ToList();
		return grid;
	}

	public static List<List<char>> GetGridChar(string path)
	{
		var input = File.ReadAllLines(path);
		var grid = input.Select(_ => _.ToList()).ToList();
		return grid;
	}

	public static string GetPathToTestInput(int dayNumber)
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToQuestion = Path.Combine(rootPath, "TestInput", $"input{dayNumber}.txt");
		return pathToQuestion;
	}

}
