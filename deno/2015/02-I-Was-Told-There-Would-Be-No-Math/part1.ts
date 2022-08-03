const input = Deno.readTextFileSync("input.txt");

const presents = input.split("\n");

let total = 0;

for (const present of presents) {
	const [l, w, h] = present.split("x").map(x => parseInt(x));
	const smallestSide = Math.min(l*w, w*h, h*l);
	total += 2*l*w + 2*w*h + 2*h*l + smallestSide;
}

console.log(total);
