const input = Deno.readTextFileSync("input.txt");

const instructions = input.split("");

let floor = 0;

for (const instruction of instructions) {
	if (instruction == "(") floor++;
	if (instruction == ")") floor--;
}

console.log(floor);
