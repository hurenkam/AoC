﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Day20
{
    public static class Input
    {
        public static String Example = @"         A           
         A           
  #######.#########  
  #######.........#  
  #######.#######.#  
  #######.#######.#  
  #######.#######.#  
  #####  B    ###.#  
BC...##  C    ###.#  
  ##.##       ###.#  
  ##...DE  F  ###.#  
  #####    G  ###.#  
  #########.#####.#  
DE..#######...###.#  
  #.#########.###.#  
FG..#########.....#  
  ###########.#####  
             Z       
             Z       
";

        public static String LargeExample = @"                   A               
                   A               
  #################.#############  
  #.#...#...................#.#.#  
  #.#.#.###.###.###.#########.#.#  
  #.#.#.......#...#.....#.#.#...#  
  #.#########.###.#####.#.#.###.#  
  #.............#.#.....#.......#  
  ###.###########.###.#####.#.#.#  
  #.....#        A   C    #.#.#.#  
  #######        S   P    #####.#  
  #.#...#                 #......VT
  #.#.#.#                 #.#####  
  #...#.#               YN....#.#  
  #.###.#                 #####.#  
DI....#.#                 #.....#  
  #####.#                 #.###.#  
ZZ......#               QG....#..AS
  ###.###                 #######  
JO..#.#.#                 #.....#  
  #.#.#.#                 ###.#.#  
  #...#..DI             BU....#..LF
  #####.#                 #.#####  
YN......#               VT..#....QG
  #.###.#                 #.###.#  
  #.#...#                 #.....#  
  ###.###    J L     J    #.#.###  
  #.....#    O F     P    #.#...#  
  #.###.#####.#.#####.#####.###.#  
  #...#.#.#...#.....#.....#.#...#  
  #.#####.###.###.#.#.#########.#  
  #...#.#.....#...#.#.#.#.....#.#  
  #.###.#####.###.###.#.#.#######  
  #.#.........#...#.............#  
  #########.###.###.#############  
           B   J   C               
           U   P   P               
";

        public static String Data = @"                                   Z     N     S       I   X A       Y Z         T                                   
                                   M     M     O       K   P A       N Z         E                                   
  #################################.#####.#####.#######.###.#.#######.#.#########.#################################  
  #.....#.......#...#.........#.#...........#.......#.#...#.....#.....#.....#.#...#...........#.............#.....#  
  #.#########.#####.#.#######.#.#####.#######.#####.#.###.#.#.#.###.#.###.###.###.#.###.###.###.###.#.###.#.#.#####  
  #.#...#.#...........#...#.................#...#...#.#...#.#.#.#.#.#...#.#.#.........#.#.#.......#.#...#.#.#.....#  
  #.###.#.#.#.#.#.###.#.#####.#######.#.#.###.#####.#.#.#####.#.#.#.###.#.#.#.#####.#.#.#.###.#.#############.#####  
  #...#.....#.#.#...#.#.....#.#.......#.#.#.#.....#.#...#.#...#.#.....#...#...#.#.#.#.#...#...#...........#...#.#.#  
  #.#####.#####.#.###.#####.#######.#.###.#.###.#.###.###.###.#######.#######.#.#.#######.#####.#####.#######.#.#.#  
  #.#...#.#.....#.#.#.#.#.#.#...#...#...#.#.#.#.#.#...#...#.......#.#.#...............#.....#.#.#.#.#.........#...#  
  #.#.#######.#.###.###.#.#.#.###.###.###.#.#.###.#.###.#.#######.#.#.###.#.#.#.#########.###.#.#.#.#######.#.###.#  
  #.#...#.....#.#...........#.#.#.#.#...#.#.......#...#.#.#.........#.#.#.#.#.#.........#.#...#.#.....#.#...#.#.#.#  
  #.#.#####.#######.###.###.#.#.###.#####.#.#########.#.#.#.#########.#.###.#########.###.#.#####.#####.#######.#.#  
  #...#...#.#.........#.#.#.....#.#.#.#...#.......#.#...#.#.....#.......#.#...#.#...#.#.#.#...#.....#.#...#.......#  
  ###.###.#########.###.#.#####.#.#.#.###.#####.###.###.#.#.###.###.#####.#.###.###.###.###.###.#.#.#.#.###.###.#.#  
  #.#...#.#.#...#...#...#.......#.#.#.........#...#.#.#.#.#...#...#...#.........#...#...........#.#.#.....#.#.#.#.#  
  #.###.#.#.#.###.#.#######.###.#.#.#####.#.#####.#.#.###.###.#.#.#.###.#.#.#####.#.#.###.#.#.#########.#####.###.#  
  #...#...#...#.#.#.#.........#...........#.#...........#.#.#.#.#.#.#.#.#.#...#...#.#...#.#.#.....#.#...#...#...#.#  
  #.###.#####.#.#####.#########.###.#.###.###.#.#.#.#####.#.#.#.#.#.#.#####.#####.###.#.#####.#####.###.#.#####.#.#  
  #.#.#...#.#.#.#...#.#...#.....#.#.#.#.#...#.#.#.#.#.......#.#.#.#.....#...#.........#.#.#.#.#.......#.....#.#...#  
  #.#.###.#.#.#.#.#######.#######.#####.#.#######.#####.#.#######.#.#.#####.#.###.#.#####.#.#.#.#########.###.#.#.#  
  #.....#...........#...#.......#.........#.#.....#...#.#.......#.#.#...#...#.#...#.......#.#.#.....#.#.#.#.....#.#  
  #.#.#####.#.#.#######.###.###.###.###.###.#####.###.###.#.#####.#.#######.#.#.###.#.#.#.#.###.#####.#.#.#####.###  
  #.#.......#.#.#.#.#.#.#.#.#.#.#.#.#.#...#.#.#...#...#...#...#.#.#.#.....#...#...#.#.#.#.#.#.....#.#...#.#...#...#  
  ###.#.#########.#.#.#.#.###.#.#.###.###.#.#.###.###.###.#.###.#.#.#.#####.#.#.#.#.#######.###.###.#.#.#.###.#.###  
  #...#.#.........#.......#...#.............#.....#.......#.#.#.#.#.....#.#.#.#.#.#.#...#...#.....#.#.#...#.#.....#  
  ###.###.#.#############.#.#########.###.#####.#.#######.#.#.#.#.#.#####.#######.###.#####.###.###.#####.#.#.###.#  
  #.#...#.#.#.....#.#...........#...#.#.#.....#.#.#.......#.#.....#...........#.........#.#.#...........#.......#.#  
  #.#.###.#####.###.#######.#.#####.#.#.###.#####.#.#########.###########.#.#####.#######.#.#.#####.#####.#.#.###.#  
  #...#.....#.#.....#...#...#...#.........#.#.....#.........#...#.........#...#.........#.......#.....#.#.#.#...#.#  
  ###.#.#####.#####.#.#####.#######.#####.#####.#########.###.#####.###########.#######.#####.#########.#######.###  
  #...#.#...#...#.#.......#...#    Z     A     X         V   T     H           X      #.#.....#...#.....#...#...#.#  
  ###.#.###.#.###.###.#####.###    W     C     P         P   A     H           Q      ###.###.#.###.#######.#.###.#  
  #...........#.#.#...#.#.#...#                                                       #.#.#...#.....#.#.#.#...#...#  
  #.###.#.#.###.#.#.###.#.#.###                                                       #.#####.#.#####.#.#.###.#.#.#  
  #...#.#.#.#.#...#.......#....VE                                                   WQ..#.......#.#.#.........#.#.#  
  ###.#.#####.###.#.###.###.###                                                       #.###.###.#.#.#.###.###.###.#  
VI..#.#...#...........#.#.....#                                                       #...#...#.........#...#...#..HH
  #.#.###.#.###.#.#.#####.#.###                                                       #.#####.#.#####.#.#####.###.#  
  #.....#...#...#.#.......#.#.#                                                       #.#...#.#...#...#.#...#.....#  
  #################.#####.###.#                                                       #.#.#.#.###########.###.#####  
  #.#...........#.#.#...#.....#                                                       #...#.....#.#.............#.#  
  #.###.###.#.#.#.#.###.#.#.###                                                       ###########.#####.#.#######.#  
  #.......#.#.#.#.....#.#.#...#                                                       #.......#...#.#.#.#.#.......#  
  #####.###.###########.#######                                                       #.#####.#.###.#.###.###.###.#  
NA....#.#...#.#.#...#.#.....#..UC                                                     #.....#...#.#.#...#.#.....#..TA
  #.#.#.#.###.#.#.###.#.#.#.#.#                                                       #####.#.###.#.#.#.#.#.#######  
  #.#...#...............#.#...#                                                     NW..#...#.....#.#.#.#.#.......#  
  #########.###################                                                       #.#.#.#.###.#.###.###.###.#.#  
  #.......#.#.........#.......#                                                       #...#.#.#.#...........#.#.#.#  
  #.#.#.#.###.###.###.#.###.###                                                       #########.#############.#####  
AC..#.#.#.......#.#.#...#.....#                                                       #.....#.......#.#...........#  
  #####.#####.#####.#.###.###.#                                                       #.###.#.###.#.#.###.#####.###  
  #.#.#...#.#...#.#.#.#.#.#....IK                                                     #.#...#...#.#.....#.....#...#  
  #.#.###.#.#####.#.###.#####.#                                                       #.#.###.#####.###.#.###.###.#  
  #.....#.#...........#...#...#                                                       #.#...#.#...#.#...#.#.....#..WQ
  #.#.#.###.#.###.###.#.#.#####                                                       #.###.#.###.#####.###.#######  
  #.#.#.....#.#...#.....#...#.#                                                     SH....#...#.....#.......#...#.#  
  #.###.#.#.###.#####.###.#.#.#                                                       #####.#####.###########.###.#  
GK....#.#.#...#.#.......#.#.#..VI                                                     #.#.....#.................#..SH
  #.###.#.#######.#.###.###.#.#                                                       #.#.#####.#####.#####.#.#.#.#  
  #...#.#.#...#.#.#...#.#.....#                                                       #...#.#.......#...#...#.#...#  
  #####.###.###.###########.#.#                                                       #####.#####.#########.###.#.#  
  #...#.#...#.....#.....#.#.#.#                                                       #...#.....#...#.#...#.#...#.#  
  #.#.#####.###.#.###.#.#.#####                                                       ###.#.###.#.###.###.#########  
  #.#.#.#.....#.#.#...#...#...#                                                     SX........#.......#.#.#.......#  
  #.###.#.#####.###.#.#.###.#.#                                                       #################.#.#####.###  
ZU....#.....#...#.#.#.#.....#.#                                                     PE....#...........#.#.#.#.....#  
  #.#####.#.#.#.#.###.#.#####.#                                                       ###.###.#.#.#.###.#.#.#.#.###  
  #.......#...#.......#.#...#..GK                                                     #...#...#.#.#.....#.....#...#  
  #######.#.#########.###.#####                                                       #.###.#######.#.###.#######.#  
RW....#...#.#.......#.#.......#                                                       #.......#.#.#.#...#.....#...#  
  ###.###.###.###.#.#####.###.#                                                       ###.#####.#.###.#.#####.###.#  
  #.....#.#.....#.#.......#.#.#                                                       #.#.#.....#.#.#.#.........#..ZX
  ###.#.#####.###.###.#.###.#.#                                                       #.#####.###.#.###############  
  #...#.....#...#.#...#.#......ZU                                                     #.#...#.............#.......#  
  #########.#.#.#.###.#.#####.#                                                       #.#.#.#.#########.###.#####.#  
  #.#.#.#.....#.#.#...#.#...#.#                                                     NA..#.#...#.......#...#...#....XQ
  #.#.#.#####.###########.#.#.#                                                       #.#.###############.#.#.###.#  
  #.....#...#.#...#.#.#...#...#                                                       #.#.............#.#.#.#.#.#.#  
  #.###.#.#.#####.#.#.#########                                                       #.#######.###.###.#.###.#.#.#  
  #...#...#...#...#.#.#.#.#.#..ZM                                                     #.........#...#.#.......#...#  
  #.###.#.#.#.###.#.#.#.#.#.#.#                                                       ###.###.#.#####.###.#.#####.#  
  #.#...#.#.#.#.#.#.......#...#                                                       #...#.#.#...#.......#.#.....#  
  #.#######.###.#.#.#####.#.###                                                       ###.#.#.#.###.#####.#.#####.#  
NW......#...............#.....#                                                       #.....#.#.#.#.#...#.#.#.#.#.#  
  #.#########.###.#####.###.###      T       Y     R       S     M         Z     N    #.#.#.#.#.#.#.###.#.###.#.###  
  #.....#.....#.......#.#.....#      E       N     W       O     H         X     M    #.#.#.#.#.#...#.............#  
  #.#.#.###.#####.#####.#.#.#.#######.#######.#####.#######.#####.#########.#####.#########.###.###.###.#####.#.#.#  
  #.#.#.#.......#...#...#.#.#.....#.#.#.....#...#.......#.#.#...#...#.......#.......#.....#...#...#.#.......#.#.#.#  
  ###.#.#.#.#.###.#.#.#.###.#.#####.#.#.#######.#.#####.#.#.###.#.#####.###.#.#.#.###.###.###.###.#####.###.###.###  
  #...#.#.#.#...#.#.#.#...#.#.#...#.......#.#...#.#...#.#...#.......#.....#.#.#.#.......#.#.#...#.#.......#.#.#.#.#  
  ###.#####.#.#.#.#.###.#.#.#####.#####.###.###.#.#.#####.#.###.#.###.#######.#.#.#.#.#####.###.###.#.#.#.###.###.#  
  #.......#.#.#.#.#.#.#.#.#.#...#...........#.#.#...#...#.#.#...#...#...#.....#.#.#.#.........#.#...#.#.#.........#  
  #.#.#.###.#.#.###.#.#########.#.#####.#####.#.#.#.#.#.#.#####.#######.#####.#######.#.#.#.#####.#.#.###.#####.###  
  #.#.#.#...#.#...#.#...#...#.....#.....#.#.....#.#.#.#...#.......#.......#.......#.#.#.#.#.#.....#.#...#.#...#...#  
  #####.#.#.#.#######.#.###.#########.###.#####.#.#######.#####.#####.#######.###.#.#.#.#######.#.###.###.###.#.###  
  #.#...#.#.#.#.#...#.#.#.#.#.....#.#...#...#.#.#.....#.....#.......#.....#...#.#...#.#.....#...#.#.....#.....#.#.#  
  #.###.#.###.#.###.#.###.#.#####.#.###.#.#.#.#.#.#######.#####.###.#.#####.#.#.#################.#.#.###.#######.#  
  #.....#...#.....#.........#.#.#.........#.#...#.#...#...#.#...#...#.#.....#.........#.....#.#...#.#...#.........#  
  ###.#####.###.#########.###.#.#####.#########.#.#.#.###.#.#####.###.#######.#####.#####.###.#.#.#.###.###.#.#.###  
  #.....#.#.#.....#.............#.....#...#...#.#.#.#.#.#.......#...#.#.#.#.....#.............#.#.#...#.#...#.#.#.#  
  ###.###.#.###.#.#.#########.#####.#.#.###.###.#.###.#.#.#########.#.#.#.###.###########.#######.#.###.###.#####.#  
  #...#.....#...#.#.#.........#.#.#.#.......#...#.......#...#.#...#.#.#...#...#.....#.#.#.......#.#.#.#...#...#...#  
  #.#####.#.#########.###.#.#.#.#.#####.###.###.#######.#.###.#.#.#.#.#.#.###.#####.#.#.#####.###.###.#.#.#.#.###.#  
  #.#.#...#.#.#.#.#.#.#.#.#.#...#.........#.#.#...#.....#...#...#...#...#.#...#.#...#.....#.....#...#.#.#.#.#.#...#  
  #.#.#.#.###.#.#.#.###.#####.#.#####.#######.#.#####.###.#####.#######.#####.#.#.#####.#########.###.#######.#.###  
  #.#...#...#.......#.#.#.....#.#...#...#.#.....#.......#.....#.#.....#.....#.....#.#...#.......#.#.......#.......#  
  #####.#####.#.#.###.#.#.###.#####.###.#.###.#.#.#######.#.###.#.#.#######.#.#####.#.#.#.###.#######.#.#####.#####  
  #.......#...#.#.#.#...#.#...............#...#.#.......#.#.#.....#.#...#...#.....#.#.#.....#.#.#.....#.....#.#.#.#  
  #.#######.#.###.#.###.#########.#########.#.#########.###.#######.#.#.#.###.#####.###.#######.###.#########.#.#.#  
  #.#.......#.#.......#.....#...#.#...#.#.#.#.#.......#.#...#.....#.#.#.....#...#...#.#...#.........#...#.#.#...#.#  
  #######.#.#.#.#.#.#.#.#####.###.#.#.#.#.###.###.#.###.#.###.###.#.#.#####.###.#.#.#.#.###.###.#.#.#.###.#.#####.#  
  #.......#.#.#.#.#.#...#.#...#...#.#...#...#.#...#.....#...#...#.#.#.....#...#...#...........#.#.#...............#  
  ###.#.#####.#####.#.#.#.#.#.###.#.###.#.#.#.###.#######.#.###.#.#.#.###.#########.#.#.#.###.#.###.#########.#.#.#  
  #...#.#.....#.....#.#.....#.......#...#.#...#.......#...#.#...#...#.#.....#.......#.#.#.#...#.#.........#...#.#.#  
  #####################################.#.###########.#####.#.#########.#####.#####################################  
                                       Z M           S     V U         V     P                                       
                                       W H           X     P C         E     E                                       
";
    };
}