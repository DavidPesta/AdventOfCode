import pathlib

input: str = pathlib.Path("input.txt").read_text()

ids = input.split("\n")

how_many_twos = 0
how_many_threes = 0

for id in ids:
	characters = list(id)
	character_counts = {}
	for character in characters:
		if (character in character_counts) == False:
			character_counts[character] = 0
		character_counts[character] = character_counts[character] + 1
	two_count_exists = False
	three_count_exists = False
	for _, val in character_counts.items():
		if val == 2:
			two_count_exists = True
		if val == 3:
			three_count_exists = True
	if two_count_exists == True:
		how_many_twos = how_many_twos + 1
	if three_count_exists == True:
		how_many_threes = how_many_threes + 1

print(how_many_twos * how_many_threes)