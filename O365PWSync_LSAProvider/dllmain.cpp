#include "stdafx.h"
#include <Windows.h>
#include <WinSock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
#include <SubAuth.h>
#include <process.h>
#include <codecvt>

#pragma comment(lib, "Ws2_32.lib")

using namespace std;

struct PasswordFilterAccount {
	PUNICODE_STRING AccountName;
	PUNICODE_STRING FullName;
	PUNICODE_STRING Password;
};

bool bPasswordOk = true;

//
// make sure all data is sent through the socket
//
int sendall(SOCKET s, const char *buf, int *len) {
	int total = 0;        // how many bytes we've sent
	int bytesleft = *len; // how many we have left to send
	int n;

	while (total < *len) {
		n = send(s, buf + total, bytesleft, 0);
		if (n == -1) { break; }
		total += n;
		bytesleft -= n;
	}

	*len = total; // return number actually sent here

	return n == -1 ? -1 : 0; // return -1 onm failure, 0 on success
}

// Regular DLL boilerplate

BOOL __stdcall APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved) {
	//WSADATA wsa;
	FILE *f = NULL;

	switch (ul_reason_for_call) {
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

//
// We don't have any setup to do here, since the core business logic
// for evaluating passwords lives in the separate OPFService.exe 
// project.  So, we can just say we've initialized immediately.
//

extern "C" __declspec(dllexport) BOOLEAN __stdcall InitializeChangeNotify(void) {
	return TRUE;
}

//
// Assuming that a socket connection has been successfully accomplished
// with the password filter service, this function will handle the
// query for the user's password and determine whether it is an approved
// password or not.  The server will respond with "true" or "false", 
// though for simplicity here I just check the first character. 
// 
// Here is a sample query:
//
//    <connect>
//    client:   test\n
//    client:   Username\n
//    client:   Password1\n
//    server:   false\n
//    <disconnect>
//
void askServerNotify(SOCKET sock, PUNICODE_STRING AccountName, PUNICODE_STRING Password) {
	using convert_type = std::codecvt_utf8<wchar_t>;
	std::wstring_convert<convert_type, wchar_t> converter;
	const char *preamble = "notify\n"; //command that is used to start password testing
	int i;
	int len;

	i = send(sock, preamble, (int)strlen(preamble), 0); //send test command
	if (i != SOCKET_ERROR) {
		std::wstring wUsername(AccountName->Buffer, AccountName->Length / sizeof(WCHAR));
		wUsername.push_back('\n');

		std::wstring wPassword(Password->Buffer, Password->Length / sizeof(WCHAR));
		wPassword.push_back('\n');

		std::string sUsername = converter.to_bytes(wUsername);
		std::string sPassword = converter.to_bytes(wPassword);

		std::string sMessage = sUsername + sPassword;

		const char * cMessage = sMessage.c_str();
		len = static_cast<int>(sMessage.size());
		i = sendall(sock, cMessage, &len);
	}
	else {
		//report error
	}
}

void askServerChange(SOCKET sock, PUNICODE_STRING AccountName, PUNICODE_STRING Password) {
	using convert_type = std::codecvt_utf8<wchar_t>;
	std::wstring_convert<convert_type, wchar_t> converter;
	char rcBuffer[1024];
	const char *preamble = "test\n"; //command that is used to start password testing
	int i;
	int len;

	i = send(sock, preamble, (int)strlen(preamble), 0); //send test command
	if (i != SOCKET_ERROR) {
		std::wstring wUsername(AccountName->Buffer, AccountName->Length / sizeof(WCHAR));
		wUsername.push_back('\n');

		std::wstring wPassword(Password->Buffer, Password->Length / sizeof(WCHAR));
		wPassword.push_back('\n');

		std::string sUsername = converter.to_bytes(wUsername);
		std::string sPassword = converter.to_bytes(wPassword);

		std::string sMessage = sUsername + sPassword;

		const char * cMessage = sMessage.c_str();
		len = static_cast<int>(sMessage.size());
		i = sendall(sock, cMessage, &len);

		//i = send(sock, sPassword.c_str(), sPassword.size(), 0);

		if (i != SOCKET_ERROR) {
			i = recv(sock, rcBuffer, sizeof(rcBuffer), 0);//read response
			if (i > 0 && rcBuffer[0] == 'f') {
				bPasswordOk = FALSE;
			}
		}
		else {
			//report error
		}
	}
	else {
		//report error
	}
}

//
// In this function, we establish a TCP connection to 127.0.0.1:5999 and determine
// whether the indicated password is acceptable according to the filter service.
// The service is a C# program also in this solution, titled "OPFService".
//
unsigned int __stdcall NotifySocket(void *v) {
	//the account object
	PasswordFilterAccount *pfAccount = static_cast<PasswordFilterAccount*>(v);

	SOCKET sock = INVALID_SOCKET;
	struct addrinfo *result = NULL;
	struct addrinfo *ptr = NULL;
	struct addrinfo hints;
	bPasswordOk = TRUE; // set fail open

	int i;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// This butt-ugly loop is straight out of Microsoft's reference example
	// for a TCP client.  It's not my style, but how can the reference be
	// wrong? ;-)
	i = getaddrinfo("127.0.0.1", "5999", &hints, &result);
	if (i == 0) {
		for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {
			sock = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
			if (sock == INVALID_SOCKET) {
				break;
			}
			i = connect(sock, ptr->ai_addr, (int)ptr->ai_addrlen);
			if (i == SOCKET_ERROR) {
				closesocket(sock);
				sock = INVALID_SOCKET;
				continue;
			}
			break;
		}

		if (sock != INVALID_SOCKET) {
			askServerNotify(sock, pfAccount->AccountName, pfAccount->Password);
			closesocket(sock);
		}
	}

	return bPasswordOk;
}

unsigned int __stdcall ChangeSocket(void *v) {
	//the account object
	PasswordFilterAccount *pfAccount = static_cast<PasswordFilterAccount*>(v);

	SOCKET sock = INVALID_SOCKET;
	struct addrinfo *result = NULL;
	struct addrinfo *ptr = NULL;
	struct addrinfo hints;
	bPasswordOk = TRUE; // set fail open

	int i;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// This butt-ugly loop is straight out of Microsoft's reference example
	// for a TCP client.  It's not my style, but how can the reference be
	// wrong? ;-)
	i = getaddrinfo("127.0.0.1", "5999", &hints, &result);
	if (i == 0) {
		for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {
			sock = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
			if (sock == INVALID_SOCKET) {
				break;
			}
			i = connect(sock, ptr->ai_addr, (int)ptr->ai_addrlen);
			if (i == SOCKET_ERROR) {
				closesocket(sock);
				sock = INVALID_SOCKET;
				continue;
			}
			break;
		}

		if (sock != INVALID_SOCKET) {
			askServerChange(sock, pfAccount->AccountName, pfAccount->Password);
			closesocket(sock);
		}
	}

	return bPasswordOk;
}

extern "C" __declspec(dllexport) BOOLEAN __stdcall PasswordFilter(PUNICODE_STRING AccountName,
	PUNICODE_STRING FullName,
	PUNICODE_STRING Password,
	BOOLEAN SetOperation) {

	//build the account struct
	PasswordFilterAccount *pfAccount = new PasswordFilterAccount();
	pfAccount->AccountName = AccountName;
	pfAccount->Password = Password;

	//start an asynchronous thread to be able to kill the thread if it exceeds the timout
	HANDLE pfHandle = (HANDLE)_beginthreadex(0, 0, ChangeSocket, (LPVOID *)pfAccount, 0, 0);

	DWORD dWaitFor = WaitForSingleObject(pfHandle, 30000); //do not exceed the timeout
	if (dWaitFor == WAIT_TIMEOUT) {
		//timeout exceeded
	}
	else if (dWaitFor == WAIT_OBJECT_0) {
		//here is where we want to be
	}
	else {
		//WAIT_ABANDONED
		//WAIT_FAILED
	}

	if (pfHandle != INVALID_HANDLE_VALUE && pfHandle != 0) {
		if (CloseHandle(pfHandle)) {
			pfHandle = INVALID_HANDLE_VALUE;
		}
	}

	return bPasswordOk;
}


extern "C" __declspec(dllexport) int __stdcall
PasswordChangeNotify(PUNICODE_STRING UserName,
	ULONG RelativeId,
	PUNICODE_STRING NewPassword) {

	//build the account struct
	PasswordFilterAccount *pfAccount = new PasswordFilterAccount();
	pfAccount->AccountName = UserName;
	pfAccount->Password = NewPassword;

	//start an asynchronous thread to be able to kill the thread if it exceeds the timout
	HANDLE pfHandle = (HANDLE)_beginthreadex(0, 0, NotifySocket, (LPVOID *)pfAccount, 0, 0);

	DWORD dWaitFor = WaitForSingleObject(pfHandle, 30000); //do not exceed the timeout
	if (dWaitFor == WAIT_TIMEOUT) {
		//timeout exceeded
	}
	else if (dWaitFor == WAIT_OBJECT_0) {
		//here is where we want to be
	}
	else {
		//WAIT_ABANDONED
		//WAIT_FAILED
	}

	if (pfHandle != INVALID_HANDLE_VALUE && pfHandle != 0) {
		if (CloseHandle(pfHandle)) {
			pfHandle = INVALID_HANDLE_VALUE;
		}
	}

	return 0;
}
