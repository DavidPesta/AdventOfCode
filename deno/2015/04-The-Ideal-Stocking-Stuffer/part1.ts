import easy from "https://deno.land/x/easyutil@0.10.0/mod.ts";
import { HashAlgorithm } from "https://deno.land/x/easyutil@0.10.0/types.ts";

const input = Deno.readTextFileSync("input.txt");

let i = 0;
while(true) {
	const hash = await easy.cryptography.hash(HashAlgorithm.MD5, `${input}${i}`);
	
	if (hash.slice(0, 5) == "00000") {
		console.log(i);
		break;
	}
	
	i++;
}
