const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`\n`).map(n => parseInt(n));

let lastDepth = input[0] + input[1] + input[2];

let numIncreases = 0;
for (let i = 1; i < input.length-2; i++) {
	let thisDepth = input[i] + input[i+1] + input[i+2];
	if (thisDepth > lastDepth) {
		numIncreases++;
	}
	
	lastDepth = thisDepth;
}

console.log(numIncreases);