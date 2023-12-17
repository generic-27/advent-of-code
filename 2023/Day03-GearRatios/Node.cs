using System.Data.Common;

namespace Day03_GearRatios;

internal class Node : IEquatable<Node>
{
    public int Row { get; set; }

    public int Col { get; set; }

    public Node(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public bool Equals(Node? other)
    {
        if (other == null || !this.GetType().Equals(other.GetType()))
        {
            return false;
        }

        Node p = other;

        return Row == p.Row && Col == p.Col;
    }

    public override int GetHashCode()
    {
        return (Row << 2) ^ Col;
    }
}
