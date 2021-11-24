import pathlib

def parse_claim(claim: str):
	result = {}
	
	claim_parts = claim.split(" ")
	
	result["ID"] = claim_parts[0][1:]
	result["StartX"] = int(claim_parts[2].split(",")[0])
	result["StartY"] = int(claim_parts[2].split(",")[1][:-1])
	result["Width"] = int(claim_parts[3].split("x")[0])
	result["Height"] = int(claim_parts[3].split("x")[1])
	result["EndX"] = result["StartX"] + result["Width"]
	result["EndY"] = result["StartY"] + result["Height"]
	
	return result

def print_fabric(fabric):
	fabric_map = ""
	
	width = len(fabric)
	height = len(fabric[0])

	for y in range(height):
		for x in range(width):
			fabric_map = fabric_map + fabric[x][y]
		fabric_map = fabric_map + "\n"
	
	print(fabric_map)

input: str = pathlib.Path("input.txt").read_text()

claims = input.split("\n")

whole_fabric_width = 0
whole_fabric_height = 0

for claim in claims:
	parsed_claim = parse_claim(claim)
	
	if whole_fabric_width < parsed_claim["EndX"]:
		whole_fabric_width = parsed_claim["EndX"]
	
	if whole_fabric_height < parsed_claim["EndY"]:
		whole_fabric_height = parsed_claim["EndY"]

whole_fabric = []
for i in range(whole_fabric_width):
	whole_fabric.append([])
	for j in range(whole_fabric_height):
		whole_fabric[i].append(".")

for claim in claims:
	parsed_claim = parse_claim(claim)
	
	for y in range(parsed_claim["StartY"], parsed_claim["EndY"]):
		for x in range(parsed_claim["StartX"], parsed_claim["EndX"]):
			if whole_fabric[x][y] != ".":
				whole_fabric[x][y] = "X"
			else:
				whole_fabric[x][y] = parsed_claim["ID"]

#print_fabric(whole_fabric)

number_of_overlaps = 0

for x_values_list in whole_fabric:
	for y_value in x_values_list:
		if y_value == "X":
			number_of_overlaps = number_of_overlaps + 1

print(number_of_overlaps)