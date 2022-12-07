#!/bin/env python

from collections import Counter

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

line = lines[0]

pos = 0;
while (pos < len(line)-4):
    part = line[pos:pos+4]
    count = len(Counter(part).keys())
    if (count == 4):
        break
    pos +=1

print(pos+4)
