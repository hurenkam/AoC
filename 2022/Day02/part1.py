#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

scores = {
    "A X": 4, 
    "A Y": 8,
    "A Z": 3,
    "B X": 1,
    "B Y": 5,
    "B Z": 9,
    "C X": 7,
    "C Y": 2,
    "C Z": 6
}

total = 0
while (len(lines)):
    line = lines.pop(0)
    score = scores[line]
    total += score

print(total)

