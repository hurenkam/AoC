use std::fs::File;
use std::io;
use std::io::BufRead;
use std::path::Path;

fn read_lines(filename: &Path) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn read_input(filename: &str) -> Vec<String> {
    let path = Path::new(filename);
    read_lines(path)
}

fn solution_1(input: &Vec<String>) -> u64 {
    let mut count = 0;

    count
}

fn solution_2(input: &Vec<String>) -> u64 {
    let mut count = 0;

    count
}

fn main() {
    let input = read_input("./input.txt");

    println!("solution 1: {}", solution_1(&input));  // 737
    println!("solution 2: {}", solution_2(&input));   // 357485433193284
}
