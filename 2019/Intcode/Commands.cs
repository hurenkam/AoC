using System;
using System.Collections.Generic;
using System.Numerics;

namespace AoC2019.Intcode
{
    public abstract class Command
    {
        public List<Byte> GetModifiers(BigInteger opcode)
        {
            var result = new List<Byte>();
            result.Add((byte)((opcode / 100) % 10));
            result.Add((byte)((opcode / 1000) % 10));
            result.Add((byte)((opcode / 10000) % 10));
            return result;
        }

        public List<String> GetArguments(List<BigInteger> code)
        {
            var result = new List<String>();
            var mods = GetModifiers(code[0]);
            for (int i = 1; i < code.Count; i++)
            {
                BigInteger arg = code[i];
                switch (mods[i-1])
                {
                    case 0: result.Add(String.Format("[{0}]", arg)); break;
                    case 1: result.Add(String.Format("{0}", arg)); break;
                    case 2: result.Add(String.Format("<{0}>", arg)); break;
                    default: throw new Exception("Unknown modifier");
                }
            }
            return result;
        }
    }

    public class Halt: iCommand
    {
        public const byte OPCODE = 99;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 0; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            return String.Format("HLT");
        }

        public void Execute(Computer computer)
        {
            computer.halt = true;
            computer.ip += 1;
        }
    }

    public class Add : Command, iCommand
    {
        public const byte OPCODE = 1;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 3; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int) ArgCount+1));
            return String.Format("ADD {0},{1} >> {2}", args[0], args[1], args[2]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);
            var r = a + b;
            computer.SetValue(r, 3);
            computer.ip = computer.ip + ArgCount + 1;
        }
    }

    public class Multiply: Command, iCommand
    {
        public const byte OPCODE = 2;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 3; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("MLT {0},{1} >> {2}", args[0], args[1], args[2]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);
            var r = a * b;
            computer.SetValue(r, 3);
            computer.ip = computer.ip + ArgCount + 1;
        }
    }

    public class Input : Command, iCommand
    {
        public const byte OPCODE = 3;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 1; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("INP >> {0}", args[0]);
        }

        public void Execute(Computer computer)
        {
            computer.SetValue(computer.input(),1);
            computer.ip = computer.ip + ArgCount + 1;
        }
    }

    public class Output : Command, iCommand
    {
        public const byte OPCODE = 4;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 1; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("OUT << {0}", args[0]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            computer.output(a);
            computer.ip = computer.ip + ArgCount + 1;
        }
    }

    public class JumpIfTrue : Command, iCommand
    {
        public const byte OPCODE = 5;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 2; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("JIT {0}? {1}", args[0], args[1]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);

            computer.ip = (a != 0) ? b : computer.ip + 1 + ArgCount;
        }
    }

    public class JumpIfFalse : Command, iCommand
    {
        public const byte OPCODE = 6;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 2; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("JIF {0}? {1}", args[0], args[1]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);

            computer.ip = (a == 0) ? b : computer.ip + 1 + ArgCount;
        }
    }

    public class LessThan : Command, iCommand
    {
        public const byte OPCODE = 7;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 3; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("LT  {0},{1} >> {2}", args[0], args[1], args[2]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);

            computer.SetValue((a < b) ? 1 : 0, 3);

            computer.ip = computer.ip + 1 + ArgCount;
        }
    }

    public class IsEqual : Command, iCommand
    {
        public const byte OPCODE = 8;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 3; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("EQ  {0},{1} >> {2}", args[0], args[1], args[2]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            var b = computer.GetValue(2);

            computer.SetValue((a == b) ? 1 : 0, 3);

            computer.ip = computer.ip + 1 + ArgCount;
        }
    }

    public class IncrementBase : Command, iCommand
    {
        public const byte OPCODE = 9;
        public BigInteger Opcode { get { return OPCODE; } }
        public BigInteger ArgCount { get { return 1; } }

        public String Disassemble(List<BigInteger> code, int index)
        {
            var args = GetArguments(code.GetRange(index, (int)ArgCount + 1));
            return String.Format("IRB {0}", args[0]);
        }

        public void Execute(Computer computer)
        {
            var a = computer.GetValue(1);
            computer.rb += a;
            computer.ip = computer.ip + 1 + ArgCount;
        }
    }
}
