const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`);

const boxes = input.split("\n");

let totalPaper = 0;

for (let i = 0; i < boxes.length; i++) {
	const box = boxes[i];
	
	const edges = box.split("x").map(n => parseInt(n));
	
	const l = edges[0];
	const w = edges[1];
	const h = edges[2];
	
	let paper = 2*l*w + 2*w*h + 2*h*l;
	
	const side1 = l*w;
	const side2 = w*h;
	const side3 = h*l;
	
	let smallestSide = Infinity;
	if (side1 < smallestSide) {
		smallestSide = side1;
	}
	if (side2 < smallestSide) {
		smallestSide = side2;
	}
	if (side3 < smallestSide) {
		smallestSide = side3;
	}
	
	paper = paper + smallestSide;
	
	totalPaper = totalPaper + paper;
}

console.log(totalPaper);