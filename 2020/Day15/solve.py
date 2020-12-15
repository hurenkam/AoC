#!/usr/bin/env python3

#===================================================================================
def Part1():
    return RunGame(2020)

def RunGame(stopat):
    global cache
    global turn
    cache = {}
    turn = 1
    lastnumber = None

    for i in startnumbers:
        lastnumber = GetNextNumber(i)

    while turn < stopat:
        lastnumber = GetNextNumber(lastnumber)

    return lastnumber


def GetNextNumber(lastnumber):
    global cache
    global turn
    nextnumber = 0
    if lastnumber in cache:
        nextnumber = turn - cache[lastnumber]

    cache[lastnumber] = turn
    turn += 1
    return nextnumber


#===================================================================================
def Part2():
    return RunGame(30000000)


#===================================================================================
print("Day 15")

examples = [
    [0,3,6],
    [1,3,2],
    [2,1,3],
    [1,2,3],
    [2,3,1],
    [3,2,1],
    [3,1,2]
]
input = [0,1,5,10,3,12,19]

startnumbers = input


print("Part1: ",Part1())
print("Part2: ",Part2())
