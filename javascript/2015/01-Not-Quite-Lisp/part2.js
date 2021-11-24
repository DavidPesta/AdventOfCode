const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`);

const length = input.length;

let floor = 0;

let position = 1;
for (; position <= length; position++) {
	if (input[position-1] == "(") {
		floor = floor + 1;
	}
	
	if (input[position-1] == ")") {
		floor = floor - 1;
	}
	
	if (floor == -1) {
		break;
	}
}

console.log(position);