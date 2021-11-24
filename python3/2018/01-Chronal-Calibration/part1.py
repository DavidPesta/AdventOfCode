import pathlib

input: str = pathlib.Path("input.txt").read_text()

values = input.split("\n")

current = 0

for value in values:
	integer = int(value)
	current = current + integer

print(current)