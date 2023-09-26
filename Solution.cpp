// Lab3_3sem.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//
#include <iostream>
//#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include "Graph.h"
#include "Problem.h"
#include "Sequence.h"
#include <fstream>
using namespace std;

int main() {
	Graph<Town, Way> graph = Graph<Town, Way>();
	
	int x_a;
	int y_a;
	int x_b;
	int y_b;
	int length;
	int cost;
	int count = 0;
	Point<Town>* p1 = NULL;
	Point<Town>* p2 = NULL;
	Point<Town>* pointer1 = NULL;
	Point<Town>* pointer2 = NULL;
	int size;
	std::cin >> size;
	//fin >> size;
	Point<Town>* points_arr = new Point<Town>[size*2];
	do {
		
		std::cin >> x_a;
		std::cin >> y_a;
		std::cin >> x_b;
		std::cin >> y_b;
		std::cin >> length;
		std::cin >> cost;
		points_arr[count] = Point<Town>(Town(x_a, y_a));
		points_arr[count +1] = Point<Town>(Town(x_b, y_b));
		for (int k = 0; k < count+2; k++)
		{
			if (points_arr[k].Get_data().Get_X() == x_a && points_arr[k].Get_data().Get_Y() == y_a) {
				p1 = &points_arr[k];
				break;
			}
		}
		for (int m = 0; m < count + 2; m++)
		{
			if (points_arr[m].Get_data().Get_X() == x_b && points_arr[m].Get_data().Get_Y() == y_b) {
				p2 = &points_arr[m];
				break;
			}
		}
		graph.Add(p1, p2, Way(length, cost));
		count += 2;
	} while (count != size*2);
	
	int x_start;
	int y_start;
	int x_end;
	int y_end;
	std::cin >> x_start;
	std::cin >> y_start;
	std::cin >> x_end;
	std::cin >> y_end;
	Point<Town>* start = NULL;
	Point<Town>* end = NULL;
	for (int i = 0; i < count; i++)
	{
		if (points_arr[i].Get_data().Get_X() == x_start && points_arr[i].Get_data().Get_Y() == y_start) {
			start = &points_arr[i];
			break;
		}
	}
	for (int j = 0; j < count; j++)
	{
		if (points_arr[j].Get_data().Get_X() == x_end && points_arr[j].Get_data().Get_Y() == y_end) {
			end = &points_arr[j];
			break;
		}
	}
	
	Route<Town, Way> graph2 = graph.Get_shortest_way(start, end);
	Sequence<Connection<Town, Way>> lines = graph2.Get_line_list();
	for (int a = 0; a < lines.size(); a++)
	{
		std::cout << lines.peek(a).Get_start()->Get_data().Get_X() << " ";
		std::cout << lines.peek(a).Get_start()->Get_data().Get_Y() << " ";
		std::cout << lines.peek(a).Get_end()->Get_data().Get_X() << " ";
		std::cout << lines.peek(a).Get_end()->Get_data().Get_Y() << "\n";
	}
	delete[] points_arr;
	return 0;
}

