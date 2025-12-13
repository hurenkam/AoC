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

fn parse_line(line: &String) -> Vec<(u64,u64)> {
    let mut result = vec![];
    let mut ranges = line.split(',');
    while let Some(range) = ranges.next() {
        let mut parts = range.split('-');
        let left = parts.next().expect("range must have a left part").parse::<u64>().expect("left part must be an int");
        let right = parts.next().expect("range must have a right part").parse::<u64>().expect("right part must be an int");
        result.push((left,right));
    }
    result
}

fn solution(lines: &Vec<String>, is_id_valid: &dyn Fn(u64) -> bool) -> u64 {
    let ranges = parse_line(
        lines.get(0).expect("file must contain at least 1 line"));

    let mut count = 0;
    for range in ranges {
        let (left,right) = range;
        for value in left..=right {
            if !is_id_valid(value) {
                //println!("invalid id: {}",value);
                count += value;
            }
        }
    }
    count
}

fn split_id(id: &String, size: usize) -> Vec<u64> {
    let mut result = vec![];
    let mut start = 0;
    let mut stop = size;
    while stop <= id.len() {
        let value = id[start..stop].parse::<u64>().expect("partial string should be a number");
        result.push(value);
        start += size;
        stop += size;
    }
    result
}

fn are_all_values_identical(values: Vec<u64>) -> bool {
    let len = values.len();
    if len < 2 { return false }

    let reference = values.get(0).unwrap();
    for value in &values {
        if *value != *reference { return false }
    }

    true
}

fn is_id_valid_1(id: u64) -> bool {
    let str = id.to_string();
    let len = str.len();
    if (len % 2) != 0 {
        return true;
    }

    let values = split_id(&str,len/2);
    if are_all_values_identical(values) {
        return false;
    }

    true
}

fn is_id_valid_2(id: u64) -> bool {
    let str = id.to_string();
    let len = str.len();

    for size in 1..len {
        if (len % size) == 0 {
            let values = split_id(&str,size);
            if are_all_values_identical(values) {
                return false;
            }
        }
    }

    true
}

fn main() {
    let input = read_input("./input.txt");
    println!("solution 1: {}", solution(&input, &is_id_valid_1));
    println!("solution 2: {}", solution(&input, &is_id_valid_2));
}
