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

fn get_num(&str) -> u8 {
    // fetch ascii number from string
}

fn main() {
    let contents = fs::read_to_string("./input.txt")
        .expect("Unable to read file");
    let mut game_id_sum = 0;
    let mut index_num = 1;
    for line in contents.lines() {
        let mut parts = line.split([':', ';']);
        // skips unneeded 'Game: [0-9]+' substring
        parts.next();
        println!("Game {index_num}");
        let mut game = parts.next();
        while game.is_some() {
            let cube_sets: Vec<&str> = game.expect("DEBUG").split(',').collect();
            for cube_count_str in cube_sets {
                // functionalize this maybe
                if cube_count_str.contains("red") && 
                   get_num(cube_count_str) > MAX_CUBE_GAME.red {
                    // skip to next game
                }
                else if cube_count_str.contains("blue") && 
                        get_num(cube_count_str) > MAX_CUBE_GAME.blue {
                    // skip to next game
                }
                else if cube_count_str.contains("green") && 
                        get_num(cube_count_str) > MAX_CUBE_GAME.green {
                    // skip to next game
                }
            }
            println!("{:?}", cubes);
            game = parts.next()
        }
        println!("\n");
        index_num += 1;
    }
    println!("{game_id_sum}");
}
