use std::fs;

#[derive(Default)]
struct CubeSet {
    red: u8,
    green: u8,
    blue: u8
}

fn get_num_from_game_string(input: &str) -> u8 {
    let mut output = "".to_string();
    for c in input.chars() {
        if !c.is_digit(10) {
            continue;
        }
        output.push(c);
    }
    
    output.parse::<u8>().unwrap()
}

fn get_min_cube_set(game_string: &str) -> CubeSet {
    let mut game_string = game_string.split(';');
    while game_string.is_some() {

    let mut cube_min_set = CubeSet {..Default::default()};
        for cube_count_str in &cube_set {
        println!("{:?}", &cube_set);
        let cube_num = get_num_from_game_string(cube_count_str);
        if cube_count_str.contains("red") && cube_num > cube_min_set.red {
            cube_min_set.red = cube_num;
        }
        else if cube_count_str.contains("blue") && cube_num > cube_min_set.blue {
            cube_min_set.blue = cube_num
        }
        else if cube_count_str.contains("green") && cube_num > cube_min_set.green {
            cube_min_set.green = cube_num
        } 
        cube_sets.next()
    }
    }
    return cube_min_set;
}

fn get_set_power(cube_set: &CubeSet) -> u32 {
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
        let game_data = line.split(':').expect("DEBUG")[1];

        // iterate through each game, storing largest cube counts
        let min_cube_set = get_min_cube_set(game_data);
        sum_of_powers += get_set_power(cube_min_set);
        println!("Running total: {sum_of_powers}\n");
        index_num += 1;
    }
    println!("{sum_of_powers}");
}
