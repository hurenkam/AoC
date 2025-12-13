use std::cmp::{max, min, Ordering};
use std::fs::File;
use std::io;
use std::io::BufRead;
use std::ops::Range;
use std::path::Path;
use std::vec::IntoIter;

fn read_lines(filename: &Path) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

fn read_input(filename: &str) -> Vec<String> {
    let path = Path::new(filename);
    read_lines(path)
}

fn parse_ranges(iter: &mut IntoIter<String>) -> Vec<Range<u64>> {
    let mut ranges: Vec<Range<u64>> = Vec::new();
    while let Some(line) = iter.next() {
        if line.is_empty() { break; }
        let mut parts = line.split('-');
        let start = parts.next().unwrap().parse::<u64>().unwrap();
        let end = parts.next().unwrap().parse::<u64>().unwrap()+1;
        ranges.push(start..end);
    }
    ranges
}

fn parse_ids(iter: &mut IntoIter<String>) -> Vec<u64> {
    let mut ids: Vec<u64> = Vec::new();
    while let Some(line) = iter.next() {
        if line.is_empty() { continue; }
        ids.push(line.parse::<u64>().unwrap());
    }
    ids
}

fn is_id_fresh(id: u64, ranges: &Vec<Range<u64>>) -> bool {
    let mut range_iter = ranges.into_iter();
    while let Some(range) = range_iter.next() {
        if range.contains(&id) { return true; }
    }

    false
}

fn merge_ranges(range1: &Range<u64>, range2: &Range<u64>) -> Option<Range<u64>> {
    if range1.start <= range2.start {
        if range1.end >= range2.start {
            let range = min(range1.start, range2.start)..max(range1.end, range2.end);
            Some(range)
        } else {
            None
        }
    } else {
        if range2.end >= range1.start {
            let range = min(range1.start, range2.start)..max(range1.end, range2.end);
            Some(range)
        } else {
            None
        }
    }
}

fn sort_ranges(ranges: &mut Vec<Range<u64>>) {
    ranges.sort_by(|a, b| {
        match a.start.cmp(&b.start) {
            Ordering::Less => Ordering::Less,
            Ordering::Greater => Ordering::Greater,
            Ordering::Equal => {
                a.end.cmp(&b.end)
            }
        }
    });
}

fn optimize_ranges(ranges: &mut Vec<Range<u64>>) {
    sort_ranges(ranges);
    let mut unmerged: Vec<Range<u64>> = vec![];
    loop {
        let last = match ranges.pop() {
            None => { break; }
            Some(range) => range
        };

        let first = match ranges.pop() {
            None => {
                unmerged.push(last);
                break;
            }
            Some(range) => range
        };

        if let Some(range) = merge_ranges(&first, &last) {
            ranges.push(range);
        } else {
            unmerged.push(last);
            ranges.push(first);
        }
    }

    sort_ranges(&mut unmerged);
    *ranges = unmerged;
}

fn solution_1(ranges: &Vec<Range<u64>>, ids: &Vec<u64>) -> u64 {
    let mut id_iter = ids.into_iter();
    let mut count = 0;
    while let Some(id) = id_iter.next() {
        if is_id_fresh(*id, ranges) { count += 1; }
    }

    count
}

fn solution_2(ranges: &mut Vec<Range<u64>>) -> u64 {
    optimize_ranges(ranges);
    let mut result = 0;
    for range in ranges {
        let count = range.end - range.start;
        result += count;
    }

    result
}

fn main() {
    let input = read_input("./input.txt");
    let mut iter = input.into_iter();
    let mut ranges = parse_ranges(&mut iter);
    let ids = parse_ids(&mut iter);

    println!("solution 1: {}", solution_1(&ranges,&ids));  // 737
    println!("solution 2: {}", solution_2(&mut ranges));   // 357485433193284
}
