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
    map: Array2D<char>
}
impl Manifold {
    fn new(lines: Vec<String>) -> Manifold {
        let rows = lines.iter().map(|row| row.chars().collect::<Vec<_>>()).collect::<Vec<_>>();
        let map = Array2D::from_rows(&rows).unwrap();
        Self { map }
    }

    fn dump(&self) {
        self.map.rows_iter().for_each(|row| println!("{}", row.collect::<String>()));
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

        *beams = new_beams.iter().unique().map(|&i|i).collect::<Vec<_>>();
        result
    }

    fn follow_tachyons(&self) -> usize {
        let (mut y,x) = self.find_start_position();
        let mut beams = vec![x];
        let mut count = 0;
        while y < self.map.column_len()-1 {
            y+=1;
            count += self.propagate_beams(y,&mut beams);
        }
        count
    }
}

fn main() {
    let input = read_lines("./input.txt");
    let manifold = Manifold::new(input);
    //manifold.dump();

    println!("solution 1: {}",manifold.follow_tachyons()); //
    println!("solution 2: {}",0); //
}
