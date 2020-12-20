#!/usr/bin/env python3
from math import prod

#===================================================================================
def Part1():
    values = []
    for line in lines:
        value = int(Evaluate1(line))
        values.append(value)

    return sum(values)

def Evaluate1(expression):
    i = expression.find('(')
    if i >= 0:
        j = FindMatchingBrace(expression)
        return Evaluate1(expression[:i] + Evaluate1(expression[i+1:j]) + expression[j+1:])
    else:
        i = FindOperator("+*",expression)
        o = expression[i]
        if i < 0:
            return expression

        v1 = int (expression[:i])
        j = FindOperator("+*",expression[i+1:])
        if j < 0:
            v2 = int (expression[i+1:])
            remaining = ""
        else:
            j = j + i + 1
            v2 = int (expression[i+1:j])
            remaining = expression[j:]

        if o == '+':
            result = str(v1+v2)
        if o == '*':
            result = str(v1*v2)

        if len(remaining)>0:
            return Evaluate1(result + remaining)

        return result

    return expression

def FindMatchingBrace(expression):
    level = 0
    i=0
    for c in expression:
        if c == '(':
            level += 1
        if c == ')':
            level -= 1
            if level == 0:
                return i
        i += 1
    return -1

def FindOperator(operators,expression):
    indices = [i for i, x in enumerate(expression) if x in operators]
    if len(indices) == 0:
        return -1

    index = min(indices)
    return index

#===================================================================================
def Part2():
    values = []
    for line in lines:
        value = int(Evaluate2(line))
        values.append(value)

    return sum(values)

def Evaluate2(expression):
    i = expression.find('(')
    if i >= 0:
        j = FindMatchingBrace(expression)
        return Evaluate2(expression[:i] + Evaluate2(expression[i+1:j]) + expression[j+1:])
    else:
        if '*' in expression:
            mults = expression.split('*')
            s = []
            for part in mults:
                s.append(int(Evaluate2(part)))
            return str(prod(s))
        else:
            if '+' in expression:
                adds = [int(i) for i in expression.split('+')]
                return str(sum(adds))

    return expression

#===================================================================================
print("Day 18")

with open('input','r') as file:
    lines = [line.strip() for line in file]

print("Part1: ",Part1())
print("Part2: ",Part2())
