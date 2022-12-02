#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

lookup = {
    "A X": "A C", 
    "A Y": "A A",
    "A Z": "A B",
    "B X": "B A",
    "B Y": "B B",
    "B Z": "B C",
    "C X": "C B",
    "C Y": "C C",
    "C Z": "C A" 
}

scores = {
    "A A": 4, 
    "A B": 8,
    "A C": 3,
    "B A": 1,
    "B B": 5,
    "B C": 9,
    "C A": 7,
    "C B": 2,
    "C C": 6
}

total = 0
while (len(lines)):
    line = lines.pop(0)
    score = scores[lookup[line]]
    total += score

print(total)

