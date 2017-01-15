#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <netdb.h>

void error(char *msg)
{
	fprintf(stderr, "%s: %s\n", msg, strerror(errno));
	exit(1);
}

int open_socket(char *host, char *port)
{
	/*
	struct addrinfo *res;
	struct addrinfo hints;
	memset(&hints, 0, sizeof(hints));
	hints.ai_family = PF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	if (getaddrinfo(host, port, &hints, &res) == -1)
		error("Can't resolve the address");
	//printf("ai_addr:%s\n", res->ai_addr);
	printf("ai_family:%d\nai_socktype:%d\nai_protocl:%d\n", res->ai_family, res->ai_socktype, res->ai_protocol);
	int d_sock =  socket(res->ai_family, res ->ai_socktype, res->ai_protocol);
	if (d_sock == -1)
		error("Can't open socket");
	
	int c = connect(d_sock, res->ai_addr, res->ai_addrlen);
	freeaddrinfo(res);
	if (c == -1)
		error("Can't connect to socket");
	*/
	int d_sock = socket(PF_INET, SOCK_STREAM, 0);
	if (d_sock == -1)
		error("Can't open socket");
	struct sockaddr_in addr;
	addr.sin_family = PF_INET;
	addr.sin_port = (in_port_t)htons(80);
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
	int d_sock;
	d_sock = open_socket("172.16.1.101", "80");
	
	//char buf[1024];
	//sprintf(buf, "GET http://172.16.1.101/test.htm HTTP/1.1\r\nHost: 172.16.1.101\r\nConnection: keep-alive\r\nCache-Control: max-age=0\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\nUpgrade-Insecure-Requests: 1\r\nUser-Agent: Mozilla/5.0 (Windows NT 10.0;// WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36\r\nAccept-Encoding: none\r\nAccept-Language: zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4\r\n\r\n", argv[1]);
	//say(d_sock, buf);
	
	
	char buf[1024];
	//sprintf(buf, "GET /EchoWcfService/UserService/getuser/%s HTTP/1.1\r\nHost: 172.16.1.101\r\n\r\n", argv[1]);
	sprintf(buf, "GET /EchoWcfService/UserService/getuser/%s HTTP/1.1\r\nHost: 172.16.1.101\r\nConnection: keep-alive\r\nCache-Control: max-age=0\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\nUpgrade-Insecure-Requests: 1\r\nUser-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.80 Safari/537.36\r\nAccept-Encoding: gzip, deflate, sdch\r\nAccept-Language: zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4\r\nCookie: ASP.NET_SessionId=uwu54hgedahywjwgpyelgvwc\r\n\r\n", argv[1]);

	say(d_sock, buf);
	
	char rec[1024];
	int bytesRevd = recv(d_sock, rec, 1023, 0);
	while (bytesRevd)
	{
		if (bytesRevd == -1)
			error("Can't read from server");
		
		rec[bytesRevd] = '\0';
		printf("%s\n", rec);
		//printf("\n");
		bytesRevd =  recv(d_sock, rec, 1023, 0);
	}
	
	close(d_sock);
	return 0;
}
