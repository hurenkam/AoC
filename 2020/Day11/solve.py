#!/usr/bin/env python3

#===================================================================================
def Part1():
    nextgen = startmap.copy()
    history = []
    while True:
        nextgen = CalculateNextGenPart1(nextgen)
        h = CalculateHash(nextgen)
        if (h in history):
            return "".join(nextgen).count('#')

        history.append(h)


def CalculateHash(seatingmap):
    s = ''.join(seatingmap)
    return hash(s)


def CalculateNextGenPart1(seatingmap):
    result = []

    y=0
    for line in seatingmap:
        x=0
        newline=""
        for currentseatstate in line:
            newline += DetermineNextSeatStatePart1(currentseatstate,x,y,seatingmap)
            x+=1
        result.append(newline)
        y+=1

    return result


# If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
# If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
# Otherwise, the seat's state does not change.
def DetermineNextSeatStatePart1(currentseatstate,x,y,seatingmap):
    occupiedseatcount = CountOccupiedAdjacentSeats(x,y,seatingmap)
    if (currentseatstate=='L') and (occupiedseatcount == 0):
        return '#'
    if (currentseatstate=='#') and (occupiedseatcount >= 4):
        return 'L'
    return currentseatstate


def CountOccupiedAdjacentSeats(x,y,seatingmap):
    s=""
    for dy in range(y-1,y+2):
        if (dy>=0) and (dy<len(seatingmap)):
            for dx in range(x-1,x+2):
                if (dx>=0) and (dx<len(seatingmap[dy])):
                    if not ((x==dx) and (y==dy)):
                        s += seatingmap[dy][dx]

    return s.count('#')


#===================================================================================
def Part2():
    nextgen = startmap.copy()
    history = []
    while True:
        nextgen = CalculateNextGenPart2(nextgen)
        h = CalculateHash(nextgen)
        if (h in history):
            return "".join(nextgen).count('#')

        history.append(h)


def CalculateNextGenPart2(seatingmap):
    result = []
    y=0
    for line in seatingmap:
        x=0
        newline=""
        for currentseatstate in line:
            newline += DetermineNextSeatStatePart2(currentseatstate,x,y,seatingmap)
            x+=1
        result.append(newline)
        y+=1

    return result


# If a seat is empty (L) and there are no occupied seats visible in any of the 8 directions, the seat becomes occupied.
# If a seat is occupied (#) and five or more occupied seats are visible in any of the 8 directions, the seat becomes empty.
# Otherwise, the seat's state does not change.
def DetermineNextSeatStatePart2(currentseatstate,x,y,seatingmap):
    occupiedseatcount = CountSeatsIn8Directions(x,y,seatingmap)
    if (currentseatstate=='L') and (occupiedseatcount == 0):
        return '#'
    if (currentseatstate=='#') and (occupiedseatcount >= 5):
        return 'L'
    return currentseatstate


def CountSeatsIn8Directions(x,y,seatingmap):
    count = 0
    directions = [(-1,-1),(0,-1),(1,-1),(-1,0),(1,0),(-1,1),(0,1),(1,1)]
    for dx,dy in directions:
        count += CountSeatsInDirection(x,y,dx,dy,seatingmap)

    return count


def CountSeatsInDirection(x,y,dx,dy,seatingmap):
    curx = x+dx
    cury = y+dy
    count = 0
    if not IsPositionInRange(curx,cury):
        return 0

    c = seatingmap[cury][curx]
    if (c=='.'):
        count = CountSeatsInDirection(curx,cury,dx,dy,seatingmap)
    if (c=='#'):
        count = 1
    return count


def IsPositionInRange(x,y):
    if ((x<0) or (x>=len(startmap[0]))):
        return False

    if ((y<0) or (y>=len(startmap))):
        return False

    return True


#===================================================================================
print("Day 11")

with open('input','r') as file:
    startmap = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
