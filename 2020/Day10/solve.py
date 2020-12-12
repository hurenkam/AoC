#!/usr/bin/env python3

#===================================================================================
def Part1():
    index = 0
    jolts = 0
    result1 = 0
    result3 = 0
    while index < len(numbers):
        found = FindIndexesOfSuitableAdapters(jolts,index)
        next = numbers[found[0]]
        delta = next - jolts
        jolts = next
        index += 1

        if (delta==1):
            result1 += 1

        if (delta==3):
            result3 += 1

    return result1 * (result3+1)


def FindIndexesOfSuitableAdapters(jolts,index):
    result = []
    while index < len(numbers):
        if (numbers[index] <= (jolts+3)):
            result.append(index)

        if (numbers[index] > (jolts+3)):
            return result

        index += 1

    return result


#===================================================================================
pathmap = {}
def Part2():
    jolts = 0
    for i in range(0,len(numbers)):
        pathmap[i-1] = FindIndexesOfSuitableAdapters(jolts,i)
        jolts = numbers[i]

    return CountPaths(-1)


cache = {}
def CountPaths(index):
    if (index in cache):
        return cache[index]

    if (index not in pathmap):
        return 1

    total = 0
    for i in pathmap[index]:
        total += CountPaths(i)

    cache[index] = total
    return total


#===================================================================================
print("Day 10")

with open('input','r') as file:
    numbers = [int(line.strip()) for line in file]
    numbers.sort()
 
print("Part1: ",Part1())
print("Part2: ",Part2())
