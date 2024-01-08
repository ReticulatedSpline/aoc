use std::fs;

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    let mut sum = 0;
    for line in contents.lines() {
        let mut first = 'a';
        let mut last = 'a';
        for character in line.chars() {
            if character.is_digit(10) {
                first = character;
                break;
            }
        }
        for character in line.chars().rev() {
            if character.is_digit(10) {
                last = character;
                break;
            }
        }
        let mut combined_chars = first.to_string();
        combined_chars.push_str(&last.to_string());
        sum += combined_chars.parse::<i32>().unwrap();
    }
    println!("{sum}");
}
