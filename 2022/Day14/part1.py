#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

rocks = []
for line in lines:
    rock = []
    parts = line.split(" -> ")
    for part in parts:
        pos = tuple([int(p) for p in part.split(',')])
        rock.append(pos)
    rocks.append(rock)

scan = {} 
def createScan():
    global scan
    for rock in rocks:
        start = rock[0]
        for end in rock[1:]:
            if (end[0]==start[0]):
                # x matches, so iterate over y
                if (start[1] < end[1]):
                    for y in range(start[1],end[1]+1):
                        scan[(start[0],y)] = '#'
                else:
                    for y in range(end[1],start[1]+1):
                        scan[(start[0],y)] = '#'
            if (end[1]==start[1]):
                # y matches so iterate over x
                if (start[0] < end[0]):
                    for x in range(start[0],end[0]+1):
                        scan[(x,start[1])] = '#'
                else:
                    for x in range(end[0],start[0]+1):
                        scan[(x,start[1])] = '#'
            start = end

def findCorners():
    global left, right, top, bottom
    s = sorted(scan)
    left = s[0][0]
    right = s[len(s)-1][0]
    s = sorted(scan,key=lambda k: k[1])
    top = s[0][1]
    bottom = s[len(s)-1][1]

def dropSandParticle(pos=(500,0)):
    global stop
    while (pos[1] < bottom):

        if (pos[0],pos[1]+1) not in scan:
            pos=(pos[0],pos[1]+1)
        else:
            if (pos[0]-1,pos[1]+1) not in scan:
                pos=(pos[0]-1,pos[1]+1)
            else:
                if (pos[0]+1,pos[1]+1) not in scan:
                    pos=(pos[0]+1,pos[1]+1)
                else:
                    scan[pos] = 'o'
                    return True

    return False

createScan()
findCorners()

count = 0
while dropSandParticle():
    count += 1

print(count)
