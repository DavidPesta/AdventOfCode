const input = "272091-815432";

const endpoints = input.split(`-`).map(x => parseInt(x));

function groupsOfSameAdjacentDigits(number) {
	const digits = number.toString();
	
	const groupsOfSameAdjacents = [];
	let sameAdjacentString = ``;
	let previousAdjacent = null;
	for (const digit of digits) {
		if (previousAdjacent == digit) {
			sameAdjacentString += digit;
		}
		else {
			if (sameAdjacentString.length >= 2) {
				groupsOfSameAdjacents.push(sameAdjacentString);
			}
			sameAdjacentString = digit;
		}
		
		previousAdjacent = digit;
	}
	
	if (sameAdjacentString.length >= 2) {
		groupsOfSameAdjacents.push(sameAdjacentString);
	}

	return groupsOfSameAdjacents;
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
	const groups = groupsOfSameAdjacentDigits(number);
	const groupCounts = groups.map(x => x.length);
	
	if (groupCounts.indexOf(2) != -1 && allDigitsAreEqualOrAscending(number)) {
		passwordCount++;
	}
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Number of Eligible Passwords: ${passwordCount}`);