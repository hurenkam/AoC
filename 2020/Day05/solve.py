#!/usr/bin/env python3

#===================================================================================
def Part1():
    return max(passes)


#===================================================================================
def Part2():
    _min = min(passes)
    _max = max(passes)
    _all = range(_min,_max)
    return list(set(_all) - set(passes))[0]


#===================================================================================
print("Day 5")

def ParseBoardingPass(line):
    line = line.replace('F','0')
    line = line.replace('B','1')
    line = line.replace('L','0')
    line = line.replace('R','1')
    return int(line,2)

with open('input','r') as file:
    passes = [ParseBoardingPass(line.strip()) for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
