use std::fs;

struct Game {
    red: u8,
    blue: u8,
    green: u8
}

const MAX_CUBE_GAME: Game = Game {
    red: 12,
    blue: 13,
    green: 14
};

fn get_num(input: &str) -> u8 {
    // fetch ascii number from string
    let mut output = "".to_string();
    for c in input.chars() {
        if !c.is_digit(10) {
            continue;
        }
        output.push(c)
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
        while game.is_some() {
            let cube_sets: Vec<&str> = game.expect("DEBUG").split(',').collect();
            let mut game_valid = true;
            for cube_count_str in &cube_sets {
                let cube_num = get_num(cube_count_str);
                if !game_valid {
                    println!("Invalid match detected in {:?}", &cube_count_str);
                    break;
                }
                if cube_count_str.contains("red") && cube_num > MAX_CUBE_GAME.red {
                    game_valid = false;
                }
                else if cube_count_str.contains("blue") && cube_num > MAX_CUBE_GAME.blue {
                    game_valid = false;
                }
                else if cube_count_str.contains("green") && cube_num > MAX_CUBE_GAME.green {
                    game_valid = false;
                }
                game_id_sum += u32::from(cube_num);
            }
            if !game_valid {
                break;
            }
            println!("{:?}", &cube_sets);
            game = parts.next()
        }
        println!("\n");
        index_num += 1;
    }
    println!("{game_id_sum}");
}
