#!/usr/bin/env python3

#===================================================================================
def Part1():
    groups = [FindAnyYes(group.split('\n')) for group in content.split('\n\n')]
    return sum( [len(group) for group in groups] )

def FindAnyYes(members):
    result = []

    for member in members:
        result = result + list( set(member) - set(result) )

    return result


#===================================================================================
def Part2():
    groups = [FindAllYes(group.split('\n')) for group in content.split('\n\n')]
    return sum( [len(group) for group in groups] )

def FindAllYes(members):
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
