using AoC2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2019.Day15
{
    public class RepairDroid
    {
        private Queue<BigInteger> _inputqueue = new Queue<BigInteger>();
        private Queue<BigInteger> _outputqueue = new Queue<BigInteger>();
        private Semaphore _guardinput = new Semaphore(0, 1);
        private Semaphore _guardoutput = new Semaphore(0, 1);
        private List<BigInteger> _inputs = new List<BigInteger>() { 1, 4, 2, 3 };

        public RepairDroid()
        {
            var computer = new Computer();
            computer.LoadProgram(Input.Data);
            computer.input = InputProvider;
            computer.output = OutputHandler;
            Thread t = new Thread(new ThreadStart(computer.Run));
            t.Start();
        }

        public void Halt()
        {
            _inputqueue.Enqueue(0);
            _guardinput.Release();
        }

        public BigInteger TryMove(int direction)
        {
            if (direction < 0) direction += 4;
            if (direction > 3) direction -= 4;

            _inputqueue.Enqueue(_inputs[(int)direction]);
            _guardinput.Release();
            _guardoutput.WaitOne();
            return _outputqueue.Dequeue();
        }

        private BigInteger InputProvider()
        {
            _guardinput.WaitOne();
            var v = _inputqueue.Dequeue();
            return v;
        }

        private void OutputHandler(BigInteger value)
        {
            _outputqueue.Enqueue(value);
            _guardoutput.Release();
        }
    }
}
