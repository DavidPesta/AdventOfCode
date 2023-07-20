const input = await Bun.file("input.txt").text();

const start = performance.now();

const score = input
	.split("\n")
	.map(x => {
			const a = x.charCodeAt(0) - 64;
			const b = x.charCodeAt(2) - 87;
			
			if (a == b) return b + 3;
			
			if (
				(b == 1 && a == 3) ||
				(b == 2 && a == 1) ||
				(b == 3 && a == 2)
			)
				return b + 6;
			
			return b + 0;
		}
	)
	.reduce((a, b) => a + b)

const end = performance.now();

console.log(`${score} - ${end - start}`);
