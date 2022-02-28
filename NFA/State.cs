using System;
using System.Collections.Generic;
using System.Linq;

namespace FormalLangs.NFA
{
    public class State<T>
        where T : IEquatable<T>
    {
        private List<Transition<T>> transitions = new();

        public State()
        {
        }

        public IReadOnlyCollection<Transition<T>> Transitions
            => transitions;

        public void AddNext(State<T> to, Func<ConditionContext<T>, bool> condition)
            => transitions.Add(new Transition<T>(this, to, condition));

        public bool IsEnd()
            => transitions.Count == 0;

        public State<T> FindNext(T input)
        {
            var next = transitions.FirstOrDefault(x => x.Condition(new ConditionContext<T>(this, x.To, input)));
            if (next == default)
            {
                throw new InvalidOperationException("Cannot be finded");
            }

            return next.To;
        }

        public static State<T> BuildStart(IEnumerable<(State<T>, Func<ConditionContext<T>, bool>)> nexts)
        {
            var state = new State<T>();

            foreach (var x in nexts)
            {
                state.AddNext(x.Item1, x.Item2);
            }

            return state;
        }

        public static State<T> BuildStart(State<T> to, Func<ConditionContext<T>, bool> condition)
        {
            var state = new State<T>();
            state.AddNext(to, condition);
            return state;
        }

        public static State<T> BuildEnd()
        {
            return new State<T>();
        }
    }
}
