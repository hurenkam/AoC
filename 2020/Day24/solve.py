#!/usr/bin/env python3

#===================================================================================
tiles = []
def Part1():
    for line in lines:
        FlipTileByPath(tiles,line)
    return len(tiles)

def FlipTileByPath(tiles,line):
    actions = {
        "nw": (-1,-1,2),
        "ne": ( 1,-1,2),
        "w":  (-2, 0,1),
        "e":  ( 2, 0,1),
        "sw": (-1, 1,2),
        "se": ( 1, 1,2)
    }
    x = 0
    y = 0
    while len(line) > 0:
        for direction in actions.keys():
            if line.startswith(direction):
                dx,dy,dl = actions[direction]
                line = line[dl:]
                x+=dx
                y+=dy

    if (x,y) in tiles:
        tiles.remove((x,y))
    else:
        tiles.append((x,y))


#===================================================================================
def Part2():
    # Note: This is slow, can take up to several minutes
    #       Probably should create a number of worker threads equal to the amount
    #       of available cpu threads, and distribute the work
    for _ in range(100):
        remove = []
        add = []
        cache = []
        for tile in tiles:
            count = CountBlackNeighbors(tile)
            if count == 0 or count > 2:
                if tile not in remove:
                    remove.append(tile)

            for neighbor in FindNeighbors(tile):
                if neighbor not in tiles and neighbor not in cache:
                    cache.append(neighbor)
                    if CountBlackNeighbors(neighbor) == 2:
                        if neighbor not in add:
                            add.append(neighbor)

        for tile in remove:
            tiles.remove(tile)
        for tile in add:
            tiles.append(tile)

    return len(tiles)


def CountBlackNeighbors(tile):
    neighbors = FindNeighbors(tile)
    count = 0
    for neighbor in neighbors:
        if neighbor in tiles:
            count +=1

    return count


def FindNeighbors(tile):
    x,y = tile
    return [(x-1,y-1),(x+1,y-1),(x-2,y),(x+2,y),(x-1,y+1),(x+1,y+1)]


#===================================================================================
print("Day 24")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
