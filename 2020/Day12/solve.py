#!/usr/bin/env python3

#===================================================================================
def Part1():
    global heading
    global pos
    heading = 90
    pos = [0,0]

    handlers = { 
        'L': TurnShip, 
        'R': TurnShip,
        'F': ForwardShip,
        'N': MoveShip,
        'E': MoveShip,
        'S': MoveShip,
        'W': MoveShip
    }

    for command in directions:
        cmd = command[0]
        amount = int(command[1:])
        handlers[cmd](cmd,amount)

    return abs(pos[0]) + abs(pos[1])

def TurnShip(cmd,amount):
    global heading
    turns = { 'L': -1, 'R': 1}
    heading = heading + turns[cmd] * amount

    if (heading < 0):
        heading += 360
    if (heading > 359):
        heading -= 360

def ForwardShip(cmd,amount):
    global heading
    global pos
    directions = { "N": [0,-1], "E": [1,0], "S": [0,1], "W": [-1,0] }
    headings = { 0:'N', 90: 'E', 180: 'S', 270: 'W' }

    delta = directions[headings[heading]]
    for i in range(0,2):
        pos[i] = pos[i] + delta[i] * amount

def MoveShip(cmd,amount):
    global pos
    directions = { "N": [0,-1], "E": [1,0], "S": [0,1], "W": [-1,0] }
    delta = directions[cmd]
    for i in range(0,2):
        pos[i] = pos[i] + delta[i] * amount


#===================================================================================
def Part2():
    global pos
    global wpt
    pos = [0,0]
    wpt = [10,1]
    
    handlers = { 
        'L': RotateWaypoint, 
        'R': RotateWaypoint,
        'F': MoveShipToWaypoint,
        'N': MoveWaypoint,
        'E': MoveWaypoint,
        'S': MoveWaypoint,
        'W': MoveWaypoint
    }

    for command in directions:
        cmd = command[0]
        amount = int(command[1:])
        handlers[cmd](cmd,amount)

    return abs(pos[0]) + abs(pos[1])

def RotateWaypoint(cmd,amount):
    global wpt
    multiplier = { 'R': [1,-1], 'L': [-1,1] }
    tmp = int(amount / 90)
    for _ in range(0,tmp):
        x=wpt[1] * multiplier[cmd][0]
        y=wpt[0] * multiplier[cmd][1]
        wpt = [x,y]

def MoveShipToWaypoint(cmd,amount):
    global pos
    global wpt
    for _ in range(0,amount):
        pos[0] += wpt[0]
        pos[1] += wpt[1]

def MoveWaypoint(cmd,amount):
    global wpt
    directions = { "N": [0,1], "E": [1,0], "S": [0,-1], "W": [-1,0] }
    delta = directions[cmd]
    for i in range(0,2):
        wpt[i] = wpt[i] + delta[i] * amount


#===================================================================================
print("Day 12")

with open('input','r') as file:
    directions = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())