using System;
using System.Collections.Generic;
using System.Linq;

namespace FormalLangs.PDA
{
    public class ScopeMachine
    {
        private Stack<char> shared = new();
        private const string OpenScopes = "([{";
        private const string CloseScope = ")]}";

        private readonly IEnumerable<string> Pairs = new[]
        {
            "()", "[]", "{}"
        };

        private State<char> start;
        private string input;

        public ScopeMachine(string input)
        {
            this.input = input;

            var end = State<char>.BuildEnd(shared);

            var openScope = new State<char>(shared);
            var closeScope = new State<char>(shared);

            openScope.AddNext(openScope, OpenScopeCondition);
            openScope.AddNext(closeScope, CloseScopeCondition);

            closeScope.AddNext(openScope, OpenScopeCondition);
            closeScope.AddNext(closeScope, CloseScopeCondition);

            start = State<char>.BuildStart(openScope, OpenScopeCondition, shared);
        }

        public void Proccess()
        {
            bool isError = false;
            var current = start;
            try
            {
                foreach (var c in input)
                {
                    var _old = current;
                    current = current.FindNext(c);
                    //Console.WriteLine($"in '{c}', old '{_old}', current '{current}'");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                isError = true;
            }

            if (!isError)
            {
                Console.WriteLine("Scopes consisted");
            }
        }

        private bool OpenScopeCondition(ConditionContext<char> context)
        {
            if (OpenScopes.Contains(context.Input))
            {
                context.Memory.Push(context.Input);
                return true;
            }

            return false;
        }

        private bool CloseScopeCondition(ConditionContext<char> context)
        {
            if (CloseScope.Contains(context.Input)
                && context.Memory.TryPop(out var val)
                && Pairs.Any(x => x.Contains(context.Input) && x.Contains(val)))
            {
                return true;
            }

            return false;
        }
    }
}
