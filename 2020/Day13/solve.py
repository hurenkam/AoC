#!/usr/bin/env python3

#===================================================================================
def Part1():
    departures={}
    tmp = [int(bus) for bus in busses if bus !='x']
    for bus in tmp:
        departs = (int(arrival / bus) +1) * bus
        waittime = departs - arrival 
        departures[waittime] = bus

    best = min(departures.keys())
    return departures[best] * best

def FindEarliestDepartureTime(desired,bus):
    return (int(desired / bus) +1) * bus


#===================================================================================
def Part2():
    mods = {bus: -i % bus for i, bus in enumerate(busses) if bus != "x"}
    sortedbusses = list(reversed(sorted(mods)))

    t = mods[sortedbusses[0]]
    r = sortedbusses[0]
    for bus in sortedbusses[1:]:
        while t % bus != mods[bus]:
            t += r
        r *= bus

    return t

#===================================================================================
print("Day 13")

with open('input','r') as file:
    lines = [line.strip() for line in file]
    arrival = int(lines[0])
    busses = ["x" if x == "x" else int(x) for x in lines[1].split(",")]

print("Part1: ",Part1())
print("Part2: ",Part2())
