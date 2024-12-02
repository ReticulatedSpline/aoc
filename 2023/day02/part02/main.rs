use std::fs;

#[derive(Default)]
#[derive(Debug)]
struct CubeSet {
    red: u32,
    green: u32,
    blue: u32
}

fn get_num_from_game_string(input: &str) -> u32 {
    let mut output = "".to_string();
    for c in input.chars() {
        if !c.is_digit(10) {
            continue;
        }
        output.push(c);
    }
    
    output.parse::<u32>().unwrap()
}

fn get_min_cube_set(round_strings: &str) -> CubeSet {
    let games = round_strings.split(';');
    let mut cube_min_set = CubeSet {..Default::default()};
    for game in games {
        println!("{:?}", &game);
        let cubes = game.split(',');
        for cube in cubes {
            let cube_num = get_num_from_game_string(cube);
            if cube.contains("red") && cube_num > cube_min_set.red {
                cube_min_set.red = cube_num;
            }
            else if cube.contains("blue") && cube_num > cube_min_set.blue {
                cube_min_set.blue = cube_num
            }
            else if cube.contains("green") && cube_num > cube_min_set.green {
                cube_min_set.green = cube_num
            } 
        }
    }
    return cube_min_set;
}

fn get_set_power(set: &CubeSet) -> u32 {
    set.red * set.blue * set.green
}

fn main() {
    let contents = fs::read_to_string("../input.txt").unwrap();
    let mut sum_of_powers: u32 = 0;
    let mut index_num = 1;

    // split input into lines
    for line in contents.lines() {
        println!("Game {index_num}:");
        
        // discard header data on each line
        let game_data = line.split(':').collect::<Vec<&str>>()[1];

        // iterate through each game, storing largest cube counts
        let min_cube_set = get_min_cube_set(game_data);
        sum_of_powers += get_set_power(&min_cube_set);
        println!("{:?}", min_cube_set);
        println!("Running total: {sum_of_powers}\n");
        index_num += 1;
    }
}
