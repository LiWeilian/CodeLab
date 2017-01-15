#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <stdlib.h>
#include <netdb.h>
#include <sys/socket.h>
#include <netinet/in.h>

void error(char *msg)
{
	fprintf(stderr, "%s: %s\n", msg, strerror(errno));
	exit(1);
}

char* my_itoa(int i)
{
	char *a;
	sprintf(a, "%d\0", i);
	return a;
}

int my_getaddrinfo(char *host, char *port, char *ip)
{
	struct addrinfo *res, *curr;
	struct addrinfo hints;
	//bzero(&hints, sizeof(hints));
	memset(&hints, 0, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	if (getaddrinfo(host, port, &hints, &res) == -1)
		error("Can't resolve the address");
	/*
	char ipstr[16];
	for (curr = res; curr != NULL; curr=curr->ai_next)
	{
		inet_ntop(AF_INET, &(((struct sockaddr_in *)(curr->ai_addr))->sin_addr), ipstr, 16);
		printf("%s\n", ipstr);
	}
	*/
	if (res != NULL)
	{
		char ipstr[16];
		inet_ntop(AF_INET, &(((struct sockaddr_in *)(res->ai_addr))->sin_addr), ip, 16);
		return 0;
	}
	else
	{
		return -1;
	}
}

int open_socket(char *host, int port)
{
	int d_sock = socket(PF_INET, SOCK_STREAM, 0);
	if (d_sock == -1)
		error("Can't open socket");
	struct sockaddr_in addr;
	addr.sin_family = PF_INET;
	addr.sin_port = (in_port_t)htons(port);
	addr.sin_addr.s_addr = inet_addr(host);
	int c = connect(d_sock, (struct sockaddr *)&addr, sizeof(struct sockaddr_in));
	if (c == -1)
		error("Can't connect to socket");
	return d_sock;
}

int say(int socket, char *s)
{
	int result = send(socket, s, strlen(s), 0);
	if (result == -1)
		fprintf(stderr, "%s: %s\n", "Error talking to the server", strerror(errno));
	return result;
}

int main(int argc, char *argv[])
{
	short iport = 30000;
	char *host = "127.0.0.1";
	
	int d_sock;
	d_sock = open_socket(host, iport);

	char rec[1024];
	int bytesRevd = recv(d_sock, rec, 1023, MSG_WAITALL);
	while (bytesRevd)
	{
		if (bytesRevd == -1)
			error("Can't read from server");
		rec[bytesRevd] = '\0';
		printf("%s", rec);//遇到\n才输出，否则要等运行完
		bytesRevd = recv(d_sock, rec, 1023, MSG_WAITALL);			
	}
	close(d_sock);
	
	return 0;
}