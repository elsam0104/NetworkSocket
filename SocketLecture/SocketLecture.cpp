#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

#include <thread>
#include <vector>
#include <string>
#include <iostream>

// Need to link with Ws2_32.lib
#pragma comment (lib, "Ws2_32.lib")
// #pragma comment (lib, "Mswsock.lib")

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

struct client_type {
	int id;
	SOCKET socket;
};

const char OPTION_VALUE = 1;
const int MAX_CLIENTS = 7;

//전방선언
int proocess_client(client_type& new_client, std::vector<client_type>& client_array, std::thread& thread);
int main();


int proocess_client(client_type& new_client, std::vector<client_type>& client_array, std::thread& thread)
{
	std::string msg = "";
	char tempmsg[DEFAULT_BUFLEN] = "";
	
	while (true)
	{
		memset(tempmsg, 0, DEFAULT_BUFLEN);

		if (new_client.socket != 0)
		{
			int iResult = recv(new_client.socket, tempmsg, DEFAULT_BUFLEN, 0);

			if (iResult != SOCKET_ERROR)
			{
				if (strcmp("", tempmsg))//보낸 메시지가 있을 떄(strcmp는 같으면 0 반환)
				{
					msg = "Client #" + std::to_string(new_client.id) + ": " + tempmsg;
				}
				std::cout << msg.c_str() << std::endl;

				for (int i = 0; i < MAX_CLIENTS; i++)
				{
					if (new_client.id != i)//메시지를 보낸 애한텐 보낼필요 없으니 체크
					{
						iResult = send(client_array[i].socket, msg.c_str(), strlen(msg.c_str()), 0);
					}
				}
			}
			else //소켓이 나갔다면
			{
				msg = "Client #" + std::to_string(new_client.id) + "Disconnected";
				std::cout << msg << std::endl;

				closesocket(new_client.socket);
				closesocket(client_array[new_client.id].socket);
				client_array[new_client.id].socket = INVALID_SOCKET;

				for (int i = 0; i < MAX_CLIENTS; i++)
				{
					if (client_array[i].socket != INVALID_SOCKET)
					{
						iResult = send(client_array[i].socket, msg.c_str(), strlen(msg.c_str()), 0); 
						//얘들아 *번 소켓이 나갔대 웅성웅성
					}
				}
				break;
			}
		}
		thread.detach();
		return 0;
	}

}


int main(void)
{
	WSADATA wsaData; //윈소켓 정보
	int iResult;

	SOCKET server_socket = INVALID_SOCKET;

	struct addrinfo* server = NULL;
	struct addrinfo hints;

	std::string msg = "";
	std::vector<client_type> client(MAX_CLIENTS);
	int num_clients = 0;
	int temp_id = -1;
	std::thread my_thread[MAX_CLIENTS];

	// Initialize Winsock
	std::cout << "Initializing winsock..." << std::endl;
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;


	// Resolve the server address and port
	std::cout << "Setting up server..." << std::endl;
	getaddrinfo(NULL, DEFAULT_PORT, &hints, &server);

	std::cout << "Creating server socket..." << std::endl;
	server_socket = socket(server->ai_family, server->ai_socktype, server->ai_protocol);//tcp 소켓 생성

	setsockopt(server_socket, SOL_SOCKET, SO_REUSEADDR, &OPTION_VALUE, sizeof(int));
	setsockopt(server_socket, IPPROTO_TCP, TCP_NODELAY, &OPTION_VALUE, sizeof(int));

	std::cout << "Binding socket..." << std::endl;
	bind(server_socket, server->ai_addr, (int)server->ai_addrlen);//주소 지정

	std::cout << "Listening socket..." << std::endl;
	listen(server_socket, SOMAXCONN);

	for (int i = 0; i < MAX_CLIENTS; i++)
	{
		client[i] = { -1,INVALID_SOCKET };
	}

	while (true)
	{
		SOCKET incoming = INVALID_SOCKET;
		incoming = accept(server_socket, NULL, NULL);

		if (incoming == INVALID_SOCKET)
			continue;
		num_clients = -1;
		temp_id = -1;

		for (int i = 0; i < MAX_CLIENTS; i++)
		{
			if (client[i].socket = INVALID_SOCKET && temp_id == -1)
			{
				client[i].socket = incoming;
				client[i].id = i;
				temp_id = i;
			}
			if (client[i].socket != INVALID_SOCKET)
			{
				num_clients++;
			}
		}
		if (temp_id != -1)
		{
			std::cout << "Client #" << client[temp_id].id << " Accepted" << std::endl;
			msg = std::to_string(client[temp_id].id);
			send(client[temp_id].socket, msg.c_str(), strlen(msg.c_str()), 0);

			my_thread[temp_id] = std::thread(proocess_client, std::ref(client[temp_id]), std::ref(client), std::ref(my_thread[temp_id]));
		}
		else
		{
			msg = "Server is Full";

			send(incoming, msg.c_str(), strlen(msg.c_str()), 0);
			std::cout << msg << std::endl;
		}
	}

	// No longer need server socket
	closesocket(server_socket);

	for (int i = 0; i < MAX_CLIENTS; i++)
	{
		my_thread[i].detach(); //자원 폐기하고 사라짐
		closesocket(client[i].socket);
	}

	WSACleanup();
	std::cout << "Program has ended successfuly." << std::endl;


	return 0;
}