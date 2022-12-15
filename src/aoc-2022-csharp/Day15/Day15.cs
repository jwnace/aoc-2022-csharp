namespace aoc_2022_csharp.Day15;

public static class Day15
{
    private static readonly string[] Input = File.ReadAllLines("Day15/day15.txt");
    private const int SearchRow = 2_000_000;
    private const int SearchArea = 4_000_000;

    public static int Part1()
    {
        var map = new Dictionary<(int X, int Y), char>();

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var sensorX = int.Parse(values[2][2..^1]);
            var sensorY = int.Parse(values[3][2..^1]);
            var beaconX = int.Parse(values[^2][2..^1]);
            var beaconY = int.Parse(values[^1][2..]);
            var distance = Math.Abs(beaconX - sensorX) + Math.Abs(beaconY - sensorY);

            map[(sensorX, sensorY)] = 'S';
            map[(beaconX, beaconY)] = 'B';

            // if the sensor doesn't reach Y = 2_000_000, then we don't care about it
            if ((sensorY > SearchRow || sensorY + distance < SearchRow) &&
                (sensorY < SearchRow || sensorY - distance > SearchRow))
            {
                continue;
            }

            var dX = distance - Math.Abs(SearchRow - sensorY);

            for (var x = sensorX - dX; x <= sensorX + dX; x++)
            {
                map[(x, SearchRow)] = map.TryGetValue((x, SearchRow), out var v) ? v : '#';
            }
        }

        return map.Where(m => m.Key.Y == SearchRow).Count(x => x.Value == '#');
    }

    public static long Part2()
    {
        var sensors = GetSensors();
        var candidates = GetCandidates(sensors);

        var answer = candidates.First(c =>
            !sensors.Any(sensor => Math.Abs(sensor.Key.X - c.X) + Math.Abs(sensor.Key.Y - c.Y) <= sensor.Value));

        return (long)SearchArea * answer.X + answer.Y;
    }

    private static IEnumerable<(int X, int Y)> GetCandidates(Dictionary<(int X, int Y), int> sensors)
    {
        var candidates = new HashSet<(int X, int Y)>();

        foreach (var sensor in sensors)
        {
            var distance = sensor.Value + 1;
            var minY = sensor.Key.Y - distance;
            var maxY = sensor.Key.Y + distance;
            var minX = sensor.Key.X - distance;
            var maxX = sensor.Key.X + distance;

            for (var i = 0; i < distance; i++)
            {
                if (sensor.Key.X + i is >= 0 and <= SearchArea && maxY - i is >= 0 and <= SearchArea)
                {
                    candidates.Add((sensor.Key.X + i, maxY - i));
                }

                if (maxX - i is >= 0 and <= SearchArea && sensor.Key.Y - i is >= 0 and <= SearchArea)
                {
                    candidates.Add((maxX - i, sensor.Key.Y - i));
                }

                if (sensor.Key.X - i is >= 0 and <= SearchArea && minY + i is >= 0 and <= SearchArea)
                {
                    candidates.Add((sensor.Key.X - i, minY + i));
                }

                if (minX + i is >= 0 and <= SearchArea && sensor.Key.Y + i is >= 0 and <= SearchArea)
                {
                    candidates.Add((minX + i, sensor.Key.Y + i));
                }
            }
        }

        return candidates;
    }

    private static Dictionary<(int X, int Y), int> GetSensors()
    {
        var sensors = new Dictionary<(int X, int Y), int>();

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var sensorX = int.Parse(values[2][2..^1]);
            var sensorY = int.Parse(values[3][2..^1]);
            var beaconX = int.Parse(values[^2][2..^1]);
            var beaconY = int.Parse(values[^1][2..]);

            var dX = Math.Abs(beaconX - sensorX);
            var dY = Math.Abs(beaconY - sensorY);
            var distance = dX + dY;

            sensors[(sensorX, sensorY)] = distance;
        }

        return sensors;
    }
}
