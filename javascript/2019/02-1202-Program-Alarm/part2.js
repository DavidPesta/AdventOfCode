const fs = require(`fs`);

const input = fs.readFileSync(`input.txt`, `utf8`).split(`,`).map(x => parseInt(x));

const startTime = Date.now();

let answer = null;

for (let noun = 0; noun <= 99; noun++) {
	for(let verb = 0; verb <= 99; verb++) {
		let memory = input.slice();
		
		memory[1] = noun;
		memory[2] = verb;
		
		// ip = instruction pointer
		for (ip = 0; ip < memory.length; ip += 4) {
			if (memory[ip] == 1) {
				memory[memory[ip+3]] = memory[memory[ip+1]] + memory[memory[ip+2]];
			}
			
			if (memory[ip] == 2) {
				memory[memory[ip+3]] = memory[memory[ip+1]] * memory[memory[ip+2]];
			}
			
			if (memory[ip] == 99) {
				break;
			}
		}
		
		if (memory[0] == 19690720) {
			answer = (100 * noun) + verb;
			break;
		}
	}
	if (answer != null) break;
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Answer is: ${answer}`);