#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def splitLines(lines):
    result = []
    for line in lines:
        result.append([line[:len(line)/2],line[len(line)/2:]])
    return result

def findLetters(packs):
    result = []
    for pack in packs:
        letters = []
        for letter in pack[0]:
            if ((letter in pack[1]) and not (letter in letters)):
                letters.append(letter)
        for letter in letters:
            result.append(letter)
    return result

def determinePriorities(letters):
    priorities = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    result = []
    for letter in letters:
        result.append(priorities.index(letter))
    return result

packs = splitLines(lines)
letters = findLetters(packs)
priorities = determinePriorities(letters)

print(sum(priorities))
