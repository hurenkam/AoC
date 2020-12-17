#!/usr/bin/env python3

#===================================================================================
def Part1():
    cube = Cube3D(lines)
    for _ in range(6):
        cube.CalculateNextGeneration()

    return len(cube._grid)


#===================================================================================
def Part2():
    cube = Cube4D(lines)
    for _ in range(6):
        cube.CalculateNextGeneration()

    return len(cube._grid)


#===================================================================================
class Cube:
    def __init__(self,lines):
        self._grid = []
        for y in range(len(lines)):
            self._parseline_(y,lines[y])


    def _parseline_(self,y,line):
        return


    def CalculateNextGeneration(self):
        pointstoexamine = []
        pointstoremove = []
        pointstoadd = []
        for point in self._grid:
            if not point in pointstoexamine:
                pointstoexamine.append(point)

            neighbors = self.FindNeighbors(point)
            for point in neighbors:
                if not point in pointstoexamine:
                    pointstoexamine.append(point)

        for point in pointstoexamine:
            count = self.CountActiveNeighbors(point)
            if point in self._grid:
                if (count < 2) or (count > 3):
                    pointstoremove.append(point)
            else:
                if (count == 3):
                    pointstoadd.append(point)

        for point in pointstoremove:
            self._grid.remove(point)

        for point in pointstoadd:
            self._grid.append(point)


    def FindNeighbors(self,point):
        return []


    def CountActiveNeighbors(self,point):
        neighbors = self.FindNeighbors(point)
        count = 0
        for point in neighbors:
            if point in self._grid:
                count += 1

        return count



#===================================================================================
class Cube3D(Cube):
    def __init__(self,lines):
        Cube.__init__(self,lines)


    def _parseline_(self,y,line):
        for x in range(len(line)):
            if line[x]=='#':
                point = (x,y,0)
                if not point in self._grid:
                    self._grid.append(point)


    def FindNeighbors(self,point):
        neighbors = []
        for x in range(-1,2):
            for y in range(-1,2):
                for z in range(-1,2):
                    neighbors.append((point[0]+x,point[1]+y,point[2]+z))

        neighbors.remove(point)
        return neighbors


#===================================================================================
class Cube4D(Cube):
    def __init__(self,lines):
        Cube.__init__(self,lines)


    def _parseline_(self,y,line):
        for x in range(len(line)):
            if line[x]=='#':
                point = (x,y,0,0)
                if not point in self._grid:
                    self._grid.append(point)


    def FindNeighbors(self,point):
        neighbors = []
        for x in range(-1,2):
            for y in range(-1,2):
                for z in range(-1,2):
                    for w in range(-1,2):
                        neighbors.append((point[0]+x,point[1]+y,point[2]+z,point[3]+w))

        neighbors.remove(point)
        return neighbors


#===================================================================================
print("Day 17")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
