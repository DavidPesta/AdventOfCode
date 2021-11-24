const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`\n`);

const startTime = Date.now();

let totalFuel = 0;
for (const moduleMass of input) {
	let moduleFuel = Math.floor(moduleMass / 3) - 2;
	let moduleFuelMass = moduleFuel;
	do {
		moduleFuelMass = Math.floor(moduleFuelMass / 3) - 2;
		if (moduleFuelMass > 0) {
			moduleFuel += moduleFuelMass;
		}
	}
	while (moduleFuelMass > 0);
	totalFuel += moduleFuel;
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Total Fuel: ${totalFuel}`);