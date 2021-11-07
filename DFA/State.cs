using System;
using System.Collections.Generic;

namespace FormalLangs.DFA
{
    public class State<T>
    where T : IEquatable<T>
    {
        public State(T value)
        {
            Value = value;
            childs = new();
        }

        private List<State<T>> childs;

        public T Value { get; private set; }

        public IReadOnlyCollection<State<T>> Childs => childs;

        public void AddNext(State<T> state) => childs.Add(state);

        public bool IsEnd()
        {
            return Value.Equals(default) && childs.Count == 0;
        }

        public static State<T> BuildStart(IEnumerable<State<T>> nexts)
        {
            var state = new State<T>(default);

            foreach(var x in nexts){
                state.AddNext(x);
            }

            return state;
        }

        public static State<T> BuildStart(State<T> next)
        {
            var state = new State<T>(default);
            state.AddNext(next);
            return state;
        }

        public static State<T> BuildEnd()
        {
            return new State<T>(default);
        }
    }
}
