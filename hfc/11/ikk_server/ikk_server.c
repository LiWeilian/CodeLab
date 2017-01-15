#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <sys/socket.h>
#include <arpa/inet.h>


int listener_d;

void error(char *msg)
{
	fprintf(stderr, "%s\n", msg);
	exit(1);
}

int read_in(int socket, char *buf, int len)
{
	char *s = buf;
	int slen = len;
	int c = recv(socket, s, slen, 0);
	while ((c > 0) && (s[c-1] != '\n')) {
		s += c;
		slen -= c;
		c = recv(socket, s, slen, 0);
	}
	if (c < 0)
		return c;
	else if (c == 0)
		buf[0] = '\0';
	else 
	 	s[c-1] = '\0';
	return len - slen;
}

int open_listener_socket()
{
	int s = socket(PF_INET, SOCK_STREAM, 0);
	if (s == -1)
		error("Can not open socket.");
	return s;
}

void bind_to_port(int socket, int port)
{
	struct sockaddr_in name;
	name.sin_family = PF_INET;
	name.sin_port = (in_port_t)htons(port);
	name.sin_addr.s_addr = htonl(INADDR_ANY);
	int c = bind(socket, (struct sockaddr *)&name, sizeof(name));
	if (c == -1)
	  error("Can not bind to port.");
}

int say(int socket, char *s)
{
	int result = send(socket, s, strlen(s), 0);
	if (result == -1)
		fprintf(stderr, "Can not send message.");
	return result;
}

int catch_signal(int sig, void (*handler)(int))
{
	struct sigaction action;
	action.sa_handler = handler;
	sigemptyset(&action.sa_mask);
	action.sa_flags = 0;
	return sigaction(sig, &action, NULL);
}

void handle_shutdown(int sig)
{
	if (listener_d)
		close(listener_d);
	fprintf(stderr, "Bye!\n");
	exit(0);
}

int main()
{
	if (catch_signal(SIGINT, handle_shutdown) == -1)
	{
		fprintf(stderr, "Can not map the handler.");
		exit(2);
	}

	listener_d = open_listener_socket();
	
	bind_to_port(listener_d, 30000);
	
	if (listen(listener_d, 10) == -1)
	  error("Can not listen.");
	  
	struct sockaddr_storage client_addr;
	unsigned int address_size = sizeof(client_addr);
	
	char read_buf[255];
	
	puts("Waiting for connection.");
	
	while (1)
	{
		int connect_d = accept(listener_d, (struct sockaddr *)&client_addr, &address_size);
		if (connect_d == -1)
	  		error("Can not open secondary socket.");
		if (!fork())
		{
			close(listener_d);
			if (say(connect_d, "Internet Knock-Knock Protocol Server\r\nVersion 1.0\r\nKnock-Knock!\r\n>") != -1)
			{
				read_in(connect_d, read_buf, sizeof(read_buf));
				if (strncasecmp("Who's there?", read_buf, 12))
				{
					say(connect_d, "You should say 'Who's there?'");				
				}
				else
				{
					if(say(connect_d, "Oscar\r\n>") != -1)
					{
						read_in(connect_d, read_buf, sizeof(read_buf));
						if (strncasecmp("Oscar Who?", read_buf, 10))
							say(connect_d, "You should say 'Oscar who?'!\r\n");
						else
							say(connect_d, "Oscar silly question, you get a silly answer.\r\n");
					}
				}
			}
			close(connect_d);
			exit(0);	
		}
		
		close(connect_d);
	}  
	
	return 0;
}