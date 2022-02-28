namespace FormalLangs.PDA
{
    public class ScopeMachineTests
    {
        public void TestConsistss1()
        {
            var machine = new ScopeMachine("(([[{}]]))");
            machine.Proccess();
        }

        public void TestConsistss2()
        {
            var machine = new ScopeMachine("()[()]{()()[]}");
            machine.Proccess();
        }

        public void TestNotConsists1()
        {
            var machine = new ScopeMachine("(([[{}]])))");
            machine.Proccess();
        }
    }
}
