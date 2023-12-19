function isNumber(c: string) {
	if (c == "0") return true;
	if (c == "1") return true;
	if (c == "2") return true;
	if (c == "3") return true;
	if (c == "4") return true;
	if (c == "5") return true;
	if (c == "6") return true;
	if (c == "7") return true;
	if (c == "8") return true;
	if (c == "9") return true;
	return false;
}

const input = await Deno.readTextFile("./input.txt");
//console.log(input);

const lines = input.split("\n");
//console.log(lines);

let total = 0;
for (const line of lines) {
	let firstNumber = undefined;
	let lastNumber = undefined;
	
	//console.log(line);
	//console.log(line.length);
	//console.log(line[7]);
	for (let i = 0; i < line.length; i++) {
		//console.log(`${i}: ${line[i]}`);
		if (isNumber(line[i])) {
			//console.log("IT IS A NUMBER!!!! YAAAAY!!!");
			firstNumber = line[i];
			break;
		}
	}
	
	for (let i = line.length-1; i >= 0; i--) {
		//console.log(`${i}: ${line[i]}`);
		if (isNumber(line[i])) {
			//console.log("IT IS A NUMBER!!!! YAAAAY!!!");
			lastNumber = line[i];
			break;
		}
	}
	
	if (firstNumber === undefined || lastNumber === undefined) continue;
	const entireNumber = firstNumber + lastNumber;
	
	total = total + parseInt(entireNumber);
	
	//console.log(`firstNumber is ${firstNumber} and lastNumber is ${lastNumber} and entireNumber is ${entireNumber} and total is ${total}`);
	//console.log("\n\n");
}

console.log(`Total: ${total}`);
