using FormalLangs.NFA;
using FormalLangs.PDA;

namespace FormalLangs
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tests = new ScopeMachineTests();
            tests.TestConsistss1();
            tests.TestConsistss2();
            tests.TestNotConsists1();

            var dummyTests = new DummyMachineTests();
            dummyTests.TestConsistss1();
            dummyTests.TestNotConsists1();
        }
    }
}
