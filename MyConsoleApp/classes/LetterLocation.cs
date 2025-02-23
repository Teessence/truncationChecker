public class LetterLocation
{
    public int OuterIndex { get; set; }
    public int InnerIndex { get; set; }
    public int Count { get; set; }

    public LetterLocation(int outerIndex, int innerIndex, int count)
    {
        OuterIndex = outerIndex;
        InnerIndex = innerIndex;
        Count = count;
    }
}