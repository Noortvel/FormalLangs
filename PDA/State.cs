using System;
using System.Collections.Generic;
using System.Linq;

namespace FormalLangs.PDA
{
    public class State<T>
        where T : IEquatable<T>
    {

        private Stack<T> memory;
        private List<Transition<T>> transitions = new();

        public State(Stack<T> memory)
        {
            this.memory = memory;
        }

        public IReadOnlyCollection<Transition<T>> Transitions
            => transitions;

        public void AddNext(State<T> to, Func<ConditionContext<T>, bool> condition)
            => transitions.Add(new Transition<T>(this, to, condition));

        public bool IsEnd()
            => transitions.Count == 0;

        public State<T> FindNext(T input)
        {
            var next = transitions.FirstOrDefault(x => x.Condition(new ConditionContext<T>(memory, this, x.To, input)));
            if (next == default)
            {
                throw new InvalidOperationException("Cannot be finded");
            }

            return next.To;
        }

        public static State<T> BuildStart(IEnumerable<(State<T>, Func<ConditionContext<T>, bool>)> nexts, Stack<T> memory)
        {
            var state = new State<T>(memory);

            foreach (var x in nexts)
            {
                state.AddNext(x.Item1, x.Item2);
            }

            return state;
        }

        public static State<T> BuildStart(State<T> to, Func<ConditionContext<T>, bool> condition, Stack<T> memory)
        {
            var state = new State<T>(memory);
            state.AddNext(to, condition);
            return state;
        }

        public static State<T> BuildEnd(Stack<T> memory)
        {
            return new State<T>(memory);
        }
    }
}
