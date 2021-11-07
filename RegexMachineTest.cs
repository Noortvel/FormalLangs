using System;
using System.Text;
using System.Text.RegularExpressions;

namespace FormalLangs
{
    public class RegexMachineTest
    {
        public void TestA()
        {
            var pattern = "a+";
            Console.WriteLine($"Test '{pattern}'");
            var dataset = new[]
            {
                "aaa",
                "a",
                "b",
                ""
            };

            const int offset = -15;
            Console.WriteLine($"{"Data",offset} {"Actual",offset} {"Expected",offset} {"Equals",offset}");
            foreach (var data in dataset)
            {
                var myregex = RegexMachine.BuildAMachine();
                var actual = myregex.IsMatch(data);
                var expected = Regex.Match(data, pattern).Success;
                var dataScoped = $"{data}";
                Console.WriteLine(
                    $"{dataScoped,offset} {actual,offset} {expected,offset} {actual == expected,offset}");
            }

            Console.WriteLine();
        }

        public void TestACDBDE()
        {
            var pattern = "(a|cd)+(b|d|e)*";
            Console.WriteLine($"Test '{pattern}'");
            var dataset = new[]
            {
                "acdaed",
                "aab",
                "cdcdbdbbbe",
                "cdcdcdaaeee",
                "acddded",
                "aeddee",
                "cdabdeeeeeee",
            };

            const int offset = -15;
            Console.WriteLine($"{"Data",offset} {"Actual",offset} {"Expected",offset} {"Equals",offset}");
            foreach (var data in dataset)
            {
                var myregex = RegexMachine.BuildACDBDE2Machine();
                var actual = myregex.IsMatch(data);
                var expected = Regex.Match(data, pattern).Success;
                var dataScoped = $"{data}";
                Console.WriteLine(
                    $"{dataScoped,offset} {actual,offset} {expected,offset} {actual == expected,offset}");
            }

            Console.WriteLine();
        }
    }
}
