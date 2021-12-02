input = open('input.txt', 'r').read().split("\n")

depths = list(map(lambda s: int(s), input))

increases = 0;
for i in range(1, len(depths)):
	if depths[i] > depths[i-1]:
		increases += 1

print(increases)