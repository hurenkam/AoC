using AoC2019.Intcode;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace AoC2019.Day7
{
    public delegate void OutputProvider(int value);
    public class Amplifier
    {
        public event OutputProvider Output;

        private List<int> _input = new List<int>();
        public void Input(int input)
        {
            _input.Add(input);
        }

        private void OutputHandler(BigInteger value)
        {
            Output?.Invoke((int) value);
        }

        private BigInteger InputProvider()
        {
            while (_input.Count < 1)
            {
                Thread.Sleep(100);
            }
            var result = _input[0];
            _input.RemoveAt(0);
            return result;
        }

        public Amplifier(int phase)
        {
            _input.Add(phase);
            var t = new Thread(new ThreadStart(Run));
            t.Start();
        }

        private void Run()
        {
            var computer = new Computer();
            computer.LoadProgram(AoC2019.Day7.Input.Code);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            computer.Run();
        }
    }
}
