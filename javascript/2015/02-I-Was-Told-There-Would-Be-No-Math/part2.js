const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`);

const boxes = input.split("\n");

let totalRibbon = 0;

for (let i = 0; i < boxes.length; i++) {
	const box = boxes[i];
	
	const edges = box.split("x").map(n => parseInt(n));
	
	const l = edges[0];
	const w = edges[1];
	const h = edges[2];
	
	largestEdge = 0;
	if (l > largestEdge) {
		largestEdge = l;
	}
	if (w > largestEdge) {
		largestEdge = w;
	}
	if (h > largestEdge) {
		largestEdge = h;
	}
	
	const indexToGetRidOf = edges.indexOf(largestEdge);
	edges.splice(indexToGetRidOf, 1);
	
	let ribbon = 2 * edges[0] + 2 * edges[1];
	ribbon = ribbon + l * w * h;
	
	totalRibbon = totalRibbon + ribbon;
}

console.log(totalRibbon);