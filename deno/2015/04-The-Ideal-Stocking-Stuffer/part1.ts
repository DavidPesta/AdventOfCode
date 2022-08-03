import { encode } from "https://deno.land/std@0.150.0/encoding/hex.ts";
import { crypto } from "https://deno.land/std@0.150.0/crypto/mod.ts";

const input = Deno.readTextFileSync("input.txt");

let i = 0;
while(true) {
	const hash = new TextDecoder().decode(
		encode(
			new Uint8Array(
				await crypto.subtle.digest(
					"MD5",
					new TextEncoder().encode(`${input}${i}`),
				),
			)
		)
	);
	
	if (hash.slice(0, 5) == "00000") {
		console.log(i);
		break;
	}
	
	i++;
}
