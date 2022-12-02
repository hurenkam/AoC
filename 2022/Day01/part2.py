#!/bin/env python

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

totals = [] 
while (len(lines)):
   totals.append(readElve(lines))

count = 0
total = 0
while (count < 3):
   value = max(totals)
   total += value
   totals.remove(value)
   count += 1

print(total)

