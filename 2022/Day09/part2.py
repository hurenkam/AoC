#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

rope = []
tailpositions = [(0,4)]
def initRope():
    for i in range(0,10):
        rope.append([0,4])

def parseCommand(line):
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

def move(delta,count):
    global rope
    global tailpositions
    while (count > 0):
        step(delta)
        count -= 1

def step(delta):
    global head
    global tail
    global tailpositions
    (x,y) = delta
    rope[0][0] += x
    rope[0][1] += y
    for i in range(1,10):
        updateKnot(i)

    tail = (rope[9][0],rope[9][1])
    if (tail not in tailpositions):
        tailpositions.append(tail)

def updateKnot(knot):
    head = rope[knot-1]
    tail = rope[knot]
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
        rope[knot][0] = tail[0]
        rope[knot][1] = tail[1]

initRope()
for line in lines:
    parseCommand(line)

print(len(tailpositions))
