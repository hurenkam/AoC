#!/usr/bin/env python3

class Game:
    def __init__(self,cups):
        self.cups = cups


    def PlayRound(self):
        pickedup = self.PickupCups()
        destination = self.SelectDestinationCup()
        self.PlaceCupsBack(destination,pickedup)
        self.SelectNewCup()


    def PickupCups(self):
        result = self.cups[1:4]
        del self.cups[1:4]
        return result


    def SelectDestinationCup(self):
        label = self.cups[0] -1
        while not label in self.cups:
            label -= 1
            if label < min(self.cups):
                label = max(self.cups)

        return label


    def PlaceCupsBack(self,destination,cups):
        index = self.cups.index(destination)
        if index+1 == len(self.cups):
            for cup in cups:
                self.cups.append(cup)
                index += 1
        else:
            for cup in cups:
                self.cups.insert(index+1,cup)
                index += 1


    def SelectNewCup(self):
        cup = self.cups.pop(0)
        self.cups.append(cup)


    def Result(self):
        index = self.cups.index(1)
        tmp = self.cups[index:] + self.cups[:index]
        _ = tmp.pop(0)
        tmp = [str(i) for i in tmp]
        return "".join(tmp)


#===================================================================================
def Part1():
    game = Game(cups)
    for _ in range(100):
        game.PlayRound()

    return game.Result()


#===================================================================================
def Part2():
    return


#===================================================================================
print("Day 23")

with open('input','r') as file:
    lines = [line.strip() for line in file]
    cups = [int(c) for c in lines[0]]
    print("Cups:", cups)

print("Part1: ",Part1())
print("Part2: ",Part2())
