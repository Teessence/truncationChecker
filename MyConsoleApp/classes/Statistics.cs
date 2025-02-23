class Statistics
{
    public int OuterIndex { get; set; }
    public int InnerIndex { get; set; }
    public int MatchingCharacters { get; set; }

    public Statistics(int outerIndex, int innerIndex, int matchingCharacters)
    {
        OuterIndex = outerIndex;
        InnerIndex = innerIndex;
        MatchingCharacters = matchingCharacters;
    }
}