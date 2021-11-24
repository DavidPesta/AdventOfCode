const fs = require(`fs`);

const lineCommands = fs.readFileSync(`input.txt`, `utf8`).split(`\n`).map(x => x.split(`,`));

function paintCursorToLineMap(lineMap, lineNumber, cursor) {
	let location = `${cursor.x},${cursor.y}`;
	
	if (!(location in lineMap)) {
		lineMap[location] = {};
	}
	
	// Only the step count for the first time that the line enters needs to be remembered.
	if (!(lineNumber in lineMap[location])) {
		lineMap[location][lineNumber] = cursor.stepCount;
	}
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
			toCursor.stepCount += commandDistance;
			break;
		case `D`:
			toCursor.y -= commandDistance;
			toCursor.stepCount += commandDistance;
			break;
		case `L`:
			toCursor.x -= commandDistance;
			toCursor.stepCount += commandDistance;
			break;
		case `R`:
			toCursor.x += commandDistance;
			toCursor.stepCount += commandDistance;
			break;
		default:
			console.log(`Command direction not found!`);
	}
	
	// The final iteration in the while loop below will paint the final endpoint, but it will not paint the starting endpoint. So do that here.
	paintCursorToLineMap(lineMap, lineNumber, cursor);
	
	// Iteratively paint the lineMap with the lineNumber until the cursor reaches the toCursor.
	while (cursor.x != toCursor.x || cursor.y != toCursor.y) {
		if (cursor.x != toCursor.x) {
			cursor.x += (toCursor.x - cursor.x < 0) ? -1 : 1;
			cursor.stepCount++;
		}
		
		if (cursor.y != toCursor.y) {
			cursor.y += (toCursor.y - cursor.y < 0) ? -1 : 1;
			cursor.stepCount++;
		}
		
		paintCursorToLineMap(lineMap, lineNumber, cursor);
	}
	
	return cursor;
}

const startTime = Date.now();

const lineMap = {};

for (let lineNumber = 0; lineNumber < lineCommands.length; lineNumber++) {
	const commands = lineCommands[lineNumber];
	let cursor = {x: 0, y: 0, stepCount: 0};
	for (const command of commands) {
		cursor = addLineToMap(lineMap, lineNumber, cursor, command);
	}
}

let fewestCombinedSteps = null;
for (location in lineMap) {
	if (Object.keys(lineMap[location]).length > 1) {
		let combinedSteps = lineMap[location][0] + lineMap[location][1];
		
		// Skip the origin where they always intersect. We're looking for the intersection steps closest to the origin, not the origin itself.
		if (combinedSteps == 0) continue;
		
		if (fewestCombinedSteps == null || fewestCombinedSteps > combinedSteps) {
			fewestCombinedSteps = combinedSteps;
		}
	}
}

console.log(`Code Run Time: ${Date.now() - startTime}`);

console.log(`Closest Intersection Steps to Origin: ${fewestCombinedSteps}`);