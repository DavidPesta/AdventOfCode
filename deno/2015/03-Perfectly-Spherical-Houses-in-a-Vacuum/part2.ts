const input = Deno.readTextFileSync("input.txt");

const directions = input.split("");

const characterDirections: Record<number, Array<string>> = {};

characterDirections[0] = directions.filter((_v, i) => i % 2 == 0); // These are the directions that Santa gets
characterDirections[1] = directions.filter((_v, i) => i % 2 == 1); // These are the directions that Robo-Santa gets

const grid: Record<string, number> = {};

// Iterate over the directions for Santa (i == 0), then iterate over the directions for Robo-Santa (i == 1)
for (let i = 0; i <= 1; i++) {
	let x = 0;
	let y = 0;
	if (grid[`${x},${y}`] == undefined) grid[`${x},${y}`] = 0;
	grid[`${x},${y}`]++;
	
	for (const direction of characterDirections[i]) {
		if (direction == "<") x -= 1;
		if (direction == ">") x += 1;
		if (direction == "v") y -= 1;
		if (direction == "^") y += 1;
		if (grid[`${x},${y}`] == undefined) grid[`${x},${y}`] = 0;
		grid[`${x},${y}`]++;
	}
}

console.log(Object.keys(grid).length);
