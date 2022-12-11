#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def parseInput(lines):
    while len(lines):
        while not lines[0].startswith("Monkey"):
            lines.pop(0)
        parseMonkey(lines)

monkeys={}
def parseMonkey(lines):
    global monkeys
    index = parseIndex(lines.pop(0))
    items = parseStartItems(lines.pop(0))
    operation = parseOperation(lines.pop(0))
    divider = parseDivider(lines.pop(0))
    monkeytrue = parseTargetIndex(lines.pop(0))
    monkeyfalse = parseTargetIndex(lines.pop(0))
    monkeys[index] = { "index": index, "items": items, "operation": operation, "divider": divider, "targets": [monkeytrue,monkeyfalse], "inspections":0 }

def parseIndex(line):
    p1 = line.split(':')
    p2 = p1[0].split(' ')
    return int(p2[1])

def parseStartItems(line):
    p1 = line.split(':')
    p2 = p1[1].split(',') 
    items = []
    for item in p2:
        items.append(int(item))
    return items

def parseOperation(line):
    p1 = line.split(':')
    p2 = p1[1].split('=')
    p3 = p2[1].strip().split(' ')
    operator = p3[1].strip()
    if operator == '+':
        return (add,int(p3[2]))
    if operator == '*':
        if p3[2] == 'old':
            return (power,None)
        return (multiply,int(p3[2]))

    raise Exception("unsupported operation")

def power(old,arg):
    return old * old

def add(old,arg):
    return old + arg

def multiply(old,arg):
    return old * arg

def parseDivider(line):
    p1 = line.split(':')
    p2 = p1[1].split(' ')
    divider = int(p2.pop())
    return divider

def parseTargetIndex(line):
    p1 = line.split(':')
    p2 = p1[1].split(' ')
    index = int(p2.pop())
    return index

def doRound():
    monkeysToVisit = sorted(monkeys)
    for index in sorted(monkeys):
        doRoundForMonkey(index)

def doRoundForMonkey(index):
    items = monkeys[index]["items"]
    monkeys[index]["items"] = []
    for item in items:
        doItemForMonkey(item,index)

def doItemForMonkey(item,index):
    monkeys[index]["inspections"] += 1
    op = monkeys[index]["operation"][0]
    arg = monkeys[index]["operation"][1]
    item = int(op(item,arg) / 3)
    test = (item % monkeys[index]["divider"] == 0)
    if (test):
        target = monkeys[index]["targets"][0]
    else:
        target = monkeys[index]["targets"][1]
    monkeys[target]["items"].append(item)    
    

parseInput(lines)
for i in range(0,20):
    doRound()

inspections = []
for key in sorted(monkeys):
   inspections.append(monkeys[key]["inspections"])

inspections.sort()
l = len(inspections)
print(inspections[l-1]*inspections[l-2])
