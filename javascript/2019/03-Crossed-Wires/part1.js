const fs = require(`fs`);

const lineCommands = fs.readFileSync(`input.txt`, `utf8`).split(`\n`).map(x => x.split(`,`));

function paintCursorToLineMap(lineMap, lineNumber, cursor) {
	let location = `${cursor.x},${cursor.y}`;
	
	if (!(location in lineMap)) {
		lineMap[location] = new Set();
	}
	
	lineMap[location].add(lineNumber);
}

function addLineToMap(lineMap, lineNumber, fromCursor, command) {
	// Clone our fromCursor object to two new cursor objects.
	let cursor = Object.assign({}, fromCursor);
	let toCursor = Object.assign({}, fromCursor);
	
	const commandDirection = command[0];
	const commandDistance = parseInt(command.slice(1));
	
	// Use the command to adjust the toCursor.
	switch (commandDirection) {
		case `U`:
			toCursor.y += commandDistance;
			break;
		case `D`:
			toCursor.y -= commandDistance;
			break;
		case `L`:
			toCursor.x -= commandDistance;
			break;
		case `R`:
			toCursor.x += commandDistance;
			break;
		default:
			console.log(`Command direction not found!`);
	}
	
	// The final iteration in the while loop below will paint the final endpoint, but it will not paint the starting endpoint. So do that here.
	paintCursorToLineMap(lineMap, lineNumber, cursor);
	
	// Iteratively paint the lineMap with the lineNumber until the cursor reaches the toCursor.
	while (cursor.x != toCursor.x || cursor.y != toCursor.y) {
		cursor.x += (toCursor.x - cursor.x < 0) ? -1 : (toCursor.x - cursor.x > 0) ? 1 : 0;
		cursor.y += (toCursor.y - cursor.y < 0) ? -1 : (toCursor.y - cursor.y > 0) ? 1 : 0;
		paintCursorToLineMap(lineMap, lineNumber, cursor);
	}
	
	return cursor;
}

const startTime = Date.now();

const lineMap = {};

for (let lineNumber = 0; lineNumber < lineCommands.length; lineNumber++) {
	const commands = lineCommands[lineNumber];
	let cursor = {x: 0, y: 0};
	for (const command of commands) {
		cursor = addLineToMap(lineMap, lineNumber, cursor, command);
	}
}

let smallestDistance = null;
for (location in lineMap) {
	if (lineMap[location].size > 1) {
		let coordinates = location.split(`,`);
		let x = parseInt(coordinates[0]);
		let y = parseInt(coordinates[1]);
		let distance = Math.abs(x) + Math.abs(y);
		
		// Skip the origin where they always intersect. We're looking for the location closest to the origin, not the origin itself.
		if (distance == 0) continue;
		
		if (smallestDistance == null || smallestDistance > distance) {
			smallestDistance = distance;
		}
	}
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Closest Intersection to Origin: ${smallestDistance}`);