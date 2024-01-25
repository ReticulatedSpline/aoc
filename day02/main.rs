use std::fs;

struct Game {
    red: u8,
    green: u8,
    blue: u8
}

const MAX_CUBE_GAME: Game = Game {
    red: 12,
    green: 13,
    blue: 14
};

fn get_num(input: &str) -> u8 {
    // fetch ascii number from string
    let mut output = "".to_string();
    for c in input.chars() {
        if !c.is_digit(10) {
            continue;
        }
        output.push(c);
    }
    
    output.parse::<u8>().unwrap()
}

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    let mut game_id_sum: u32 = 0;
    let mut index_num = 1;
    for line in contents.lines() {
        let mut parts = line.split([':', ';']);
        parts.next(); // skip unneeded 'Game: [0-9]+' substring
        println!("Game {index_num}:");
        let mut game = parts.next();
        let mut game_valid = true;
        while game.is_some() {
            let cube_sets: Vec<&str> = game.expect("DEBUG").split(',').collect();
            game_valid = true;
            println!("{:?}", &cube_sets);
            for cube_count_str in &cube_sets {
                let cube_num = get_num(cube_count_str);
                if cube_count_str.contains("red") && cube_num > MAX_CUBE_GAME.red {
                    game_valid = false;
                }
                else if cube_count_str.contains("blue") && cube_num > MAX_CUBE_GAME.blue {
                    game_valid = false;
                }
                else if cube_count_str.contains("green") && cube_num > MAX_CUBE_GAME.green {
                    game_valid = false;
                }
                
                if !game_valid {
                    println!("Invalid combination: {:?}", &cube_count_str[1..]);
                    break;
                }
            }
            
            if !game_valid {
                println!("Skipping game...");
                break;
            }
            
            game = parts.next()
        }
        if game_valid {
            game_id_sum += index_num;
            println!("Sum: {game_id_sum}");
        }
        println!("\n");
        index_num += 1;
    }
    println!("{game_id_sum}");
}