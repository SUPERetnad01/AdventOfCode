﻿// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days.Day1;
using AdventOfCode2024.Days.Day10;
using AdventOfCode2024.Days.Day11;
using AdventOfCode2024.Days.Day13;
using AdventOfCode2024.Days.Day16;
using AdventOfCode2024.Days.Day19;
using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day20;
using AdventOfCode2024.Days.Day21;
using AdventOfCode2024.Days.Day22;
using AdventOfCode2024.Days.Day23;
using AdventOfCode2024.Days.Day3;
using AdventOfCode2024.Days.Day5;

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
DayThirtheenPuzzels.HandlePuzzels();

//new DaySixteenPuzzels().HandlePuzzels();
//new DayEighteenPuzzles().HandlePuzzels();
new DayNineteenPuzzles().HandlePuzzles();
//DayTwentyPuzzels.HandlePuzzels();

new DayTwentyOnePuzzels().HandlePuzzles();
//new StolenCode().HandlePuzzele();
//new DayTwentyTwoPuzzels().HandlePuzzles();
new DayTwentyTreePuzzles().HandlePuzzles();
Console.ReadLine();