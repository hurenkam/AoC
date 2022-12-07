#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip('\n') for line in file]

stacks = {
    1: [],
    2: [],
    3: [],
    4: [],
    5: [],
    6: [],
    7: [],
    8: [],
    9: [],
}

def processLine(line):
    for count in range(1,10):
        c = line[count*4-3]
        if (c != ' '):
            stacks[count].insert(0,line[count*4-3])
    
def readStacks():
    line = lines.pop(0)
    while line[1] != '1':
        print(line)
        processLine(line)
        line = lines.pop(0)

def printStacks():
    print("Stacks:")
    print("=========================")
    for stack in range(1,10):
        print(stack,stacks[stack])

def moveCrate(line):
    stripped = line.strip()
    if (stripped == ''):
        return

    parts = (line.strip()).split(' ')
    amount = int(parts[1])
    src = int(parts[3])
    dst = int(parts[5])
    move = stacks[src][amount*-1:]
    stacks[src]=stacks[src][:amount*-1]
    stacks[dst]=stacks[dst]+move
    printStacks()

readStacks()
printStacks()

while (len(lines)>0):
    moveCrate(lines.pop(0))

result = ""
for stack in range(1,10):
    result += stacks[stack].pop()

print(result)
