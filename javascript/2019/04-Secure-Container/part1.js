const input = "272091-815432";

const endpoints = input.split(`-`).map(x => parseInt(x));

function numberOfSameAdjacentDigits(number) {
	const digits = number.toString();
	
	let highestNumberOfSameAdjacentDigits = 0;
	let previousAdjacent = null;
	let numberOfSameAdjacents = 0;
	for (const digit of digits) {
		if (previousAdjacent == digit) {
			numberOfSameAdjacents++;
		}
		else {
			numberOfSameAdjacents = 1;
		}
		
		if (numberOfSameAdjacents > highestNumberOfSameAdjacentDigits) {
			highestNumberOfSameAdjacentDigits = numberOfSameAdjacents;
		}
		
		previousAdjacent = digit;
	}
	
	return highestNumberOfSameAdjacentDigits;
}

function allDigitsAreEqualOrAscending(number) {
	const digits = number.toString();
	const length = digits.length;
	for (i = 1; i < length; i++) {
		if (digits[i] < digits[i-1]) {
			return false;
		}
	}
	return true;
}

const startTime = Date.now();

let passwordCount = 0;
for (let number = endpoints[0]; number <= endpoints[1]; number++) {
	if (numberOfSameAdjacentDigits(number) >= 2 && allDigitsAreEqualOrAscending(number)) {
		passwordCount++;
	}
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Number of Eligible Passwords: ${passwordCount}`);