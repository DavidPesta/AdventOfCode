const input = Deno.readTextFileSync("input.txt");

const instructions = input.split("");

let floor = 0;
let position = 0;

for (const instruction of instructions) {
	position++;
	if (instruction == "(") floor++;
	if (instruction == ")") floor--;
	if (floor == -1) break;
}

console.log(position);
