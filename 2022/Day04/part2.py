#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def convertLineToRanges(lines):
    line = lines.pop(0)
    areas = line.split(',')
    ranges = []
    for area in areas:
        points = area.split('-');
        ranges.append([int(points[0]),int(points[1])])
    return ranges

def findRangesForAllPairs(lines):
    pairs = []
    while (len(lines)):
        pairs.append(convertLineToRanges(lines))
    return pairs

def findRangesWhichOverlap(pairs):
    count = 0
    for pair in pairs:
        range0 = range(pair[0][0],pair[0][1]+1)
        range1 = range(pair[1][0],pair[1][1]+1)
        if (pair[0][0] in range1) or (pair[0][1] in range1):
            count +=1
        else:
            if (pair[1][0] in range0) or (pair[1][1] in range0):
                count +=1
    return count

pairs = findRangesForAllPairs(lines)
count = findRangesWhichOverlap(pairs)
print(count)
