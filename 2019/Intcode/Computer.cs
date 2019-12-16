using System;
using System.Collections.Generic;
using System.Numerics;

namespace AoC2019.Intcode
{
    public delegate void OutputHandler(BigInteger value);
    public delegate BigInteger InputProvider();

    public interface iCommand
    {
        BigInteger ArgCount { get; }

        String Disassemble(List<BigInteger> code, int index);
        void Execute(Computer computer);
    }

    public class Memory : Dictionary<BigInteger, BigInteger>
    {
    }

    public class Computer
    {
        public Dictionary<BigInteger, iCommand> instructionset = new Dictionary<BigInteger, iCommand>() {
            { Add.OPCODE,           new Add()           },
            { Multiply.OPCODE,      new Multiply()      },
            { Input.OPCODE,         new Input()         },
            { Output.OPCODE,        new Output()        },
            { JumpIfFalse.OPCODE,   new JumpIfFalse()   },
            { JumpIfTrue.OPCODE,    new JumpIfTrue()    },
            { LessThan.OPCODE,      new LessThan()      },
            { IsEqual.OPCODE,       new IsEqual()       },
            { IncrementBase.OPCODE, new IncrementBase() },
            { Halt.OPCODE,          new Halt()          },
        };

        public Memory ram = new Memory();
        public BigInteger ip = 0;
        public BigInteger rb = 0;
        public InputProvider input = () => { return 0; };
        public OutputHandler output = (value) => { };
        public Boolean halt = false;

        public BigInteger GetValue(Byte arg)
        {
            var mod = GetModifiers(ram[ip])[arg - 1];
            switch (mod)
            {
                case 0: return ram.ContainsKey(ram[ip + arg]) ? ram[ram[ip + arg]] : 0;
                case 1: return ram[ip + arg];
                case 2: return ram.ContainsKey(rb + ram[ip + arg]) ? ram[rb + ram[ip + arg]] : 0;
                default: throw new Exception("undefined mode");
            }
        }

        public void SetValue(BigInteger value, Byte arg)
        {
            var mod = GetModifiers(ram[ip])[arg - 1];
            switch (mod)
            {
                case 0: ram[ram[ip + arg]] = value; return;
                case 1: throw new Exception("mode 1 is not supported for SetValue");
                case 2: ram[rb + ram[ip + arg]] = value; return;
                default: throw new Exception("undefined mode");
            }
        }

        public List<Byte> GetModifiers(BigInteger opcode)
        {
            var result = new List<Byte>();
            result.Add((byte)((opcode / 100) % 10));
            result.Add((byte)((opcode / 1000) % 10));
            result.Add((byte)((opcode / 10000) % 10));
            return result;
        }

        public void LoadProgram(List<BigInteger> code)
        {
            BigInteger index = 0;
            foreach (var item in code)
                ram[index++] = item;
        }

        public void Run()
        {
            //Console.WriteLine("Computer.Run()");
            while (!halt)
            {
                var opcode = ram[ip] % 100;
                var command = instructionset[opcode];
                command.Execute(this);
            }
        }

        public void Disassemble(List<BigInteger> code)
        {
            int index = 0;
            while (index < code.Count)
            {
                var opcode = code[index] % 100;
                if (instructionset.ContainsKey(opcode))
                {
                    var command = instructionset[opcode];
                    Console.WriteLine("{0}\t{1}",index,command.Disassemble(code, index));
                    index = index + (int)command.ArgCount + 1;
                }
                else
                {
                    index += 1;
                }
            }
        }
    }
}
