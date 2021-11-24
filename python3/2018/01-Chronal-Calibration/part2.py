import pathlib

input: str = pathlib.Path("input.txt").read_text()

values = input.split("\n")

current_frequency = 0
previous_frequencies = []
previous_frequencies.append(current_frequency)
frequency_duplicate = None

while frequency_duplicate == None:
	for value in values:
		integer = int(value)
		current_frequency = current_frequency + integer
		previous_frequencies.append(current_frequency)
		number_of_occurrences = previous_frequencies.count(current_frequency)
		if number_of_occurrences > 1:
			frequency_duplicate = current_frequency
			break

print(frequency_duplicate)