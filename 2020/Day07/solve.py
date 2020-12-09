#!/usr/bin/env python3

#===================================================================================
def Part1():
    #print("Parsed: ", parsed)
    # Find bags that eventually hold at least one 'shiny gold' bag
    found = FindParents(["shiny gold"],[])
    return len(found)

def FindParents(keys,found):
    #print("FindParents:",keys)
    bags = []
    for key in keys:
        for bag in parsed.keys():
            if ((key in parsed[bag]) and not (bag in bags)):
                bags.append(bag)

    for bag in bags:
        if bag not in found:
            found.append(bag)
    
    if len(bags) > 0:
        found = FindParents(bags,found)

    return found


#===================================================================================
def Part2():
    #print("Parsed:", parsed)
    return CountBags('shiny gold')

def CountBags(key):
    #print("CountBags(",key,")")
    count = 0
    for bag in parsed[key]:
        count += parsed[key][bag] + parsed[key][bag]*CountBags(bag)
        #count += CountBags(bag)

    #print("Result:", count)
    return count


#===================================================================================
print("Day 7")

def SplitUp(item):
    cleanitem = item.replace(".", "").replace(" bags", "").replace(" bag", "")
    key = cleanitem[2:]
    value = int(cleanitem[:2])
    return key,value

def ParseLine(line):
    result = {}
    tmp = line.split(" bags contain ")
    key = tmp[0]
    if (tmp[1] != "no other bags."):
        tmp = tmp[1].split(", ")
        for item in tmp:
            bag,count = SplitUp(item)
            result[bag] = count

    return key, result

with open('input','r') as file:
    lines = [line.strip() for line in file]
    parsed = {}
    for line in lines:
        key, result = ParseLine(line)
        parsed[key]= result

print("Part1: ",Part1())
print("Part2: ",Part2())
