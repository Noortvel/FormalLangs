using System;

namespace FormalLangs.NFA
{
    public class DummyMachine
    {
        private State<char> start;
        private string input;

        public DummyMachine(string input)
        {
            this.input = input;

            var end = State<char>.BuildEnd();
            start = State<char>.BuildStart(end, TrueCondition);
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
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                isError = true;
            }

            if (!isError)
            {
                Console.WriteLine("NFA machine worked!");
            }
        }

        private bool TrueCondition(ConditionContext<char> context)
            => true;
    }
}
