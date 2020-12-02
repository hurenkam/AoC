#!/usr/bin/env python3

print("Day 1")

with open('input','r') as file:
    numbers = [int(line.strip()) for line in file]


print("Input",numbers)

def Part1():
    for number1 in numbers:
        for number2 in numbers:
            if ((number1+number2)==2020):
                return number1*number2

def Part2():
    for number1 in numbers:
        for number2 in numbers:
            for number3 in numbers:
                if ((number1+number2+number3)==2020):
                    return number1*number2*number3

print("Part1: ",Part1())
print("Part2: ",Part2())


