use std::fs::File;
use std::io;
use std::io::BufRead;
use array2d::Array2D;

fn read_lines(filename: &str) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn parse_input_1(input: &Vec<String>) -> Vec<(char,Vec<i64>)> {
    let mut result = vec![];

    let mut lines = input[..input.len()-1].iter();
    let mut rows = vec![];
    while let Some(line) = lines.next() {
        rows.push(line.split_ascii_whitespace()
            .map(|value| value.parse::<i64>().unwrap())
            .collect::<Vec<_>>());
    }
    let values: Array2D<i64> = Array2D::from_rows(&rows).unwrap();
    let columns = values.columns_iter();

    let actions = input[input.len()-1].split_ascii_whitespace()
        .map(|value| match value {
            "+" => '+',
            "*" => '*',
            _ => panic!(),
        })
        .collect::<Vec<_>>();

    for (i,column) in columns.enumerate() {
        let action = actions.get(i).unwrap();
        result.push((*action,column.map(|&value|value).collect::<Vec<_>>()));
    }

    result
}

fn parse_input_2(input: &Vec<String>) -> Vec<(char,Vec<i64>)> {
    let result = vec![];
    result
}

fn calculate_total(records: Vec<(char, Vec<i64>)>) -> i64 {
    let mut total = 0;

    let mut iter = records.into_iter();
    while let Some((action,values)) = iter.next() {
        total += match action {
            '+' => values.into_iter().sum::<i64>(),
            '*' => values.into_iter().product::<i64>(),
            _ => panic!()
        }
    }

    total
}

fn main() {
    let input = read_lines("./input.txt");
    let parsed_input = parse_input_1(&input);
    println!("solution 1: {}",calculate_total(parsed_input)); // 6172481852142
    let parsed_input = parse_input_2(&input);
    println!("solution 2: {}",calculate_total(parsed_input)); //
}
