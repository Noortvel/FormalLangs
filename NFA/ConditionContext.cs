using System;
using System.Collections.Generic;

namespace FormalLangs.NFA
{
    public struct ConditionContext<T>
        where T : IEquatable<T>
    {
        public ConditionContext(State<T> from, State<T> to, T input)
        {
            From = from;
            To = to;
            Input = input;
        }

        public State<T> From { get; private set; }

        public State<T> To { get; private set; }

        public T Input { get; private set; }
    }
}
