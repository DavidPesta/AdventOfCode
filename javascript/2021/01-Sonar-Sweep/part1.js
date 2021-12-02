const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`\n`).map(n => parseInt(n));

let lastDepth = input[0];

let numIncreases = 0;
for (let i = 1; i < input.length; i++) {
	if (input[i] > lastDepth) {
		numIncreases++;
	}
	
	lastDepth = input[i];
}

console.log(numIncreases);