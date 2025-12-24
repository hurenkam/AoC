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

fn calculate_part1(surfaces: &HashMap::<(Point,Point),usize>, keys: &Vec<(Point,Point)>) -> usize {
    surfaces.get(keys.last().unwrap()).unwrap().clone()
}

fn find_rows_and_columns(points: &Vec<Point>) -> (Vec<i64>,Vec<i64>) {
    let mut rows: Vec<i64> = vec![];
    let mut columns: Vec<i64> = vec![];

    for point in points {
        rows.push(point.0);
        columns.push(point.1);
    }
    rows = rows.into_iter().unique().sorted().collect_vec();
    columns = columns.into_iter().unique().sorted().collect_vec();
    //println!("{}, {:?}", rows.len(), rows);
    //println!("{}, {:?}", columns.len(), columns);

    (rows,columns)
}

fn find_neighbors(point: &Point, points: &Vec<Point>) -> Vec<Point> {
    let mut result: Vec<Point> = vec![];

    result
}

fn calculate_polygon(points: &Vec<Point>) -> Vec<Point> {
    let (rows, columns) = find_rows_and_columns(points);
    let mut columns_by_row : HashMap<i64,Vec<i64>> = HashMap::new();
    for row in rows {
        let mut columns = vec![];
        for point in points {
            if (point.0 == row) { columns.push(point.1); }
        }
        println!("row: {}, columns: {:?}", row, columns);
        columns_by_row.insert(row, columns);
    }

    let mut rows_by_column : HashMap<i64,Vec<i64>> = HashMap::new();
    for column in columns {
        let mut rows = vec![];
        for point in points {
            if (point.1 == column) { rows.push(point.0); }
        }
        println!("column: {}, rows: {:?}", column, rows);
        rows_by_column.insert(column, rows);
    }

    let mut polygon: Vec<Point> = vec![];

    /*
    let mut points = points.clone();
    for point in &points {
        println!("point: {:?}, neighbors: {:?}",*point, find_neighbors(*point, &points));
    }
    */

    polygon
}

fn find_horizontal_and_vertical_lines(points: &Vec<Point>) -> (Vec<(Point, Point)>, Vec<(Point, Point)>) {
    let mut horizontal: Vec<(Point,Point)> = vec![];
    let mut vertical: Vec<(Point,Point)> = vec![];

    todo!();

    (horizontal, vertical)
}

fn do_lines_intersect(horizontal: &(Point,Point), vertical: &(Point,Point)) -> bool {
    let ((hl,hy),(hr,_)) = horizontal;
    let ((vx,ty),(_,by)) = vertical;

    // is vertical line left or right of horizontal line?
    if vx <= hl || vx >= hr { return false }

    // is horizontal line above or below vertical line?
    if hy <= ty || hy >= by { return false }

    // if not, then they must intersect
    true
}

fn does_rectangle_intersect(key: &(Point,Point), horizontal: &Vec<(Point, Point)>, vertical: &Vec<(Point, Point)>) -> bool {
    let (from,to) = key;
    let lx = min(from.0,to.0);
    let rx = max (from.0,to.0);
    let ty = min(from.1,to.1);
    let by = max(from.1,to.1);
    let top = ((lx,ty),(rx,ty));
    let bottom = ((lx,by),(rx,by));
    let left = ((lx,ty),(lx,by));
    let right = ((rx,ty),(rx,by));

    // - check if the rectangles vertical lines cross any of the polygon horizontal lines
    for h in horizontal {
        if do_lines_intersect(h,&left) { return true }
        if do_lines_intersect(h,&right) { return true }
    }

    // - check if the rectangles horizontal lines cross any of the polygon vertical lines
    for v in vertical {
        if do_lines_intersect(&top,v) { return true }
        if do_lines_intersect(&bottom,v) { return true }
    }

    false
}

fn calculate_part2(points: &Vec<Point>, surfaces: &HashMap::<(Point,Point),usize>, keys: &Vec<(Point,Point)>) -> usize {

    // algorithm:
    // ----------
    // determine horizontal and vertical lines of the polygon
    // iterate over the rectangles from part1, starting from the bottom of the list:
    // - check if the horizontal lines cross any of the polygon vertical lines
    // - check if the vertical lines cross any of the polygon horizontal lines
    // repeat until a candidate is found that does not intersect,
    // since the keys are sorted, this must be the largest possible rectangle.

    let (horizontal,vertical) = find_horizontal_and_vertical_lines(&points);
    let mut index = keys.len()-1;
    while does_rectangle_intersect(keys.get(index).unwrap(),&horizontal,&vertical) {
        index -= 1;
    }

    surfaces.get(keys.get(index).unwrap()).unwrap().clone()
}

fn main() {
    let input = read_lines("./input.txt");
    let mut points = parse_input(input);
    sort_points(&mut points);
    //println!("{:?}",points);

    let surfaces = calculate_surfaces(&points);
    let keys = get_keys_sorted_by_surface(&surfaces);
    println!("solution 1: {}",calculate_part1(&surfaces,&keys)); // 4758598740
    println!("solution 2: {}",calculate_part2(&points,&surfaces,&keys)); //
}
