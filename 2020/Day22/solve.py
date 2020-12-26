#!/usr/bin/env python3

#===================================================================================
class Game:
    def __init__(self,deck1,deck2):
        self.deck1 = list(deck1)
        self.deck2 = list(deck2)


    def Run(self):
        while len(self.deck1) > 0 and len(self.deck2) > 0:
            self.PlayRound()

        if len(self.deck1) > 0:
            return 1

        return 2


    def PlayRound(self):
        card1 = self.deck1.pop(0)
        card2 = self.deck2.pop(0)
        winner = self.DetermineRoundWinner(card1, card2)
        if (winner == 1):
            self.deck1.append(card1)
            self.deck1.append(card2)
        else:
            self.deck2.append(card2)
            self.deck2.append(card1)


    def DetermineRoundWinner(self,card1,card2):
        if (card1 > card2):
            return 1

        return 2


    def Result(self):
        if len(self.deck1) == 0:
            winner = self.deck2
        else:
            winner = self.deck1

        i = 1
        result = 0
        while True:
            result += winner.pop() * i
            if not len(winner) > 0:
                break

            i += 1

        return result


#===================================================================================
def Part1():
    game = Game(players[0],players[1])
    _ = game.Run()
    return game.Result()



#===================================================================================
gamecount = 0
class RecursiveGame(Game):
    def __init__(self,deck1,deck2):
        global gamecount
        gamecount += 1
        self.gamecount = gamecount
        Game.__init__(self,deck1,deck2)
        self.history1 = []
        self.history2 = []


    def PlayRound(self):
        if self.HasDeckOccuredEarlierInGame():
            self.Player1Wins()
            return

        Game.PlayRound(self)


    def HasDeckOccuredEarlierInGame(self):
        if self.deck1 in self.history1 or self.deck2 in self.history2:
            return True

        self.history1.append(list(self.deck1))
        self.history2.append(list(self.deck2))
        return False


    def Player1Wins(self):
        self.deck2 = []


    def DetermineRoundWinner(self,card1,card2):
        if card1 <= len(self.deck1) and card2 <= len(self.deck2):
            return RecursiveGame(self.deck1,self.deck2).Run()

        if (card1 > card2):
            return 1

        return 2


#===================================================================================
def Part2():
    game = RecursiveGame(players[0],players[1])
    game.Run()
    return game.Result()



#===================================================================================
print("Day 22")

def ReadPlayer(lines):
    line = lines.pop(0)
    while not line.startswith("Player"):
        line = lines.pop(0)

    deck = []
    line = lines.pop(0)
    while not line == "":
        deck.append( int(line) )
        if not len(lines) > 0:
            break
        line = lines.pop(0)

    return deck

with open('input','r') as file:
    lines = [line.strip() for line in file]
    players = []
    players.append(ReadPlayer(lines))
    players.append(ReadPlayer(lines))

print("Part1: ",Part1())
print("Part2: ",Part2())
