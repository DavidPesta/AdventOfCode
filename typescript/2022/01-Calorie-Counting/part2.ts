const input = await Bun.file("input.txt").text();


// Imperative

let start = performance.now();

const elfTotalCalorieList = [];
const elfCalorieLists = input.split("\n\n");
for (const elfCalorieList of elfCalorieLists) {
	const elfCalories = elfCalorieList.split("\n");
	
	let calories = 0;
	
	for (const elfCalorie of elfCalories) {
		calories += +elfCalorie;
	}
	
	elfTotalCalorieList.push(calories);
}

elfTotalCalorieList.sort((a, b) => b - a);

let top3ElfCalories = elfTotalCalorieList[0] + elfTotalCalorieList[1] + elfTotalCalorieList[2];

let end = performance.now();

console.log(`Imperative: ${top3ElfCalories} - ${end - start}`);


// Declarative

start = performance.now();

top3ElfCalories = input
	.split("\n\n")
	.map(x => x
		.split("\n")
		.reduce((a, b) => +a + +b, 0)
	)
	.sort((a, b) => b - a)
	.slice(0, 3)
	.reduce((a, b) => a + b, 0)

end = performance.now();

console.log(`Declarative: ${top3ElfCalories} - ${end - start}`);
