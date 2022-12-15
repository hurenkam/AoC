#!/bin/env python
import sys

startLocations=[]
endLocations=[]
results=[]

sys.setrecursionlimit(10000)

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def findStartAndEndLocations():
    global lines
    global start
    global end
    y = 0
    for line in lines:
        x = 0
        for c in line:
            if c == 'E':
                startLocations.append((x,y))
            if c == 'a' or c == 'S':
                endLocations.append((x,y))
            x += 1
        y+=1

distances={}
def mapDistances(currentDistance,pos):
    (x,y) = pos
    if (pos in distances.keys()) and (distances[pos] <= currentDistance):
        return

    c = lines[y][x]
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

    if (toElevation-fromElevation) >= -1:
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

findStartAndEndLocations()
for start in startLocations:
    (x,y) = start
    distances={}
    mapDistances(0,start)
    for location in endLocations:
        if (location in distances):
            results.append(distances[location])
results.sort()
print(results[0])
