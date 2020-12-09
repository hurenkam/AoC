#!/usr/bin/env python3

print("Day 3")

with open('input','r') as file:
    map = [line.strip() for line in file]



def NumberOfTreesOnSlope(slope):
    x = 0
    y = 0
    dx,dy = slope
    count = 0
    while y < len(map):
        p = x % len(map[y])
        if (map[y][p]=='#'):
            count += 1

        x+=dx
        y+=dy

    return count


def Part1():
    return CheckSlope(3,1)


def Part2():
    slopes = [(1,1),(3,1),(5,1),(7,1),(1,2)]
    product = 1
    for dx,dy in slopes:
        product = product * CheckSlope(dx,dy)

    return product


print("Part1: ",Part1())
print("Part2: ",Part2())
