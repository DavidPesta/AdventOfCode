const input = Deno.readTextFileSync("input.txt");

const presents = input.split("\n");

let ribbon = 0;

for (const present of presents) {
	const dimensions = present.split("x").map(x => parseInt(x));
	const [l, w, h] = dimensions;
	const smallestDimensions = dimensions.sort((a, b) => a - b).slice(0, 2);
	ribbon += 2*smallestDimensions[0] + 2*smallestDimensions[1] + w*h*l;
}

console.log(ribbon);
