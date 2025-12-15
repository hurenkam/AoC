use std::cmp::Ordering;
use std::collections::HashMap;
use std::fs::File;
use std::io;
use std::io::BufRead;
use itertools::Itertools;

fn read_lines(filename: &str) -> Vec<String> {
    let file = File::open(filename).expect(format!("file {:?} should be readable", filename).as_str());
    let content: Vec<String> = io::BufReader::new(file).lines().map(|line|line.unwrap()).collect();
    content
}

type Point = (i64,i64,i64);

fn parse_input(input: Vec<String>) -> Vec<Point> {
    input.iter().map(|line|{
        line.split(',').map(|s|{
            s.parse::<i64>().unwrap()
        }).collect::<Vec<i64>>().into_iter().collect_tuple().unwrap()
    }).collect()
}

fn sort_points(points: &mut Vec<Point>) {
    points.sort_by(|a, b| {
        match a.0.cmp(&b.0) {
            Ordering::Less => Ordering::Less,
            Ordering::Greater => Ordering::Greater,
            Ordering::Equal => {
                match a.1.cmp(&b.1) {
                    Ordering::Less => Ordering::Less,
                    Ordering::Greater => Ordering::Greater,
                    Ordering::Equal => a.2.cmp(&b.2)
                }
            }
        }
    });
}

fn square(i: i64) -> i64 {
    i*i
}

fn calculate_squared_distance(from: Point,to: Point) -> i64 {
    square(from.0 - to.0) + square(from.1 - to.1) + square(from.2 - to.2)
}

fn calculate_distances(points: &Vec<Point>) -> HashMap<(Point,Point),i64> {
    let mut distances: HashMap<(Point,Point),i64> = HashMap::new();
    for i in 0..points.len() {
        for j in i+1..points.len() {
            let from = points[i];
            let to = points[j];
            distances.insert((from,to),calculate_squared_distance(from,to));
        }
    }
    distances
}
fn get_keys_sorted_by_distance(distances: &HashMap<(Point, Point),i64>) -> Vec<(Point, Point)> {
    let mut keys: Vec<(Point,Point)> = distances.keys().cloned().collect_vec();
    keys.sort_by(|a, b| {
        let da = distances.get(a).unwrap();
        let db = distances.get(b).unwrap();
        da.cmp(&db)
    });
    keys
}

fn get_circuit_index_for_point(circuits: &Vec<Vec<Point>>, point: Point) -> Option<usize> {
    for (i,circuit) in circuits.iter().enumerate() {
        if circuit.contains(&point) {
            return Some(i);
        }
    }

    None
}

fn add_pair_to_circuits(circuits: &mut Vec<Vec<Point>>, pair: (Point,Point)) {
    let from = get_circuit_index_for_point(&circuits, pair.0);
    let to = get_circuit_index_for_point(&circuits, pair.1);
    if from == None && to == None {
        circuits.push(vec![pair.0,pair.1]);
        return;
    }

    if from != None && to != None {
        if from == to {
            // nothing to do
            return;
        } else {
            // need to merge two segments
            let from = from.unwrap();
            let to = to.unwrap();
            let mut combined: Vec<Point> = circuits.get(from).unwrap().clone();
            combined.extend(circuits.get(to).unwrap().clone());
            *circuits.get_mut(from).unwrap() = combined.iter().unique().cloned().collect_vec();
            circuits.remove(to);
            return
        }
    }

    if let Some(point) = from {
        circuits.get_mut(point).unwrap().push(pair.1);
    }

    if let Some(point) = to {
        circuits.get_mut(point).unwrap().push(pair.0);
    }
}

fn sort_circuits(circuits: &mut Vec<Vec<Point>>) {
    circuits.sort_by(|a, b| {
        let la = a.len();
        let lb = b.len();
        lb.cmp(&la)
    });
}

fn connect_pairs(count: usize, distances: &HashMap<(Point,Point),i64>) -> Vec<Vec<Point>> {
    let sorted_keys = get_keys_sorted_by_distance(distances);
    let mut circuits : Vec<Vec<Point>> = Vec::new();
    for i in 0..count {
        add_pair_to_circuits(&mut circuits, sorted_keys[i]);
    }
    sort_circuits(&mut circuits);
    circuits
}

fn connect_all_pairs(points: &mut Vec<Point>, distances: &HashMap<(Point,Point),i64>) -> (Point, Point) {
    let sorted_keys = get_keys_sorted_by_distance(distances);
    let mut i = 0;
    let mut key = sorted_keys[0];
    while points.len() > 0 {
        key = sorted_keys[i];
        points.retain(|x| *x != key.0 && *x != key.1);
        i+=1;
    }
    key
}

fn calculate_part1(distances: &HashMap<(Point,Point),i64>) -> usize {
    let circuits = connect_pairs(1000, &distances);
    circuits[0..3].iter().map(|i|i.len()).collect_vec().iter().product()
}

fn calculate_part2(points: &mut Vec<Point>, distances: &HashMap<(Point,Point),i64>) -> i64 {
    let pair = connect_all_pairs(points,distances);
    pair.0.0 * pair.1.0
}

fn main() {
    let input = read_lines("./input.txt");
    let mut points = parse_input(input);
    sort_points(&mut points);
    let distances = calculate_distances(&points);

    println!("solution 1: {}",calculate_part1(&distances)); // 68112
    println!("solution 2: {}",calculate_part2(&mut points,&distances)); // 44543856
}
