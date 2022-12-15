#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

pairs = []
while len(lines):
    left = eval(lines.pop(0))
    right = eval(lines.pop(0))
    line = lines.pop(0)
    if line != '':
        raise Exception("expected blank line")
    pairs.append((left,right))

def areInRightOrder(left,right):
    result = compare(left,right)
    return (result <= 0)

def compare(left,right):
    if (type(left) in (int,) and type(right) in (int,)):
        return compareInt(left,right)

    if type(left) in (int,):
        left = [left]

    if type(right) in (int,):
        right = [right]

    return compareList(left,right)

def compareInt(left,right):
    if (left < right):
        return -1
    if (left > right):
        return 1
    return 0

def compareList(left,right):
    lenl = len(left)
    lenr = len(right)
    count = 0
    while (count < lenl) and (count < lenr):
        result = compare(left[count],right[count])
        if (result != 0):
            return result
        count+= 1

    if (lenl < lenr):
        return -1
    if (lenl > lenr):
        return 1
    return 0

index = 1
indices = []
for pair in pairs:
    if areInRightOrder(pair[0],pair[1]):
        indices.append(index)
    index +=1

print(sum(indices))
