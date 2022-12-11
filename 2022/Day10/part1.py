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
        count -= 1
        cyclecount += 1
        if (((cyclecount+20)%40)==0):
            value = cyclecount * x
            signal.append(value)

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

processLines(lines)

total = 0
for s in signal:
    total += s

print(total)
