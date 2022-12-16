#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def manhattanDistance(pos1,pos2):
    return abs(pos1[0]-pos2[0]) + abs(pos1[1]-pos2[1])

def findRanges(y):
    ranges = []
    for pos in sensors:
        distance = sensors[pos]
        delta = abs(y - pos[1])
        if (delta < distance):
           left = pos[0]-(distance-delta)
           right = pos[0]+(distance-delta)
           ranges.append((left,right))
    return ranges

def mergeRanges(ranges):
    ranges = sorted(ranges)
    result = []
    current = ranges.pop(0)
    while (len(ranges)):
        following = ranges.pop(0)
        if current[1] >= following[0]:
            current = (current[0],following[1])
        else:
            result.append(current)
            current = following
    result.append(current)
    return result

def countPositionsInRanges(ranges):
    count = 0
    for r in ranges:
        count += r[1]-r[0]+1
    return count

def countBeaconsInRanges(y,ranges):
    count = 0
    for beacon in set(beacons):
        if (beacon[1] == y):
            for r in ranges:
                if (beacon[0] in range(r[0],r[1]+1)):
                    count += 1
    return count

sensors = {}
beacons = []
for line in lines:
    (sensor,beacon) = line.split(':')
    parts = sensor[10:].split(', ')
    x = int(parts[0].split('=')[1])
    y = int(parts[1].split('=')[1])
    sensor = (x,y)
    parts = beacon[22:].split(', ')
    x = int(parts[0].split('=')[1])
    y = int(parts[1].split('=')[1])
    beacon = (x,y)
    beacons.append(beacon)
    distance = manhattanDistance(sensor,beacon)
    sensors[sensor] = distance

ranges = sorted(findRanges(2000000))
ranges = mergeRanges(ranges)
positions = countPositionsInRanges(ranges)
beacons = countBeaconsInRanges(2000000,ranges)
print(positions-beacons)

