namespace Day12_HillClimbingAlgorithm;

internal class ElevationNode : IEquatable<ElevationNode>, IComparable<ElevationNode>
{
    public int Row { get; set; }

    public int Column { get; set; }

    public char Character { get; set; }

    public int Priority { get; set; }

    public int CompareTo(ElevationNode? other)
    {
        return Priority.CompareTo(other.Priority);
    }

    public bool Equals(ElevationNode? other)
    {
        if ((other == null) || !this.GetType().Equals(other.GetType()))
        {
            return false;
        }

        ElevationNode p = other;

        return Row == p.Row && Column == p.Column;
    }

    public override int GetHashCode()
    {
        return (Row << 2) ^ Column;
    }
}
