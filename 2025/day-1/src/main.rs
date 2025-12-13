use std::fs::File;
use std::io;
use std::io::BufRead;
use std::path::Path;


fn read_lines(filename: &Path) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn read_input() -> Vec<String> {
    let path = Path::new("./input.txt");
    read_lines(path)
}

fn parse_line(line: String) -> (i32,i32) {
    let c = line.get(..1).expect("line must not be empty");
    let factor = match c {
        "L" => -1,
        "R" => 1,
        _ => { panic!("line must start with L or R character") }
    };
    let value = line
        .get(1..).expect("line must have more than 1 character")
        .parse::<i32>().expect("line must have a value directly following L or R");
    (factor,value)
}

fn solution_1(lines: &Vec<String>) {
    let mut dial = 50;
    let mut count = 0;
    for line in lines {
        let (factor,value) = parse_line(line.clone());

        let amount = factor*value;
        dial = (dial + amount) % 100;
        if dial == 0 {
            count += 1;
        }
    }
    println!("solution 1: {}", count);
}

fn update_2(mut dial: i32, factor: i32, mut value: i32) -> (i32, i32) {
    print!("update_2()   dial: {}, factor: {}, value: {} ",dial,factor,value);
    let mut count = 0;
    while value > 0 {
        value -= 1;
        dial = (dial + factor) % 100;
        if (dial < 0) {
            dial += 100;
        }
        if dial == 0 {
            count += 1;
        }
    }

    println!("dial: {}, count: {}", dial, count);
    (dial, count)
}

fn update_2b(mut dial: i32, factor: i32, mut value: i32) -> (i32, i32) {
    print!("update_2b()  dial: {}, factor: {}, value: {} ",dial,factor,value);
    let mut count = 0;

    let mut remainder = (dial + factor * value) % 100;
    let mut wraps = ((dial + factor * value) / 100).abs();
    if remainder < 0 {
        remainder += 100;
        if (dial != 0) {
            wraps += 1;
        }
    }
    dial = remainder;
    count += wraps;
    if (remainder == 0) && (wraps == 0) {
        count += 1;
    }

    println!("dial: {}, count: {}, remainder: {}, wraps: {}", dial, count, remainder, wraps);
    (dial, count)
}

fn solution_2(lines: &Vec<String>) {
    let mut dial = 50;
    let mut count = 0;
    for line in lines {
        let (factor,mut value) = parse_line(line.clone());
        let (new_dial_value, count_delta) = update_2(dial,factor,value);
        let (dial2b,delta2b) = update_2b(dial,factor,value);
        assert_eq!(new_dial_value,dial2b);
        assert_eq!(count_delta,delta2b);
        dial = new_dial_value;
        count = count + count_delta;
    }
    println!("solution 2: {}", count);
}

fn main() {
    let input = read_input();
    solution_1(&input);
    solution_2(&input);
}
