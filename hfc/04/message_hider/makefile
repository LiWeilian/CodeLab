encrypt.o: encrypt.c
	gcc -c encrypt.c
message_hider.o:message_hider.c
	gcc -c message_hider.c
message_hider.exe:encrypt.o message_hider.o
	gcc encrypt.o message_hider.o -o message_hider.exe