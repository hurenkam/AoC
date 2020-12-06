#!/usr/bin/env python3

#===================================================================================
def Part1():
    groups = [ParseGroupPart1(group.strip()) for group in content.split('\n\n')]
    count = 0
    for group in groups:
        count += len(group)
    return count

def ParseGroupPart1(group):
    members = group.split('\n')
    result = []

    for member in members:
        result = result + list( set(member) - set(result) )

    return result


#===================================================================================
def Part2():
    groups = [ParseGroupPart2(group.strip()) for group in content.split('\n\n')]
    count = 0
    for group in groups:
        count += len(group)
    return count

def ParseGroupPart2(group):
    members = group.split('\n')
    result = list(members.pop(0))

    for member in members:
        result = list( set(result) - (set(result) - set(member)) )

    return result


#===================================================================================
print("Day 6")

with open('input','r') as file:
    content = file.read()

print("Part1: ",Part1())
print("Part2: ",Part2())
