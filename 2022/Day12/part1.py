#!/bin/env python

with open('input.txt','r') as file:
#with open('test.txt','r') as file:
    lines = [line.strip() for line in file]

def findStartAndEndLocations():
    global lines
    global start
    global end
    y = 0
    for line in lines:
        if 'S' in line:
            x = line.index('S')
            start = (x,y)
        if 'E' in line:
            x = line.index('E')
            end = (x,y)
        y+=1

distances={}
def mapDistances(currentDistance,pos):
    if (pos in distances.keys()) and (distances[pos] <= currentDistance):
        return

    if (not pos in distances.keys()) or (distances[pos] > currentDistance):
        distances[pos] = currentDistance

    up =    (pos[0],pos[1]-1)
    down =  (pos[0],pos[1]+1)
    left =  (pos[0]-1,pos[1])
    right = (pos[0]+1,pos[1])

    if isNotBlocked(pos,up):
        mapDistances(currentDistance+1,up)
    if isNotBlocked(pos,down):
        mapDistances(currentDistance+1,down)
    if isNotBlocked(pos,left):
        mapDistances(currentDistance+1,left)
    if isNotBlocked(pos,right):
        mapDistances(currentDistance+1,right)

def isNotBlocked(fromPos,toPos):
    (x,y) = toPos
    if (x<0) or (x>=len(lines[0])):
        return False
    if (y<0) or (y>=len(lines)):
        return False

    fromElevation = elevation(fromPos)
    toElevation = elevation(toPos)
    if (fromElevation == toElevation):
        return True

    if (toElevation-fromElevation) == 1:
        return True

    if (toElevation-fromElevation) <0:
        return True

    return False

def elevation(pos):
    (x,y) = pos
    c = lines[y][x]
    if c == 'S':
        c = 'a'
    if c == 'E':
        c = 'z'
    return ord(c) - ord('a')

import sys
sys.setrecursionlimit(10000)

start = ()
end = ()
findStartAndEndLocations()
mapDistances(0,start)
print(distances[end])

