#!/bin/env python3

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def readElve(lines):
    numbers = []
    total = 0
    line = lines.pop(0)
    while (line != ""):
        number = int(line)
        numbers.append(number)
        total += number
        line = lines.pop(0)
    return total

max = 0
count = 0
while (len(lines)):
   total = readElve(lines)
   if (total > max):
       max = total
   count += 1

print(max)
