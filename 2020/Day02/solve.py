#!/usr/bin/env python3

print("Day 2")

with open('input','r') as file:
    entries = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())



def Part1():
    validcount = 0
    for entry in entries:
        min, max, char, password = ParseEntry(entry)
        if (IsValidPasswordPart1(min,max,char,password)):
            validcount+=1

    return validcount


def ParseEntry(entry):
    t1 = entry.split(':')
    t2 = t1[0].split(' ')
    t3 = t2[0].split('-')
    first = int(t3[0].strip())
    second = int(t3[1].strip())
    char = t2[1].strip()
    password = t1[1].strip()
    return first,second,char,password


def IsValidPasswordPart1(min,max,char,password):
    count = password.count(char)
    return ((count >= min) and (count <= max))



def Part2():
    validcount = 0
    for entry in entries:
        first, second, char, password = ParseEntry(entry)
        if (IsValidPasswordPart2(first,second,char,password)):
            validcount+=1

    return validcount


def IsValidPasswordPart2(first,second,char,password):
    if ((password[first-1]==char) and (password[second-1]!=char)):
        return True

    if ((password[first-1]!=char) and (password[second-1]==char)):
        return True

    return False
