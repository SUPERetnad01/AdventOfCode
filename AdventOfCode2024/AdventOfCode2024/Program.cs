// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days.Day1;
using AdventOfCode2024.Days.Day10;
using AdventOfCode2024.Days.Day11;
using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day3;
using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Days.Day8;

DayOnePuzzels.HandleQuestions();
DayTwoPuzzels.HandleQuestions();
DayThreePuzzels.HandleQuestions();
//DayFourPuzzels.HandleQuestions(); // takes about 10 seconds
DayFivePuzzels.HandleQuestions();

//var stackSize = 10000000;
//Thread thread = new Thread(new ThreadStart(DaySixPuzzels.HandleQuestions), stackSize);
//thread.Start();

//DaySevenPuzzels.HandlePuzzels();
//DayNinePuzzels.HandlePuzzels();

new DayTenPuzzels().HandlePuzzles();
new DayElevenPuzzles().HandlePuzzles();

Console.ReadLine();