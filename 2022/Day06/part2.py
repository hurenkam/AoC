#!/bin/env python

from collections import Counter

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

line = lines[0]

pos = 0;
while (pos < len(line)-14):
    part = line[pos:pos+14]
    count = len(Counter(part).keys())
    if (count == 14):
        break
    pos +=1

print(pos+14)
