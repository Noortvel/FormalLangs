using System.Collections.Generic;
using FormalLangs.DFA;

namespace FormalLangs.Regexpr
{
    public class RegexMachine
    {
        private StateMachine<char> stateMachine;

        private RegexMachine()
        {
        }

        public bool IsMatch(IEnumerable<char> src)
        {
            stateMachine.Reset();
            stateMachine.Init(src);
            stateMachine.MoveAll();
            return stateMachine.IsEndState;
        }

        public static RegexMachine BuildACDBDE2Machine()
        {
            var stateMachine = new StateMachine<char>(BuildACDBDE2());
            var regexMachine = new RegexMachine();
            regexMachine.stateMachine = stateMachine;
            return regexMachine;
        }

        public static RegexMachine BuildAMachine()
        {
            var stateMachine = new StateMachine<char>(BuildA());
            var regexMachine = new RegexMachine();
            regexMachine.stateMachine = stateMachine;
            return regexMachine;
        }

        public static State<char> BuildA()
        {
            //a+
            var end = State<char>.BuildEnd();

            var a1 = new State<char>('a');
            a1.AddNext(a1);
            a1.AddNext(end);

            var start = State<char>.BuildStart(a1);

            return start;
        }

        public static State<char> BuildACDBDE2()
        {
            //(a|cd)+(b|d|e)*

            var end = State<char>.BuildEnd();

            var a1 = new State<char>('a');
            var cd1c = new State<char>('c');
            var cd1d = new State<char>('d');
            cd1c.AddNext(cd1d);

            var a2 = new State<char>('a');
            var cd2c = new State<char>('c');
            var cd2d = new State<char>('d');
            cd2c.AddNext(cd2d);

            var b1 = new State<char>('b');
            var d1 = new State<char>('d');
            var e1 = new State<char>('e');

            var start = State<char>.BuildStart(a1);
            start.AddNext(cd1c);

            a1.AddNext(a2);
            a1.AddNext(cd2c);
            a1.AddNext(b1);
            a1.AddNext(d1);
            a1.AddNext(e1);
            a1.AddNext(end);

            cd1d.AddNext(cd2c);
            cd1d.AddNext(a2);
            cd1d.AddNext(b1);
            cd1d.AddNext(d1);
            cd1d.AddNext(e1);
            cd1d.AddNext(end);

            cd2d.AddNext(cd2c);
            cd2d.AddNext(a2);
            cd2d.AddNext(b1);
            cd2d.AddNext(d1);
            cd2d.AddNext(e1);
            cd2d.AddNext(end);

            a2.AddNext(a2);
            a2.AddNext(cd2c);
            a2.AddNext(b1);
            a2.AddNext(d1);
            a2.AddNext(e1);
            a2.AddNext(end);

            b1.AddNext(b1);
            b1.AddNext(d1);
            b1.AddNext(e1);
            b1.AddNext(end);

            d1.AddNext(d1);
            d1.AddNext(b1);
            d1.AddNext(e1);
            d1.AddNext(end);

            e1.AddNext(e1);
            e1.AddNext(b1);
            e1.AddNext(d1);
            e1.AddNext(end);

            return start;
        }
    }
}
