use std::fs::File;
use std::io;
use std::io::BufRead;
use array2d::Array2D;

fn read_lines(filename: &str) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn combine_actions_and_values(actions: Vec<char>, values: Vec<Vec<i64>>) -> Vec<(char, Vec<i64>)> {
    let mut result = vec![];
    for (i,column) in values.into_iter().enumerate() {
        let action = actions.get(i).unwrap();
        result.push((*action,column));
    }
    result
}

fn get_actions(input: &Vec<String>) -> Vec<char> {
    input[input.len()-1].split_ascii_whitespace()
        .map(|value| match value {
            "+" => '+',
            "*" => '*',
            _ => panic!(),
        })
        .collect::<Vec<_>>()
}

fn get_values_1(input: &Vec<String>) -> Vec<Vec<i64>> {
    let mut lines = input[..input.len()-1].iter();
    let mut rows = vec![];

    // values are per row listed horizontally separated by a space
    while let Some(line) = lines.next() {
        rows.push(line.split_ascii_whitespace()
            .map(|value| value.parse::<i64>().unwrap())
            .collect::<Vec<_>>());
    }

    // values are collected in a Array2D where
    // each column in the array is input for a math
    // operation.
    let values: Array2D<i64> = Array2D::from_rows(&rows).unwrap();

    // iterate over the columns, to convert the
    // array into a Vec<Vec<i64>> where the inner
    // vec's each contain the values for a single problem
    values.columns_iter().map(|column|
        column.map(|&value|value).collect::<Vec<i64>>()
    ).collect::<Vec<_>>()
}

fn get_values_2(input: &Vec<String>) -> Vec<Vec<i64>> {
    let lines = input[..input.len()-1].iter();
    let rows = lines.map(|row| row.chars().collect::<Vec<_>>()).collect::<Vec<_>>();
    let chars = Array2D::from_rows(&rows).unwrap();

    // values are listed vertically, where values for
    // different problems are separated by a column
    // of spaces
    let mut result = vec![];
    let mut record = vec![];
    let mut columns = chars.columns_iter();
    while let Some(column) = columns.next() {

        // convert the vertical column to a string, and remove trailing whitespace
        // so we can attempt to parse it
        let column: String = column.collect::<Vec<_>>().iter().map(|&c|*c).collect();
        if let Some(s) = column.split_ascii_whitespace().next() {

            // the line should now hold a single integer value, so parse it
            if let Ok(value) = s.parse::<i64>() {
                record.push(value);
            } else { panic!(); }
        } else {

            // nothing remains after stripping whitespace, so we must
            // have hit an empty column, marking the end of a section
            // let's push the record to result and start a new record
            result.push(record);
            record = vec![];
        }
    }

    // no more columns to process, but we still need to
    // push the final record to result.
    result.push(record);

    result
}

fn calculate_total(records: Vec<(char, Vec<i64>)>) -> i64 {
    // iterate over all the records, execute their associated
    // action, and calculate the total of the results.
    records.iter().map(|(action,values)| {
        match action {
            '+' => values.into_iter().sum::<i64>(),
            '*' => values.into_iter().product::<i64>(),
            _ => panic!()
        }
    }).collect::<Vec<_>>().iter().sum()
}

fn main() {
    let input = read_lines("./input.txt");

    let records = combine_actions_and_values(get_actions(&input), get_values_1(&input));
    println!("solution 1: {}",calculate_total(records)); // 6172481852142

    let records = combine_actions_and_values(get_actions(&input), get_values_2(&input));
    println!("solution 2: {}",calculate_total(records)); // 10188206723429
}
