// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days.Day1;
using AdventOfCode2024.Days.Day10;
using AdventOfCode2024.Days.Day11;
using AdventOfCode2024.Days.Day12;
using AdventOfCode2024.Days.Day13;
using AdventOfCode2024.Days.Day14;
using AdventOfCode2024.Days.Day15;
using AdventOfCode2024.Days.Day16;
using AdventOfCode2024.Days.Day17;
using AdventOfCode2024.Days.Day18;
using AdventOfCode2024.Days.Day19;
using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day20;
using AdventOfCode2024.Days.Day21;
using AdventOfCode2024.Days.Day22;
using AdventOfCode2024.Days.Day23;
using AdventOfCode2024.Days.Day24;
using AdventOfCode2024.Days.Day25;
using AdventOfCode2024.Days.Day3;
using AdventOfCode2024.Days.Day4;
using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Days.Day6;
using AdventOfCode2024.Days.Day7;
using AdventOfCode2024.Days.Day8;
using AdventOfCode2024.Days.Day9;
using System.Diagnostics;


var stopWatch = new Stopwatch();
stopWatch.Start();
DayOnePuzzels.HandleQuestions();
DayTwoPuzzels.HandleQuestions();
DayThreePuzzels.HandleQuestions();
DayFourPuzzels.HandleQuestions();
DayFivePuzzels.HandleQuestions();
DaySixPuzzels.HandleQuestions();
DaySevenPuzzels.HandlePuzzels();
DayEightPuzzels.HandlePuzzels();
DayNinePuzzels.HandlePuzzels();

new DayTenPuzzels().HandlePuzzles();
new DayElevenPuzzles().HandlePuzzles();
new DayTwelvePuzzles().HandlePuzzles();
DayThirtheenPuzzels.HandlePuzzels();

new DayFourthteenPuzzles().HandlePuzzles();
new DayFiftheenPuzzels().HandlePuzzels();
new DaySixteenPuzzels().HandlePuzzels();
new DaySeventeenPuzzles().HandlePuzzles();
new DayEighteenPuzzles().HandlePuzzels();
new DayNineteenPuzzles().HandlePuzzles();
DayTwentyPuzzels.HandlePuzzels();
new DayTwentyOnePuzzels().HandlePuzzeles();
new DayTwentyTwoPuzzels().HandlePuzzles();
new DayTwentyTreePuzzles().HandlePuzzles();
new DayTwentyFourPuzzles().HandlePuzzles();
new DayTwentyFivePuzzles().HandlePuzzels();

stopWatch.Stop();

Console.WriteLine($"Total amount of time to run (sec) : {stopWatch.Elapsed.TotalSeconds}");


Console.ReadLine();