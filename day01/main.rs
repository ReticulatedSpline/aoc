use std::fs;

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    let mut sum = 0;
    for line in contents.lines() {
        let mut first: Option<u32> = None;
        let mut last: Option<u32> = None;
        for character in line.chars() {
            if character.is_digit(10) {
                first = character.to_digit(10);
                break;
            }
        }
        for character in line.chars().rev() {
            if character.is_digit(10) {
                last = character.to_digit(10);
                break;
            }
        }
        sum += first.unwrap() + last.unwrap();
    }
    println!("{sum}");
}
