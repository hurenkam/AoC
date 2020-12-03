#!/usr/bin/env python3

print("Day 3")

with open('input','r') as file:
    map = [line.strip() for line in file]



def CheckSlope(dx,dy):
    x = 0
    y = 0
    treecount = 0
    while y < len(map):
        p = x % len(map[y])
        if (map[y][p]=='#'):
            treecount += 1

        x+=dx
        y+=dy

    return treecount


def Part1():
    return CheckSlope(3,1)


def Part2():
    i1 = CheckSlope(1,1)
    i2 = CheckSlope(3,1)
    i3 = CheckSlope(5,1)
    i4 = CheckSlope(7,1)
    i5 = CheckSlope(1,2)
    return i1 * i2 * i3 * i4 * i5



print("Part1: ",Part1())
print("Part2: ",Part2())
