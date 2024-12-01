namespace AdventOfCode2024.Utils;

public class ReadInputFile
{
	public static string GetPathToInput(int dayNumber) 
	{
		var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
		var pathToQuestion = Path.Combine(rootPath, "Days", $"Day{dayNumber}", "input.txt");
		return pathToQuestion;
	}
}
