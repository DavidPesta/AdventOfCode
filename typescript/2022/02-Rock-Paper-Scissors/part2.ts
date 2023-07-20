const input = await Bun.file("input.txt").text();

const start = performance.now();

const score = input
	.split("\n")
	.map(x => {
			const a = x.charCodeAt(0) - 64;
			const r = x[2];
			
			let b = 0;
			
			if (r == "X") {
				if (a == 1) b = 3;
				if (a == 2) b = 1;
				if (a == 3) b = 2;
				return b + 0;
			}
			
			if (r == "Y") {
				b = a;
				return b + 3;
			}
			
			if (r == "Z") {
				if (a == 1) b = 2;
				if (a == 2) b = 3;
				if (a == 3) b = 1;
				return b + 6;
			}
			
			throw new Error(`Input line is malformed: ${x}`);
		}
	)
	.reduce((a, b) => a + b)

const end = performance.now();

console.log(`${score} - ${end - start}`);
