class Computer:
    def __init__(self):
        self.ip = 0
        self.acc = 0
        self.done = False
        self.instructions={
            "acc": self.Acc,
            "jmp": self.Jmp,
            "nop": self.Nop
        }

    def Run(self,program):
        linesexecuted=[]
        while ((not self.done) and (self.ip<len(program))):
            linesexecuted.append(self.ip)
            self._ExecuteInstruction(program[self.ip])
            self.ip += 1

            if (self.ip in linesexecuted):
                self.done = True

        return self.ip, self.acc

    def _ExecuteInstruction(self,instruction):
        tmp = instruction.split(' ')
        cmd = tmp.pop(0)
        if cmd in self.instructions:
            self.instructions[cmd](tmp)
        else:
            print("Warning: Ignoring unknown instruction", cmd, "at position",self.ip)


    #===================================================================================
    # Instruction Set
    #===================================================================================

    def Acc(self,args):
        delta = int(args[0])
        self.acc += delta

    def Jmp(self,args):
        delta = int(args[0])
        self.ip += (delta - 1)

    def Nop(self,args):
        return
