const input = Deno.readTextFileSync("input.txt");

const directions = input.split("");

let x = 0;
let y = 0;
const grid: Record<string, number> = {};
grid[`${x},${y}`] = 1;

for (const direction of directions) {
	if (direction == "<") x -= 1;
	if (direction == ">") x += 1;
	if (direction == "v") y -= 1;
	if (direction == "^") y += 1;
	if (grid[`${x},${y}`] == undefined) grid[`${x},${y}`] = 0;
	grid[`${x},${y}`]++;
}

console.log(Object.keys(grid).length);
