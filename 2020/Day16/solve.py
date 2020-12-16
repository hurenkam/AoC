#!/usr/bin/env python3

#===================================================================================
def Part1():
    parsedRules = {}
    for rule in rules:
        key,ranges = ParseRule(rule)
        parsedRules [key] = ranges

    parsedNearby = [ParseTicket(ticket) for ticket in nearbytickets]

    numbers = []
    for parsedTicket in parsedNearby:
        invalidnumbers = FindInvalidNumbers(parsedTicket,parsedRules)
        numbers += invalidnumbers

    return sum(numbers)


def ParseRule(rule):
    key, tmp = rule.split(": ")
    valid = []
    for validrange in tmp.split(" or "):
        tmp = validrange.split('-')
        valid += range(int(tmp[0]),int(tmp[1])+1)

    return key,valid


def ParseTicket(ticket):
    return [int(value) for value in ticket.split(',')]


def FindInvalidNumbers(parsedTicket,parsedRules):
    results = []
    numbers = []
    for value in parsedTicket:
        result = False
        for rule in parsedRules.values():
            if value in rule:
                result = True

        results.append(result)
        if not result:
            numbers.append(value)

    return numbers


#===================================================================================
def Part2():
    parsedRules = {}
    for rule in rules:
        key,ranges = ParseRule(rule)
        parsedRules [key] = ranges

    parsedNearby = [ParseTicket(ticket) for ticket in nearbytickets]
    validNearby = FindValidTickets(parsedNearby,parsedRules)
    fieldmatches = FindMatchesForFields(validNearby,parsedRules)
    fieldmap = DiscoverFieldMap(fieldmatches)
    parsedTicket = ParseTicket(yourticket[0])

    result = 1
    for key in fieldmap.keys():
        if key.startswith("departure"):
            result *= parsedTicket[fieldmap[key]]

    return result


def FindValidTickets(parsedNearby,parsedRules):
    valid = []
    for parsedTicket in parsedNearby:
        invalidnumbers = FindInvalidNumbers(parsedTicket,parsedRules)
        if len(invalidnumbers) == 0:
            valid.append(parsedTicket)

    return valid


def FindMatchesForFields(validNearby,parsedRules):
    rulematches = {}
    for key in parsedRules.keys():
        matches = list(range(0,len(validNearby[0])))
        for ticket in validNearby:
            for i in range(0,len(ticket)):
                if not ticket[i] in parsedRules[key]:
                    matches.remove(i)

        rulematches[key]=matches

    print("RuleMatches:",rulematches)
    return rulematches


def DiscoverFieldMap(rulematches):
    foundmatches = {}
    remaining = len(rulematches.keys())
    while remaining > 0:
        keys = rulematches.keys()
        foundkeys = []
        foundindices = []
        for key in keys:
            if len(rulematches[key])==1:
                index = rulematches[key][0]
                foundmatches[key]=index
                foundindices.append(index)
                foundkeys.append(key)
        
        for key in foundkeys:
            rulematches.pop(key)

        for index in foundindices:
            for key in rulematches.keys():
                if index in rulematches[key]:
                    rulematches[key].remove(index)

        remaining = len(rulematches.keys())

    print("Found Matches:",foundmatches)
    return foundmatches


#===================================================================================
print("Day 16")

with open('input','r') as file:
    lines = [line.strip() for line in file]

    rules = []
    yourticket = []
    nearbytickets = []
    line = lines.pop(0).strip()
    while line != "":
        rules.append(line)
        line = lines.pop(0).strip()

    while line != "your ticket:":
        line = lines.pop(0).strip()
    line = lines.pop(0).strip()
    while line != "":
        yourticket.append(line)
        line = lines.pop(0).strip()

    while line != "nearby tickets:":
        line = lines.pop(0).strip()
    line = lines.pop(0).strip()
    while line != "":
        nearbytickets.append(line)
        if (len(lines)==0):
            break
        line = lines.pop(0).strip()

print("Part1: ",Part1())
print("Part2: ",Part2())
