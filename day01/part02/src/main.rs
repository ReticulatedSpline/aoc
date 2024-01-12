use std::fs;

fn main() {
    let mut contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    
    let mut sum = 0;
    
    let digit_strs =
        [("zero", "z0o"),
         ("one", "o1e"),
         ("two", "t2o"),
         ("three", "t3e"),
         ("four", "f4r"),
         ("five", "f5e"),
         ("six", "s6x"),
         ("seven", "s7n"),
         ("eight", "e8t"),
         ("nine", "n9e")];

    println!("Before re: {}", contents);
    for digit in digit_strs {
        contents = contents.replace(digit.0, digit.1);
    }
    println!("After  re: {}", contents);

    for line in contents.lines() {
        let mut is_first_set = false;
        let mut first = 'a';
        let mut last = 'a';
               
        for character in line.chars() {
            if !character.is_digit(10) {
                continue;
            }
            
            if !is_first_set {
                first = character;
                is_first_set = true;
            }

            last = character;
        }
        let mut combined_chars = first.to_string();
        combined_chars.push_str(&last.to_string());
        sum += combined_chars.parse::<i32>().unwrap();
    }
    println!("{sum}");
}