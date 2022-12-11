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

def calculateScenicScore(forrest,x,y):
    current = forrest[y][x]
    left =    countVisibleTrees(forrest,current,x,y,-1,0)
    right =   countVisibleTrees(forrest,current,x,y,1,0)
    top =     countVisibleTrees(forrest,current,x,y,0,-1)
    bottom =  countVisibleTrees(forrest,current,x,y,0,1)
    result = left*right*top*bottom
    return left * right * top * bottom

def countVisibleTrees(forrest,current,x,y,dx,dy):
    height = len(forrest)
    width = len(forrest[0])
    count = 0
    x += dx
    y += dy
    if (x>=0) and (x<width) and (y>=0) and (y<height):
        if (forrest[y][x] < current):
            return 1 + countVisibleTrees(forrest,current,x,y,dx,dy)
        else:
            return 1

    return 0


forrest = buildMatrix()
height = len(forrest)
width = len(forrest[0])
score = 0
for y in range(0,height):
    for x in range(0,width):
        result = calculateScenicScore(forrest,x,y)
        if (result > score):
            score = result

print(score)
