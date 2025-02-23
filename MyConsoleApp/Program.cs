namespace MyConsoleApp
{
    class Program
    {
        public static bool IsTruncated(string targetString, List<List<string>> Possibilities)
        {
            try
            {
                targetString = targetString.ToLowerInvariant();

                for (int x = 0; x < Possibilities.Count; x++)
                {
                    for (int y = 0; y < Possibilities[x].Count; y++)
                    {
                        Possibilities[x][y] = Possibilities[x][y].ToLowerInvariant();
                    }
                }

                Dictionary<char, List<LetterLocation>> Locations = GetLetterLocations(targetString, Possibilities);

                if (ContainsEmptyTargetLetter(Locations))
                {
                    return true;
                }

                Dictionary<char, int> TargetFrequency = GetLetterFrequency(targetString);

                foreach (var kvp in TargetFrequency)
                {
                    if (Locations.TryGetValue(kvp.Key, out List<LetterLocation> locations))
                    {
                        foreach (var combination in GetMinimumCombinations(kvp.Value, locations))
                        {
                            bool isTruncatedVar = ProcessStartingCombination(combination, targetString, Possibilities, Locations, TargetFrequency, []);

                            if (!isTruncatedVar)
                            {
                                return false;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return true;
            }
        }

        public static Dictionary<char, int> DecreaseFrequencies(Dictionary<char, int> inputtingDictionary, Dictionary<char, int> TargetFrequency)
        {
            foreach (var kvp in TargetFrequency)
            {
                char key = kvp.Key;
                int decreaseValue = kvp.Value;

                if (inputtingDictionary.ContainsKey(key))
                {
                    inputtingDictionary[key] = Math.Max(0, inputtingDictionary[key] - decreaseValue);
                    if (inputtingDictionary[key] == 0)
                    {
                        inputtingDictionary.Remove(key);
                    }
                }
            }

            return inputtingDictionary;
        }

        public static Dictionary<char, int> Recalculate(Dictionary<char, int> Frequencies, List<List<string>> Possibilities, LetterLocation ll)
        {
            Dictionary<char, int> TargetFrequency = GetLetterFrequency(Possibilities[ll.OuterIndex][ll.InnerIndex]);
            return DecreaseFrequencies(Frequencies, TargetFrequency);
        }

        public static bool ProcessStartingCombination(List<LetterLocation> StartingCombination, string TargetString, List<List<string>> Possibilities, Dictionary<char, List<LetterLocation>> Locations, Dictionary<char, int> TargetFrequency, List<LetterLocation> UsedLocations)
        {
            if (TargetFrequency.Count == 0)
            {
                return false;
            }

            foreach (LetterLocation ll in StartingCombination)
            {
                TargetFrequency = Recalculate(TargetFrequency, Possibilities, ll);
                UsedLocations.Add(ll);
            }

            if (TargetFrequency.Count == 0)
            {
                return false;
            }

            var topItem = TargetFrequency.First();

            if (Locations.TryGetValue(topItem.Key, out List<LetterLocation> locations))
            {
                foreach (var combination in GetMinimumCombinations(topItem.Value, locations))
                {
                    bool result = ProcessStartingCombination(combination, TargetString, Possibilities, Locations, TargetFrequency, UsedLocations);

                    if (!result)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }

            return true;
        }

        public static IEnumerable<List<LetterLocation>> GetMinimumCombinations(int minimumValue, List<LetterLocation> locations)
        {
            var sortedLocations = locations.OrderByDescending(l => l.Count).ToList(); // Order by highest count first

            Stack<Tuple<List<LetterLocation>, int, int, HashSet<int>>> stack = new Stack<Tuple<List<LetterLocation>, int, int, HashSet<int>>>();
            stack.Push(new Tuple<List<LetterLocation>, int, int, HashSet<int>>(new List<LetterLocation>(), 0, 0, new HashSet<int>())); // Track used OuterIndex values

            while (stack.Count > 0)
            {
                var (currentCombination, sum, index, usedOuterIndices) = stack.Pop();

                if (sum == minimumValue)
                {
                    yield return new List<LetterLocation>(currentCombination);
                    continue;
                }

                if (sum > minimumValue) continue;

                for (int i = index; i < sortedLocations.Count; i++)
                {
                    var location = sortedLocations[i];

                    if (usedOuterIndices.Contains(location.OuterIndex))
                    {
                        continue;
                    }

                    int newSum = sum + location.Count;
                    if (newSum > minimumValue)
                    {
                        continue;
                    }

                    var newCombination = new List<LetterLocation>(currentCombination) { location };
                    var newUsedOuterIndices = new HashSet<int>(usedOuterIndices) { location.OuterIndex };

                    stack.Push(new Tuple<List<LetterLocation>, int, int, HashSet<int>>(newCombination, newSum, i + 1, newUsedOuterIndices));
                }
            }
        }

        public static Dictionary<char, int> GetLetterFrequency(string input)
        {
            return input.Where(c => c != ' ').GroupBy(char.ToLower).ToDictionary(g => g.Key, g => g.Count()).OrderByDescending(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static bool ContainsEmptyTargetLetter(Dictionary<char, List<LetterLocation>> locations)
        {
            foreach (var kvp in locations)
            {
                if (kvp.Value == null || kvp.Value.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static Dictionary<char, List<LetterLocation>> Initialize(string targetString)
        {
            Dictionary<char, List<LetterLocation>> locations = [];

            foreach (char letter in targetString)
            {
                if (letter == ' ')
                {
                    continue;
                }

                if (!locations.TryGetValue(letter, out List<LetterLocation> letterLocations))
                {
                    locations[letter] = new List<LetterLocation>();
                }
            }

            return locations;
        }


        public static Dictionary<char, List<LetterLocation>> AppendLocations(Dictionary<char, List<LetterLocation>> locations, List<List<string>> Possibilities)
        {
            for (int x = 0; x < Possibilities.Count; x++)
            {
                for (int y = 0; y < Possibilities[x].Count; y++)
                {
                    for (int z = 0; z < Possibilities[x][y].Length; z++)
                    {
                        if (locations.TryGetValue(Possibilities[x][y][z], out List<LetterLocation> letterLocations))
                        {
                            var xxx = letterLocations.Find(v => v.OuterIndex == x && v.InnerIndex == y);

                            if (xxx == null)
                            {
                                letterLocations.Add(new LetterLocation(x, y, 1));
                            }
                            else
                            {
                                xxx.Count++;
                            }
                        }
                    }
                }
            }

            return locations;
        }

        public static Dictionary<char, List<LetterLocation>> GetLetterLocations(string targetString, List<List<string>> Possibilities)
        {
            try
            {
                Dictionary<char, List<LetterLocation>> locations = Initialize(targetString);
                locations = AppendLocations(locations, Possibilities);
                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return [];
            }
        }
    }
}
