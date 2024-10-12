namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public abstract record SegmentResultType
{
    public sealed record SegmentSuccess(double Time) : SegmentResultType
    {
        public double CompletionTime => Time;
    }

    public abstract record SegmentFailure : SegmentResultType
    {
        public sealed record ExceededSpeed : SegmentFailure;

        public sealed record ExceededPower : SegmentFailure;

        public sealed record WrongDirection : SegmentFailure;

        public sealed record NotMoving : SegmentFailure;
    }
}