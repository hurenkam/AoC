#!/usr/bin/env python3

preamblelength = 25
inputfile = "input"

#===================================================================================
def Part1():
    return FindNotMatchingNumber()

def FindNotMatchingNumber():
    for index in range (preamblelength,len(numbers)):
        if not IsSumOfPreviousNumbers(index):
            return numbers[index]

def IsSumOfPreviousNumbers(index):
    number = numbers[index]
    for i in range (index-preamblelength,index):
        for j in range (i+1,index):
            if (numbers[i]+numbers[j]) == number:
                return True

    return False

#===================================================================================
def Part2():
    number = FindNotMatchingNumber()
    sumrange = FindRangeThatSumsUpTo(number)
    return min(sumrange) + max(sumrange)

def FindRangeThatSumsUpTo(number):
    for i in range (0,len(numbers)):
        total = numbers[i]
        for j in range (i+1,len(numbers)):
            total += numbers[j]
            if (number == total):
                return numbers[i:j+1]
            if (number < total):
                break

    return []


#===================================================================================
print("Day 9")

with open(inputfile,'r') as file:
    numbers = [int(line.strip()) for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
