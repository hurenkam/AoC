#!/usr/bin/env python3
from computer import Computer

#===================================================================================
def Part1():
    _,acc = Computer().Run(program)
    return acc


#===================================================================================
def Part2():
    pos = 0
    while pos < len(program):
        tmp = program.copy()
        cmd = program[pos][0:3]
        args = program[pos][3:]

        if (cmd != "acc"):
            if (cmd=="jmp"):
                tmp[pos]= "nop"+args
            if (cmd=="nop"):
                tmp[pos]= "jmp"+args

            ip,acc = Computer().Run(tmp)
            if (ip >= len(tmp)):
                return acc

        pos += 1


#===================================================================================
print("Day 8")

with open('input','r') as file:
    program = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
