using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FormalLangs.DFA
{
    public class StateMachine<TState> : IEnumerator<State<TState>>
    where TState : IEquatable<TState>
    {
        public StateMachine(State<TState> start)
        {
            Start = start;
        }

        private IEnumerator<TState> inputEnumerator;

        public IEnumerable<TState> Input { get; private set; }

        public State<TState> Current { get; private set; }

        public State<TState> Start { get; private set; }

        public StateMachineState MachineState { get; private set; } = StateMachineState.None;

        object IEnumerator.Current => Current;

        public bool IsEndState => MachineState == StateMachineState.End;

        public void Init(IEnumerable<TState> input)
        {
            Input = input;
            Current = Start;
            inputEnumerator = Input.GetEnumerator();
            MachineState = StateMachineState.Proccess;
        }

        public bool MoveNext()
        {
            if (MachineState == StateMachineState.None ||
                MachineState != StateMachineState.Proccess)
            {
                return false;
            }

            if (!inputEnumerator.MoveNext())
            {
                var current = Current;
                if(current == null)
                {
                    Console.WriteLine("Current is null");
                }
                var endNode = Current.Childs.FirstOrDefault(x => x.IsEnd());
                if(endNode == default)
                {
                    MachineState = StateMachineState.InputEndedButEndStateNotArrived;
                    return false;
                }

                MachineState = StateMachineState.End;
                return false;                            
            }

            var token = inputEnumerator.Current;
            var node = Current.Childs.FirstOrDefault(x => x.Value.Equals(token));
            if (node == default)
            {
                var endNode = Current.Childs.FirstOrDefault(x => x.IsEnd());
                if (endNode == default)
                {
                    MachineState = StateMachineState.InputHasNotEnd;
                    return false;
                }
            }

            Current = node;
            return true;
        }

        public void MoveAll()
        {
            while(MoveNext());
        }

        public void Reset()
        {
            Input = default;
            Current = Start;
            if(inputEnumerator != default)
            {
                inputEnumerator.Dispose();
            }
            inputEnumerator = default;
            MachineState = StateMachineState.None;
        }

        public void Dispose()
        {
            inputEnumerator.Dispose();
        }
    }
}
