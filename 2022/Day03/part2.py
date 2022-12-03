#!/bin/env python

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

def findBadge(packs):
    pack1 = packs.pop(0);
    pack2 = packs.pop(0);
    pack3 = packs.pop(0);
    for letter in pack1:
        if ((letter in pack2) and (letter in pack3)):
            return letter

def findAllBadges(packs):
    result = []
    while (len(packs)):
        badge = findBadge(packs)
        result.append(badge)
    return result

def determinePriorities(letters):
    priorities = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    result = []
    for letter in letters:
        result.append(priorities.index(letter))
    return result

badges = findAllBadges(lines)
priorities = determinePriorities(badges)

print(sum(priorities))
