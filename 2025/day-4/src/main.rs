use std::fs::File;
use std::io;
use std::io::BufRead;
use std::path::Path;
use array2d::Array2D;

fn read_lines(filename: &Path) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn read_input(filename: &str) -> Vec<String> {
    let path = Path::new(filename);
    read_lines(path)
}

#[derive(Copy,Clone,Debug,Eq,PartialEq)]
enum Position {
    Roll(bool),
    Empty
}

struct Map {
    map: Array2D<Position>
}
impl Map {
    fn new(lines: &Vec<String>) -> Self {
        let width = lines.get(0).unwrap().len();
        let height = lines.len();
        let mut map = Array2D::filled_with(Position::Empty,width,height);
        for (y,line) in lines.clone().into_iter().enumerate() {
            for (x,c) in line.chars().enumerate() {
                if c == '@' { map.set(x,y,Position::Roll(false)).expect("x and y must be within array bounds"); }
            }
        }

        Self {
            map
        }
    }

    fn is_roll(&self, x: i64, y: i64) -> bool {
        if x<0 || y <0 || x>(self.width()-1) || y>(self.height()-1) {
            return false
        }

        match *self.map.get(x as usize,y as usize).unwrap() {
            Position::Roll(_) => { true }
            Position::Empty => { false }
        }
    }

    fn count_neighbors(&self, x: i64, y: i64) -> usize {
        let mut count = 0;
        for i in y-1..=y+1 {
            for j in x-1..=x+1 {
                if !((x==j) && (y==i)) {
                    if self.is_roll(j,i) {
                        count += 1;
                    }
                }
            }
        }
        count
    }

    fn is_roll_that_can_be_moved(&self, x: i64, y: i64) -> bool {
        if !self.is_roll(x,y) { return false }
        let count = self.count_neighbors(x,y);
        if count > 3 { return false }
        true
    }

    fn update_movable_blocks(&mut self) {
        for y in 0..self.height() {
            for x in 0..self.width() {
                if self.is_roll_that_can_be_moved(x,y) { self.map.set(x as usize,y as usize,Position::Roll(true)).expect("unable to set position") }
            }
        }
    }

    fn count_movable_blocks(&self) -> u64 {
        let mut count = 0;
        for x in 0..self.width() {
            for y in 0..self.height() {
                if *self.map.get(x as usize,y as usize).unwrap() == Position::Roll(true) {
                    count += 1;
                }
            }
        }
        count
    }

    fn remove_movable_blocks(&mut self) {
        for x in 0..self.width() {
            for y in 0..self.height() {
                if *self.map.get(x as usize,y as usize).unwrap() == Position::Roll(true) {
                    self.map.set(x as usize,y as usize,Position::Empty).unwrap();
                }
            }
        }
    }

    fn width(&self) -> i64 { self.map.row_len() as i64 }
    fn height(&self) -> i64 { self.map.column_len() as i64 }
}

fn solution_1(lines: &Vec<String>) -> u64 {
    let mut map = Map::new(lines);
    map.update_movable_blocks();
    map.count_movable_blocks()
}

fn solution_2(lines: &Vec<String>) -> u64 {
    let mut map = Map::new(lines);
    let mut count = 0;
    loop {
        map.update_movable_blocks();
        let result = map.count_movable_blocks();
        map.remove_movable_blocks();
        count += result;

        if result == 0 { break; }
    }
    count
}

fn main() {
    let input = read_input("./input.txt");
    println!("solution 1: {}", solution_1(&input));  // 1395
    println!("solution 2: {}", solution_2(&input));  // 8451
}
