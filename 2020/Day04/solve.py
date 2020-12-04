#!/usr/bin/env python3

#===================================================================================
def Part1():
    valid = 0

    for passport in passports:
        if (IsPassportValidPart1(passport)):
            valid += 1

    return valid

def IsPassportValidPart1(passport):
    required = ["byr","iyr","eyr","hgt","hcl","ecl","pid"]
    return all(item in passport.keys() for item in required)



#===================================================================================
def Part2():
    valid = 0

    for passport in passports:
        if (IsPassportValidPart2(passport)):
            valid += 1

    return valid


# byr (Birth Year)
# iyr (Issue Year)
# eyr (Expiration Year)
# hgt (Height)
# hcl (Hair Color)
# ecl (Eye Color)
# pid (Passport ID)
# cid (Country ID)
def IsPassportValidPart2(passport):
    required = ["byr","iyr","eyr","hgt","hcl","ecl","pid"]
    if not all(item in passport.keys() for item in required):
        return False

    if not IsYearValid(passport["byr"], 1920, 2002):
        return False

    if not IsYearValid(passport["iyr"], 2010, 2020):
        return False

    if not IsYearValid(passport["eyr"], 2020, 2030):
        return False

    if not IsHeightValid(passport["hgt"]):
        return False

    if not IsHairColorValid(passport["hcl"]):
        return False

    if not IsEyeColorValid(passport["ecl"]):
        return False

    if not IsPassportIdValid(passport["pid"]):
        return False

    return True

# four digits; at least <min> and at most <max>
def IsYearValid(year, min, max):
    if (len(year) != 4):
        return False

    year = int(year)
    if ((year < min) or (year > max)):
        return False

    return True

# a number followed by either cm or in:
#    If cm, the number must be at least 150 and at most 193.
#    If in, the number must be at least 59 and at most 76.
def IsHeightValid(hgt):
    post = hgt[-2:]
    pre = hgt[:-2]

    if (len(pre) < 1) or (len(post)!=2):
        return False

    if (post not in ["cm","in"]):
        return False

    for c in pre:
        if c not in "0123456789":
            return False

    number = int(pre)

    if ((post == "in") and (number >= 59) and (number <= 76)):
        return True

    if ((post == "cm") and (number >= 150) and (number <= 193)):
        return True

    return False

# a # followed by exactly six characters 0-9 or a-f.
def IsHairColorValid(ecl):
    if (len(ecl)!=7):
        return False

    if (ecl[0] != '#'):
        return False

    for c in ecl[1:]:
        if c not in "0123456789abcdef":
            return False

    return True

# exactly one of: amb blu brn gry grn hzl oth.
def IsEyeColorValid(ecl):
    eyecolors = ["amb","blu","brn","gry","grn","hzl","oth"]
    return ecl in eyecolors

# a nine-digit number, including leading zeroes.
def IsPassportIdValid(pid):
    if (len(pid)!=9):
        return False

    for c in pid:
        if c not in "0123456789":
            return False

    return True



#===================================================================================
print("Day 4")

def ParsePassport(unparsedPassport):
    passport = {}
    unparsedPassport = unparsedPassport.replace('\n',' ')
    data = [item.strip() for item in unparsedPassport.split(' ')]
    for item in data:
        if (':' in item):
            tmp = item.split(':')
            passport[tmp[0]]=tmp[1]

    return passport

with open('input','r') as file:
    passportdata = file.read()
    passports = [ParsePassport(passport) for passport in passportdata.split("\n\n")]

print("Part1: ",Part1())
print("Part2: ",Part2())
