#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

packets = []
while len(lines):
    left = eval(lines.pop(0))
    right = eval(lines.pop(0))
    line = lines.pop(0)
    if line != '':
        raise Exception("expected blank line")
    packets.append(left)
    packets.append(right)

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

packets.append([[2]])
packets.append([[6]])
from functools import cmp_to_key
packets = sorted(packets,key=cmp_to_key(compare))
index2 = packets.index([[2]])+1
index6 = packets.index([[6]])+1
print(index2*index6)
