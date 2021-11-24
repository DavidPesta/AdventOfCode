import pathlib

input: str = pathlib.Path("input.txt").read_text()

ids = input.split("\n")

for index, id1 in enumerate(ids):
	for id2 in ids[index+1:]:
		idlist1 = list(id1)
		idlist2 = list(id2)
		number_of_differences = 0
		position_of_difference = 0
		for i in range(len(idlist1)):
			if idlist1[i] != idlist2[i]:
				number_of_differences = number_of_differences + 1
				position_of_difference = i
		if number_of_differences == 1:
			del idlist1[position_of_difference]
			answer = "".join(idlist1)
			print(answer)
			exit()