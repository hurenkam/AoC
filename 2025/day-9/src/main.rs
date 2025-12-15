use std::cmp::{max, min, Ordering};
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

type Point = (i64,i64);

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
            Ordering::Equal => a.1.cmp(&b.1)
        }
    });
}

fn calculate_surface(from: Point,to: Point) -> usize {
    ((max(from.0,to.0) - min(from.0,to.0)+1) * (max(from.1,to.1) - min(from.1,to.1)+1)) as usize
}

fn calculate_surfaces(points: &Vec<Point>) -> HashMap<(Point,Point),usize> {
    let mut surfaces: HashMap<(Point, Point),usize> = HashMap::new();
    for i in 0..points.len() {
        for j in i+1..points.len() {
            let from = points[i];
            let to = points[j];
            surfaces.insert((from, to), calculate_surface(from, to));
        }
    }
    surfaces
}
fn get_keys_sorted_by_surface(surface: &HashMap<(Point, Point),usize>) -> Vec<(Point, Point)> {
    let mut keys: Vec<(Point,Point)> = surface.keys().cloned().collect_vec();
    keys.sort_by(|a, b| {
        let da = surface.get(a).unwrap();
        let db = surface.get(b).unwrap();
        da.cmp(&db)
    });
    keys
}

fn calculate_part1(points: &Vec<Point>) -> usize {
    let surfaces = calculate_surfaces(&points);
    let keys = get_keys_sorted_by_surface(&surfaces);
    surfaces.get(keys.last().unwrap()).unwrap().clone()
}

fn calculate_part2(points: &Vec<Point>) -> usize {
    let polygon = calculate_polygon(&points);

    0
}

fn main() {
    let input = read_lines("./input.txt");
    let mut points = parse_input(input);
    sort_points(&mut points);
    //println!("{:?}",points);

    println!("solution 1: {}",calculate_part1(&points)); // 4758598740
    println!("solution 2: {}",calculate_part2(&points)); //
}
