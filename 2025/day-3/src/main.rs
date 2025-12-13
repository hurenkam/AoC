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

fn find_highest_digit(input: &String) -> (usize,u8) {
    let mut pos = 0;
    let len = input.len();
    if len < 1 { panic!("string too small") }

    let mut value = input[0..1].parse::<u8>().expect("should be a digit");
    for p in 1..len {
        let v = input[p..=p].parse::<u8>().expect("should be a digit");
        if v > value {
            pos = p;
            value = v;
        }
    }
    (pos, value)
}

fn determine_power(input: &String, digits: u8) -> u64 {
    let len = input.len();
    let mut result: u64 = 0;
    let mut from = 0;

    for i in 0..digits {
        let to = len-digits as usize + i as usize;
        let s = &input[from..=to].to_string();

        let (pos,  value) = find_highest_digit(s);
        result = result * 10 + value as u64;
        from = from + pos + 1;
    }

    result
}

fn solution(lines: &Vec<String>, digits: u8) -> u64 {
    let mut value: u64 = 0;
    for line in lines {
        value += determine_power(line, digits);
    }
    value
}

fn main() {
    let input = read_input("./input.txt");
    println!("solution 1: {}", solution(&input, 2));  // 17613
    println!("solution 2: {}", solution(&input, 12)); // 175304218462560
}
