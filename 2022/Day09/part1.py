#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

head = [0,4]
tail = [0,4]
start = [0,4]

def walk(line):
    parts = line.split(' ')
    direction = parts[0]
    count = int(parts[1])
    directions = {
        'U': (0,-1),
        'D': (0,1),
        'L': (-1,0),
        'R': (1,0)
    }
    delta = directions[direction]
    move(delta,count)

tailpositions = [(0,4)]
def move(delta,count):
    global head
    global tail
    global tailpositions
    while (count > 0):
        step(delta)
        count -= 1

def step(delta):
    global head
    global tail
    global tailpositions
    (x,y) = delta
    head[0] += x
    head[1] += y
    deltax = head[0] - tail[0]
    deltay = head[1] - tail[1]
    if (abs(deltax) > 1) or (abs(deltay) > 1):
        if (deltax > 0):
            tail[0] += 1
        if (deltax < 0):
            tail[0] -= 1
        if (deltay > 0):
            tail[1] += 1
        if (deltay < 0):
            tail[1] -= 1
        position = (tail[0],tail[1])
        if (position not in tailpositions):
            tailpositions.append(position)
        

for line in lines:
    walk(line)

print(len(tailpositions))
