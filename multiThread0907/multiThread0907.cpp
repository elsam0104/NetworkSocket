#include<iostream>
#include<thread>
#include<time.h>
using namespace std;

void func1(int* a,int st, int ed) {
	for (int i = st; i <= ed; i++)
	{
		*a += i;
	}
}
int main()
{
	int a=0;
	clock_t s;

	s = clock();
	thread t1(func1,&a,1,3333);
	thread t2(func1,&a,3333,6666);
	thread t3(func1,&a,6666,10001);
	//thread t3([] {
	//	for (int i = 0; i < 10; i++)
	//	{
	//		cout << "Thread 3 is running" << endl;
	//	}
	//	});
	t1.join();
	t2.join();
	t3.join();
	cout <<a<<" " << (float)s/CLOCKS_PER_SEC;
	//cout << "main is die"<<endl;
}