#!/bin/env python

from collections import Counter

with open('input.txt','r') as file:
    lines = [line.strip() for line in file]

class File:
    def __init__(self,name,size):
        self.name = name
        self.size = int(size)

class Directory:
    def __init__(self,name,parent=None):
        self.name = name
        self.parent = parent
        self.dirs = {}
        self.files = {}

    def toString(self):
        result = ""
        if (self.parent!=None):
            return self.parent.toString() + self.name + "/"

        return self.name + '/'

    def size(self):
        total = 0
        for entry in self.dirs:
            total += self.dirs[entry].size()
        for entry in self.files:
            total += self.files[entry].size
        return total


root=Directory('')
cwd=root

def processLines(lines):
    line = lines.pop(0)
    if not ' ' in line:
        return

    parts = line.split(' ')
    if parts[0] != '$':
        raise Exception("Not a command")

    output = []
    while (len(lines)>0) and (lines[0][0] != '$'): 
        output.append(lines.pop(0))

    handleCommand(parts[1:],output)


def handleCommand(args,data):
    global cwd
    if args[0]=='cd':
        handleChangeDir(args[1])
    if args[0]=='ls':
        handleList(data)

def handleChangeDir(arg):
    global cwd
    if (arg=="/"):
        cwd = root
    else:
        if (arg==".."):
            cwd = cwd.parent
        else:
            if not arg in cwd.dirs:
               newdir = Directory(arg,cwd)
               cwd.dirs[arg] = newdir
               cwd = newdir

def handleList(data):
    global cwd
    for line in data:
        parts = line.split(' ')
        if (parts[0]!='dir'):
            size = parts[0]
            name = parts[1]
            cwd.files[name]=File(name,size)

dirs = {} 
def findDirectorySizes(cwd):
    global dirs

    if cwd is None:
        return
    
    dirs[cwd.toString()] = cwd.size()

    for directory in cwd.dirs:
        findDirectorySizes(cwd.dirs[directory])


def findSmallestDirWithSizeBiggerThan(size):
    current = "/"
    for directory in dirs:
        if ((dirs[directory] > size) and (dirs[directory] < dirs[current])):
            current = directory
    return current


while (len(lines)>0):
    processLines(lines)

findDirectorySizes(root)

total = 70000000
inuse = dirs['/']
free = total - inuse
required = 30000000
tobefreed = required - free

selected = findSmallestDirWithSizeBiggerThan(tobefreed)
print(dirs[selected])

