use std::fs;

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    
    let mut sum = 0;
    for line in contents.lines() {
        let mut is_first_set = false;
        let mut first = 'a';
        let mut last = 'a';
        let characters = line.chars();
        
        // this is operating on chars so it won't compile.
        // is 10 regex calls per line really the best way?
        characters.map(|x| match x {
            'one' => '1',
            'two' => '2',
            'three' => '3',
            'four' => '4',
            'five' => '5',
            'six' => '6',
            'seven' => '7',
            'eight' => '8',
            'nine' => '9',
            _ => x
        });
        
        for character in characters {
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
}i
