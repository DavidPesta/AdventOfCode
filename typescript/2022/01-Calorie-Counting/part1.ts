const input = await Bun.file("input.txt").text();


// Imperative

let start = performance.now();

let mostCalories = 0;
const elfCalorieLists = input.split("\n\n");
for (const elfCalorieList of elfCalorieLists) {
	const elfCalories = elfCalorieList.split("\n");
	
	let calories = 0;
	
	for (const elfCalorie of elfCalories) {
		calories += +elfCalorie;
	}
	
	if (calories > mostCalories) {
		mostCalories = calories;
	}
}

let end = performance.now();

console.log(`Imperative: ${mostCalories} - ${end - start}`);


// Declarative

start = performance.now();

mostCalories = input
	.split("\n\n")
	.map(x => x
		.split("\n")
		//.map(x => +x)
		.reduce((a, b) => +a + +b, 0)
	)
	.reduce((a, b) => a > b ? a : b)

end = performance.now();

console.log(`Declarative: ${mostCalories} - ${end - start}`);
