namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class SegmentResultType
{
    public class SegmentSuccess(double time) : SegmentResultType
    {
        public double CompletionTime { get; init; } = time;
    }

    public class SegmentFailure : SegmentResultType
    {
        public class ExceededSpeed : SegmentFailure;

        public class ExceededPower : SegmentFailure;

        public class WrongDirection : SegmentFailure;

        public class NotMoving : SegmentFailure;
    }
}