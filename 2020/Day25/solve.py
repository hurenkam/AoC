#!/usr/bin/env python3

#===================================================================================
def Part1():
    cardpublickey = int(lines[0])
    doorpublickey = int(lines[1])
    cardloopsize = FindLoopSizeForPublicKey(cardpublickey)
    return FindEncryptionKey(doorpublickey,cardloopsize)


def FindLoopSizeForPublicKey(publickey):
    value = 1
    loop = 1
    while True:
        value = UpdateValue(7,value)
        if (value == publickey):
            break
        loop += 1

    return loop


def UpdateValue(subject,value):
    divider = 20201227
    return (value * subject) % divider


def FindEncryptionKey(subject,loopsize):
    value = 1
    for _ in range(loopsize):
        value = UpdateValue(subject,value)

    return value


#===================================================================================
def Part2():
    return


#===================================================================================
print("Day 25")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
