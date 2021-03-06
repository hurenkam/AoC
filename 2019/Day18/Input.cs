﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Day18
{
    public static class Input
    {
        public static String Data = @"#################################################################################
#l....#.........#.....#.....#...........#...........#.......#...#...............#
#.#.###.#####.#.#####.#.###.#.###.#####.#.#.#########.#####.#.#.#.#####.#.#######
#.#.....#.#...#i......#.#...#.Z.#.....#.#.#e......A...#...#.#.#..q#...#.#.#.....#
#.#####.#.#.###########.#.###.#######.###.#########.#####.#.#.#####.#.#.###.###.#
#.#...#.#.#.....#...#...#.....#.....#...#...#.....#.......#.#...#...#.#.#.....#.#
#.#.#.#.#.#####.#.#.#.#########.###.###.#.###.###.#######.#.#.###.###.#.#.#####F#
#.#.#.#.......#...#.#.#.#.......#.....#.#.#...#.#...#.....#.#.#...#.#...#u..#...#
#.#.#.#############.#.#.#.###########.#.###.###.###.###.###.#.#.###.###.###.#.#.#
#.#.#...............#.#.#.....#...#...#.#...#.....#...#.#.#...#.#.........#.#.#.#
#.#.###############.#.#.#####.#.#.###.#.#.###.#######.#.#.#####.#####.#####.#.###
#.#...#.......#.#...#...#...#...#...#.#.#...#.#...#...#.#.N...#.....#...#...#...#
#####.#.#####.#.#.#####.###.#####.#.#.#.###.#.#.#.#.###.#.#.#######.###.#.#####.#
#.....#.#.....#.#...#.......#...#.#.#.#.#...#...#.#.#...#.#...#...#...#.#.#.#...#
#.#####.#.#####T###.#.#######.#.###.#.#.#.###.###.#.###.#.###.#.#.###.###.#.#.#.#
#.......#.#.........#...#.....#.....#.#.#...#.#...#...#.#..x#...#.#...#...#.#.#.#
#.#######.###########.###.###########.#.###.###.#.###.#.#########.#.###.###.#.#.#
#.#...#...#.......#...#...#.......#...#d#.#.#...#...#.#...#.......#.........#.#.#
#.###.#.###.#####.#.###.###.#####.#.###.#.#.#.#####.#.###.#.###.#######.#####J#.#
#.....#g..#.#...#.#...#.#.#...#...#...#.#.#...#...#.#.#.....#.S.#.....#.#.....#.#
#####.###.#.###.#.#.###.#.#.#.#.#####.#.#.#####.###.#.###.#####.#.###.###.#######
#.......#.......#.#.#...#.#.#.#...#...#.#...#.......#...#.#...#.#...#...#.......#
#############.###.#.#####.#.#.###.#.###.###.#.#########.###.#######.###.#.#####.#
#.........#...#...#.....#.#.#.#.#.#.#...#...#.#...#...#...#.........#...#.#.#...#
#.#######.###.#.#######.#.#.#.#.#.#.#.#B#.###.#.#.#.#.###.###########.###.#.#.#.#
#.#.....#...#.#.......#.#.#.#.#...#.#.#.#.....#.#.#.#...#...........#.#...#.#.#.#
#.###.#.###.#######.#.#.#.#.#.#.###.###.#.#####.#.#.#.#.#########.#.#.#.###.#.###
#...#.#...#...#...#.#...#...#.#.#...#...#.#.....#...#.#.#.....#.#.#.#.#.....#...#
###.#.#.#####.#.#.#.#####.###.#.#.###.###.###########.#.###.#.#.#.#.#G#########.#
#...#.#.#...#...#.#...#...#...#.#.#.....#.#.........#.#.....#.#.#.#.#.....#.....#
#.###.#.#.#.#####.###.#####.#.###.#.###.#.#.#######.#.#######.#.#.#######.#.###.#
#.#...#...#.....#...#.......#.#...#.#...#.#.#...#.#.#.....#.#.#.#.......#.#...#.#
#.#########.#.#.###.###.#######.###.#.#.#.#.#.#.#.#.###.#.#.#.#.#######.#.#.#.###
#.#.......#.#.#.#.#.#...#.......#...#.#.#.#...#.#.#...#.#.#.#.....#.#...#.#.#...#
#.#.#####.###.#.#.#.#####.###########.#.#.#.###.#.###.#.#.#.#####.#.#.###.#.###P#
#.#...#.#.....#.#.#...#...#...........#.#.#...#.#...#.#.#.#.......#.#.....#.#...#
#.###.#.#######.#.###.#.#########.#####.#.#####.#.#.#.###.#.#######.#########.#.#
#...#.#...#.....#...#...#.......#...#...#...#...#.#.#.#...#...#...#...#.......#.#
###.#.#.###.#####.#.#####.#####.###.#.#####.#.###.###.#.#####.#.#.#.#.###.#####.#
#w....#...........#.......#.........#.........#.........#.......#...#.....#.....#
#######################################.@.#######################################
#...........#...K...#.......#.......#.....#.....#.........#.......#.............#
#.#######.###.#.#####.#####.#.###.###.#.#.#.#.###.#####.###.#.#.###.###########.#
#p......#.#...#.......#...#.#...#.#...#.#...#.........#.....#.#.#...#.........#.#
#######.#.#.###########.#.#.###.#.#.###.#.###################.###.###.#.###.###.#
#y....#.#...#.#.......#.#.#...#.#...#.#.#...#...#.#.........#.....#...#...#.#...#
#.#.###.#####.#.###.#.#.#.#.#.#.#####.#.###.#.#.#.#.#######.#.#####.#####.###.#.#
#.#.#...#...#...#...#.#h#...#.#...#...#.#.#.#.#.#.#.#...#...#.....#.#...#.#...#.#
#.#.#.###.#.#.###.#####.###.#####.#.###.#.#.#.#.#.#.#.#.###.#####.###.#.#.#.###.#
#.#.#.....#.#...#...#...#...#...#...#...#.#.#.#...#.#.#...#.....#.#...#...#.#...#
#M#.#######.###.###.#.###.###.#.#.###.###.#.#.###.#.#.###.#####.#.#.#####.#.#####
#.#.#.....#.#...#...#...#...#.#...#...#.#b..#...#.#...#z#.#.....#...#...#.#.....#
#.###.#.#.#.#####.#####.#####.#####.###.#.###.#.#######.#.#####.#####.###.#####.#
#...#.#.#.#.....#.....#.#...#.#.....#...#.#...#.........#.....#.#.#.........#...#
###.#.#.#.#####.###.#.#.#.#.#.#.#####.#.#.#####.#######.#####.#.#.#.#######.#.#.#
#...#.#.#.....#.....#.#...#...#...#...#.#.#...#.......#.....#.#.#...#...#...#.#.#
#.###O#.#####.#######.#######D###.#.###.#.#.#.###########.#.#.#.#####.#.#####H#.#
#.#...#.#.........#.....#...#...#.#...#.#...#.............#.#.#.......#.#...#.#.#
#.#.###.###########.#####.#.#####.###.#.###.#############.###.#########.#.#.#.#.#
#.#.#.#.#...#.......#.....#.......#...#.#...#.....#.......#...#.....#.....#...#k#
#.#.#.#.#.#.#.#######.#############.#####.###.###.#########.###.###.###########.#
#...#.#...#.#f#.....#.#.......#...#.....#.#...#.#.......#...#...#.#...#.....#.#.#
#.###.#####.#.#####.###.#####.#.#.#####.###.###.#######.#.#######.###.#.###.#.#.#
#.#...#..r..#.....#.....#.....#.#.......#...#.....#...#...#.........#...#...#.#.#
#.###.#V###.#####.#.#####.#####.#######.#.#######.#.#.###.#.#######.#####.###.#.#
#.#...#...#.....#...#...#...#...#.....#.#.#.....#.#.#.....#.....#.....#...#.....#
#.#.#####.#####.#####.#.###.###.#.#####.#.#.###.#.#.###########.#.###.#E###.#####
#m#.#.....#.#.....R...#..o#.#...#.#...#.#.#.#...#...#.........#.#.#.#.#.#.#...#.#
#.#.#.#####.#.###########.#.#.###.#.#.#.#.#.#.#######.#######.#.#.#.#.#.#.###.#.#
#...#.#.......#.....#...#.#.#.#.#...#.#.#...#.........#...#.#.#.#.#...#.#...#.#.#
#.###.#######.#.###.###.#.#.#.#.#.###.#.###############.#.#.#.#.#.#####.###.#.#.#
#.#...#.....#.#...#...#...#.#.#...#...#.#.........#.....#...#.I.#.#.Q.#...#.#...#
#.#.###.###.#####.###.#####.#.#.###.###.#.###.#####.###.###.#####.#.#.###.#.###.#
#.#v#...#.#.....#n..#.#...#.#.#...#.L...#...#.#.....#.#.#..j....#...#.#...#...#.#
#.#.#X###.#####.###.#.#.#.#.#.###.#########.#.#.#####.#.#.###########.#.###.#.#.#
#.#...#...Y.#.#.....#s#.#.#...#.#...#...#.#.#...#.....#.#...#.........#...#.#...#
#.#####.###.#.#######.#.#.#####.###.###.#.#.#####.###.#.#####.###########.#.#####
#.#.......#.#.....#.#.#.#.#.......#...#.#...#.....#.#.#.....#..a..........#.W...#
#.#######.#.###.#C#.#.#.#.#.###.#####.#.#.###.#####.#U#####.###################.#
#.........#c....#...#...#.....#.........#.....#t..........#.....................#
#################################################################################";
    }
}
