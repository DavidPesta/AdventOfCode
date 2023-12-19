const wordToNumber: Map<string, string> = new Map<string, string>([
	["zero", "0"],
	["one", "1"],
	["two", "2"],
	["three", "3"],
	["four", "4"],
	["five", "5"],
	["six", "6"],
	["seven", "7"],
	["eight", "8"],
	["nine", "9"]
]);

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

function findFirstWord(line: string) {
	const allWords = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
	let firstWord = "";
	let lowestIndex = line.length;
	
	for (const word of allWords) {
		const position = line.indexOf(word);
		if (position === -1) continue;
		if (position < lowestIndex) { // As it loops through every word, it selects for the word with the ever decreasing position to find the first word
			firstWord = word;
			lowestIndex = position;
		}
	}
	
	return firstWord;
}

function convertFirstWordToNumber(line: string): string {
	const firstWord = findFirstWord(line);
	return line.replace(firstWord, wordToNumber.get(firstWord)!);
}

function findLastWord(line: string) {
	const allWords = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
	let lastWord = "";
	let highestIndex = -1;
	
	for (const word of allWords) {
		const position = line.lastIndexOf(word);
		if (position === -1) continue;
		if (position > highestIndex) { // As it loops through every word, it selects for the word with the ever increasing position to find the last word
			lastWord = word;
			highestIndex = position;
		}
	}
	
	return lastWord;
}

function convertLastWordToNumber(line: string): string {
	//console.log(line);
	let lastWord = findLastWord(line);
	const originalLastWord = lastWord;
	//console.log(originalLastWord);
	lastWord = lastWord.split("").reverse().join("");
	//console.log(lastWord);
	line = line.split("").reverse().join("");
	//console.log(line);
	line = line.replace(lastWord, wordToNumber.get(originalLastWord)!);
	//console.log(line);
	
	//console.log("\n");
	return line.split("").reverse().join("");
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
	const line1 = convertFirstWordToNumber(line);
	//console.log(line1);
	//console.log(line1.length);
	for (let i = 0; i < line1.length; i++) {
		//console.log(`${i}: ${line1[i]}`);
		if (isNumber(line1[i])) {
			//console.log("IT IS A NUMBER!!!! YAAAAY!!!");
			firstNumber = line1[i];
			break;
		}
	}
	
	//console.log(line);
	const line2 = convertLastWordToNumber(line);
	//console.log(line2);
	for (let i = line2.length-1; i >= 0; i--) {
		//console.log(`${i}: ${line2[i]}`);
		if (isNumber(line2[i])) {
			//console.log("IT IS A NUMBER!!!! YAAAAY!!!");
			lastNumber = line2[i];
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
