#!/usr/bin/env python3

#===================================================================================
def Part1():
    handlers = {
        "mask" : ProcessMask1,
        "mem" : ProcessMem1
    }

    for line in lines:
        cmd, address, value = ParseLine(line)
        handlers[cmd](address,value)

    return sum(mem1.values())


def ParseLine(line):
    address = None
    cmd, value = [part.strip() for part in line.split('=')]
    if ('[' in cmd):
        pos = cmd.find('[')
        address = int(cmd[pos+1:-1])
        cmd = cmd[:pos]

    return cmd,address,value   


ormask = 0
andmask = 0b111111111111111111111111111111111111
def ProcessMask1(_,value):
    global ormask
    global andmask
    ormask = int(value.replace('X','0'),2)
    andmask = int(value.replace('X','1'),2)


mem1 = {}
def ProcessMem1(address,value):
    tmp = (int(value) | ormask ) & andmask
    mem1[address] = tmp



#===================================================================================
def Part2():
    handlers = {
        "mask" : ProcessMask2,
        "mem" : ProcessMem2
    }

    for line in lines:
        cmd, address, value = ParseLine(line)
        handlers[cmd](address,value)

    return sum(mem2.values())


mask2 = 0
floats = []
def ProcessMask2(_,value):
    global mask2
    global floats
    floats = [(35-pos) for pos, char in enumerate(value) if char =='X']
    mask2 = int(value.replace('X','0'),2)


mem2 = {}
def ProcessMem2(address,value):
    for a in FindFloatingAddresses(address | mask2):
        mem2[a]=int(value)


allonemask = 0b111111111111111111111111111111111111
def FindFloatingAddresses(address):
    current = [address]
    for bit in floats:
        tmp = []
        for address in current:
            oraddress = 1 << bit
            andaddress = (allonemask ^ oraddress)
            tmp.append(address | oraddress)
            tmp.append(address & andaddress)

        current = tmp

    return current



#===================================================================================
print("Day 14")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
