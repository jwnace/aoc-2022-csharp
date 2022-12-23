using System.Diagnostics;

var stopwatch = new Stopwatch();

// stopwatch.Start();
// var day01Part1 = Day01.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 01, Part 1 took {stopwatch.Elapsed} to run => {day01Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day01Part2 = Day01.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 01, Part 2 took {stopwatch.Elapsed} to run => {day01Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day02Part1 = Day02.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 02, Part 1 took {stopwatch.Elapsed} to run => {day02Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day02Part2 = Day02.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 02, Part 2 took {stopwatch.Elapsed} to run => {day02Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day03Part1 = Day03.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 03, Part 1 took {stopwatch.Elapsed} to run => {day03Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day03Part2 = Day03.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 03, Part 2 took {stopwatch.Elapsed} to run => {day03Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day04Part1 = Day04.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 04, Part 1 took {stopwatch.Elapsed} to run => {day04Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day04Part2 = Day04.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 04, Part 2 took {stopwatch.Elapsed} to run => {day04Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day05Part1 = Day05.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 05, Part 1 took {stopwatch.Elapsed} to run => {day05Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day05Part2 = Day05.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 05, Part 2 took {stopwatch.Elapsed} to run => {day05Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day06Part1 = Day06.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 06, Part 1 took {stopwatch.Elapsed} to run => {day06Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day06Part2 = Day06.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 06, Part 2 took {stopwatch.Elapsed} to run => {day06Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day07Part1 = Day07.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 07, Part 1 took {stopwatch.Elapsed} to run => {day07Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day07Part2 = Day07.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 07, Part 2 took {stopwatch.Elapsed} to run => {day07Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day08Part1 = Day08.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 08, Part 1 took {stopwatch.Elapsed} to run => {day08Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day08Part2 = Day08.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 08, Part 2 took {stopwatch.Elapsed} to run => {day08Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day09Part1 = Day09.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 09, Part 1 took {stopwatch.Elapsed} to run => {day09Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day09Part2 = Day09.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 09, Part 2 took {stopwatch.Elapsed} to run => {day09Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day10Part1 = Day10.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 10, Part 1 took {stopwatch.Elapsed} to run => {day10Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day10Part2 = Day10.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 10, Part 2 took {stopwatch.Elapsed} to run => {day10Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day11Part1 = Day11.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 11, Part 1 took {stopwatch.Elapsed} to run => {day11Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day11Part2 = Day11.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 11, Part 2 took {stopwatch.Elapsed} to run => {day11Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day12Part1 = Day12.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 12, Part 1 took {stopwatch.Elapsed} to run => {day12Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day12Part2 = Day12.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 12, Part 2 took {stopwatch.Elapsed} to run => {day12Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day13Part1 = Day13.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 13, Part 1 took {stopwatch.Elapsed} to run => {day13Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day13Part2 = Day13.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 13, Part 2 took {stopwatch.Elapsed} to run => {day13Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day14Part1 = Day14.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 14, Part 1 took {stopwatch.Elapsed} to run => {day14Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day14Part2 = Day14.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 14, Part 2 took {stopwatch.Elapsed} to run => {day14Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day15Part1 = Day15.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 15, Part 1 took {stopwatch.Elapsed} to run => {day15Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day15Part2 = Day15.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 15, Part 2 took {stopwatch.Elapsed} to run => {day15Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day16Part1 = Day16.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 16, Part 1 took {stopwatch.Elapsed} to run => {day16Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day16Part2 = Day16.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 16, Part 2 took {stopwatch.Elapsed} to run => {day16Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day17Part1 = Day17.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 17, Part 1 took {stopwatch.Elapsed} to run => {day17Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day17Part2 = Day17.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 17, Part 2 took {stopwatch.Elapsed} to run => {day17Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day18Part1 = Day18.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 18, Part 1 took {stopwatch.Elapsed} to run => {day18Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day18Part2 = Day18.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 18, Part 2 took {stopwatch.Elapsed} to run => {day18Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day19Part1 = Day19.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 19, Part 1 took {stopwatch.Elapsed} to run => {day19Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day19Part2 = Day19.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 19, Part 2 took {stopwatch.Elapsed} to run => {day19Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day20Part1 = Day20.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 20, Part 1 took {stopwatch.Elapsed} to run => {day20Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day20Part2 = Day20.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 20, Part 2 took {stopwatch.Elapsed} to run => {day20Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day21Part1 = Day21.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 21, Part 1 took {stopwatch.Elapsed} to run => {day21Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day21Part2 = Day21.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 21, Part 2 took {stopwatch.Elapsed} to run => {day21Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day22Part1 = Day22.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 22, Part 1 took {stopwatch.Elapsed} to run => {day22Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day22Part2 = Day22.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 22, Part 2 took {stopwatch.Elapsed} to run => {day22Part2}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day23Part1 = Day23.Part1();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 23, Part 1 took {stopwatch.Elapsed} to run => {day23Part1}");
// stopwatch.Reset();
//
// stopwatch.Start();
// var day23Part2 = Day23.Part2();
// stopwatch.Stop();
//
// Console.WriteLine($"Day 23, Part 2 took {stopwatch.Elapsed} to run => {day23Part2}");

stopwatch.Start();
var day24Part1 = Day24.Part1();
stopwatch.Stop();

Console.WriteLine($"Day 24, Part 1 took {stopwatch.Elapsed} to run => {day24Part1}");
stopwatch.Reset();

stopwatch.Start();
var day24Part2 = Day24.Part2();
stopwatch.Stop();

Console.WriteLine($"Day 24, Part 2 took {stopwatch.Elapsed} to run => {day24Part2}");
