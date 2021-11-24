const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`\n`);

const startTime = Date.now();

let totalFuel = 0;
for (const moduleMass of input) {
	totalFuel += Math.floor(moduleMass / 3) - 2;
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Total Fuel: ${totalFuel}`);