#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

cyclecount = 0
x = 1
signal = []

def addx(args):
    global x
    addCycles(2)
    x += int(args[0])

def noop(args):
    addCycles(1)

def addCycles(count):
    global cyclecount
    while (count):
        pos = (cyclecount % 40)
        if (pos==0):
            print('')
        if (pos == x-1) or (pos == x) or (pos == x+1):
            print('#',end='')
        else:
            print('.',end='')
        count -= 1
        cyclecount += 1

def processLines(lines):
    cmdhandlers = {
        "addx": addx,
        "noop": noop
    }
    for line in lines:
        args = line.split(' ')
        cmd = args.pop(0)
        if (cmd in cmdhandlers):
            cmdhandlers[cmd](args)
    print('')

processLines(lines)

total = 0
for s in signal:
    total += s

