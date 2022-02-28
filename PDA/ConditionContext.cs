using System;
using System.Collections.Generic;

namespace FormalLangs.PDA
{
    public struct ConditionContext<T>
        where T : IEquatable<T>
    {
        public ConditionContext(Stack<T> memory, State<T> from, State<T> to, T input)
        {
            Memory = memory;
            From = from;
            To = to;
            Input = input;
        }

        public Stack<T> Memory { get; private set; }

        public State<T> From { get; private set; }

        public State<T> To { get; private set; }

        public T Input { get; private set; }
    }
}
