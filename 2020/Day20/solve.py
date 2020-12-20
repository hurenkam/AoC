#!/usr/bin/env python3
import copy
from math import prod

#===================================================================================
class Tile:
    def __init__(self,lines):
        self.id = None
        self.data = []
        self._ParseHeader(lines)
        self._ParseData(lines)

    def _ParseHeader(self,lines):
        header = lines.pop(0)
        if not header.startswith("Tile"):
            return

        self.id = int(header[4:-1])

    def _ParseData(self,lines):
        line = lines.pop(0)
        while line != "":
            self.data.append(line)
            if len(lines) > 0:
                line = lines.pop(0)
            else:
                line = ""

    def Rotate(self,rotation):
        rotation = rotation % 4
        if rotation >= 2:
            self.Flip(3)
            rotation -= 2

        if rotation == 1:
            tmp = []
            for x in range(len(self.data[0])):
                s = ""
                for y in range(len(self.data)):
                    s += self.data[y][x]
                tmp.insert(0,s)

            self.data = tmp

    def Flip(self,flip):
        tmp = self.data.copy()
        if (flip==1) or (flip==3):
            tmp.reverse()
            self.data = tmp

        if (flip==2) or (flip==3):
            tmp2 =[]
            for line in tmp:
                l = list(line)
                l.reverse()
                tmp2.append(''.join(l))
        
            self.data = tmp2

    def Print(self):
        print("Tile",self.id,":")
        for line in self.data:
            print(line)
        print("")

class Match:
    def __init__(self,id1,r1,f1,id2,r2,f2):
        self.id1 = id1
        self.r1 = r1
        self.f1 = f1
        self.id2 = id2
        self.r2 = r2
        self.f2 = f2

#===================================================================================
def Part1():
    global tiles
    global matches
    global corners

    tiles = ParseTiles(lines)
    matches = FindMatches(tiles)
    corners = FindCorners(matches)
    return prod(corners)


def FindCorners(matches):
    corners = []
    for match in matches.keys():
        if len(matches[match]) == 2:
            corners.append(match)

    return corners


def ParseTiles(lines):
    tiles = {}
    while len(lines) > 0:
        tile = Tile(lines)
        if tile.id is not None:
            tiles[tile.id]=tile

    return tiles


def FindMatches(tiles):
    matches = {}
    sortedkeys = sorted(list(tiles.keys()))
    for key1 in sortedkeys:
        matches[key1] = []
        for rotation1 in range(4):
            for flip1 in range(3):
                cmp1 = copy.deepcopy(tiles[key1])
                cmp1.Rotate(rotation1)
                cmp1.Flip(flip1)

                for key2 in sortedkeys:
                    if key2 != key1:
                        for rotation2 in range(4):
                            for flip2 in range(3):
                                cmp2 = copy.deepcopy(tiles[key2])
                                cmp2.Rotate(rotation2)
                                cmp2.Flip(flip2)

                                if cmp1.data[0] == cmp2.data[0]:
                                    if key2 not in matches[key1]:
                                        matches[key1].append(key2)

    return matches


#===================================================================================
def Part2():
    global tiles
    global matches
    global corners


#===================================================================================
print("Day 20")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
