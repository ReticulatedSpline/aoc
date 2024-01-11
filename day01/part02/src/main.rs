use std::fs;
use regex::Regex;

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    
    let mut sum = 0;
    
    let regex_data =
        [(r"zero", "0"),
         (r"one", "1"),
         (r"two", "2"),
         (r"three", "3"),
         (r"four", "4"),
         (r"five", "5"),
         (r"six", "6"),
         (r"seven", "7"),
         (r"eight", "8"),
         (r"nine", "9")];

    // compile regexes outside of hot loop
    let mut regexes = vec![];
    for data in regex_data {
        let (pattern, replacement) = data;
        // dubious repacking of tuple...
        regexes.push((Regex::new(pattern).unwrap(), replacement));
    }

    for line in contents.lines() {
        for regex in regexes {
            let (re, replacement) = regex;
            re.replace_all(&line, replacement);
        }
        println!("After  re: {}", line);

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
