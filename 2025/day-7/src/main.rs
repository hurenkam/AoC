use std::collections::HashMap;
use std::fs::File;
use std::io;
use std::io::BufRead;
use array2d::Array2D;
use itertools::Itertools;

fn read_lines(filename: &str) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

struct Manifold {
    map: Array2D<char>,
    cache: HashMap<(usize, usize), usize>
}
impl Manifold {
    fn new(lines: Vec<String>) -> Manifold {
        let rows = lines.iter().map(|row| row.chars().collect::<Vec<_>>()).collect::<Vec<_>>();
        let map = Array2D::from_rows(&rows).unwrap();
        let cache = HashMap::new();
        Self { map, cache }
    }

    fn find_start_position(&self) -> (usize, usize) {
        for (i, row) in self.map.rows_iter().enumerate() {
            for (j, c) in row.enumerate() {
                if *c == 'S' { return (i,j) }
            }
        }
        panic!("no start position found");
    }

    fn propagate_beams(&self, y: usize, beams: &mut Vec<usize>) -> usize {
        let mut result = 0;
        let mut new_beams: Vec<usize> = vec![];
        for x in beams.clone() {
            if *self.map.get(y,x).unwrap() == '^' {
                result += 1;
                if x>0 { new_beams.push(x-1); }
                if x<self.map.row_len()-1 { new_beams.push(x+1); }
            } else {
                new_beams.push(x);
            }
        };

        // make sure there are no duplicates in the collection
        *beams = new_beams.iter().unique().map(|&i|i).collect::<Vec<_>>();
        result
    }

    fn count_tachyon_splits(&self) -> usize {
        let (y,x) = self.find_start_position();
        let mut beams = vec![x];

        (y..self.map.column_len()-1).enumerate().map(|(y,_)|
            self.propagate_beams(y+1,&mut beams)
        ).collect::<Vec<_>>().iter().sum::<usize>()
    }

    fn count_realities(&mut self,from: (usize,usize)) -> usize {
        if self.cache.keys().contains(&from) {
            return *self.cache.get(&from).unwrap();
        }

        let mut result = 0;
        let (mut y,x) = from;

        while let Some(c) = self.map.get(y,x) {
            if c != &'^' {
                y += 1;
            } else {
                if x>0 {
                    result += self.count_realities((y,x-1));
                }
                if x<self.map.row_len()-1 {
                    result += self.count_realities((y,x+1));
                }
                self.cache.insert(from,result);
                return result;
            }
        }

        self.cache.insert(from,1);
        1
    }
}

fn main() {
    let input = read_lines("./input.txt");
    let mut manifold = Manifold::new(input);

    println!("solution 1: {}",manifold.count_tachyon_splits()); // 1642
    println!("solution 2: {}",manifold.count_realities(manifold.find_start_position())); // 47274292756692
}
