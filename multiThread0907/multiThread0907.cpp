#include<iostream>
#include<thread>
#include<time.h>
#include <mutex>
using namespace std;

void func1(int* a, int st, int ed, mutex* m, mutex* m2) {
	for (int i = st; i <= ed; i++)
	{
		m->lock();
		m2->lock();
		*a += i;
		cout << "thread 1" << endl;
		m2->unlock();
		m->unlock();
	}
}
void func2(int* a, int st, int ed, mutex* m, mutex* m2) {
	for (int i = st; i <= ed; i++)
	{
		while (true)
		{
			m2->lock();
			if (m->try_lock())
			{
				cout << "스레드 2" << endl;
				m->unlock();
				m2->unlock();
				break;
			}
			else 
			{
				m2->unlock();
				continue;			
			}
		}
	}
}
int main()
{
	int a = 0;
	clock_t s;
	mutex m, m2;

	s = clock();
#pragma region prev Code
	thread t1(func1, &a, 0, 5000, &m, &m2);
	thread t2(func1, &a, 5001, 10000, &m, &m2);
	//thread t3([] {
	//	for (int i = 0; i < 10; i++)
	//	{
	//		cout << "Thread 3 is running" << endl;
	//	}
	//	});
	t2.join();
	t1.join();
	//t3.join();
#pragma endregion

	cout <<a<<" " << (float)s / CLOCKS_PER_SEC;
	cout << "main is die"<<endl;
}