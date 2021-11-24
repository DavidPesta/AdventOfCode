const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`);

const length = input.length;

let floor = 0;

for (let i = 0; i < length; i++) {
	if (input[i] == "(") {
		floor = floor + 1;
	}
	
	if (input[i] == ")") {
		floor = floor - 1;
	}
}

console.log(floor);