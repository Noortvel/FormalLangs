namespace FormalLangs.NFA
{
    public class DummyMachineTests
    {
        public void TestConsistss1()
        {
            var machine = new DummyMachine("0");
            machine.Proccess();
        }

        public void TestNotConsists1()
        {
            var machine = new DummyMachine("00");
            machine.Proccess();
        }
    }
}
