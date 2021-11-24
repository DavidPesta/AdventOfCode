const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`,`).map(x => parseInt(x));

input[1] = 12;
input[2] = 2;

//console.log(JSON.stringify(input));

const startTime = Date.now();

// ip = instruction pointer
for (ip = 0; ip < input.length; ip += 4) {
	if (input[ip] == 1) {
		input[input[ip+3]] = input[input[ip+1]] + input[input[ip+2]];
	}
	
	if (input[ip] == 2) {
		input[input[ip+3]] = input[input[ip+1]] * input[input[ip+2]];
	}
	
	if (input[ip] == 99) {
		break;
	}
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Value at index position 0 is: ${input[0]}`);