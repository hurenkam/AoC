#!/bin/env python

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

smalldirs = []
def findSmallDirectories(cwd):
    global smalldirs

    if cwd is None:
        return
    
    if (cwd.size() < 100000):
        smalldirs.append(cwd)

    for directory in cwd.dirs:
        findSmallDirectories(cwd.dirs[directory])



while (len(lines)>0):
    processLines(lines)

findSmallDirectories(root)

total = 0
for directory in smalldirs:
    total += directory.size()


print(total)
