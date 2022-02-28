using System;

namespace FormalLangs.NFA
{
    public class Transition<T>
        where T : IEquatable<T>
    {
        public State<T> From { get; private set; }

        public State<T> To { get; private set; }

        public Func<ConditionContext<T>, bool> Condition { get; private set; }

        public Transition(
            State<T> from,
            State<T> to,
            Func<ConditionContext<T>, bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}
