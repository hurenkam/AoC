#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]


def buildMatrix():
    forrest = []
    for line in lines:
        treeline = []
        for tree in line:
            height = int(tree)
            treeline.append(height)
        forrest.append(treeline)
    return forrest

def isVisible(forrest,y,x):
    current = forrest[y][x]
    height = len(forrest)
    width = len(forrest[0])
    left =    maxHeight(forrest,y,y,0,x-1)
    right =   maxHeight(forrest,y,y,x+1,width-1)
    top =     maxHeight(forrest,0,y-1,x,x)
    bottom  = maxHeight(forrest,y+1,height-1,x,x)
    return ((current > left) or  (current > right) or  (current > top) or  (current > bottom))

def maxHeight(forrest,top,bottom,left,right):
    result = -1
    for y in range(top,bottom+1):
        for x in range(left,right+1):
            if (forrest[y][x]>result):
                result = forrest[y][x]
    return result


forrest = buildMatrix()
height = len(forrest)
width = len(forrest[0])
count = 0
for y in range(0,height):
    line = ""
    for x in range(0,width):
        if (isVisible(forrest,y,x)):
            count +=1
            line += 'v'
        else:
            line += '.'

print(count)
