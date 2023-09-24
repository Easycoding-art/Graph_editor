#pragma once
#include "Sequence.h"
#include <iostream>
#include <string>
using namespace std;

template <typename T> class Point {
public :
	Point(T value) {
		data = value;
	}
	Point() {}
	T Get_data() const{
		return data;
	}
	friend bool operator == (const Point<T>& p1, const Point<T>& p2) {
		bool result = true;
		if (p1.Get_data() == p2.Get_data()) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};
private:
	T data;
};

template <typename Data, typename Parameters>class Connection {
public :
	Connection(Point<Data>* start, Point<Data>* end, Parameters param) {
		start_point = start;
		end_point = end;
		parameters = param;
	}
	Connection() {}

	Point<Data>* Get_end() const {
		return this->end_point;
	}

	Point<Data>* Get_start() const {
		return this->start_point;
	}

	Parameters Get_parameters() const {
		return this->parameters;
	}

	friend bool operator == (const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		bool result = true;
		if (connection1.Get_parameters() == connection2.Get_parameters() && connection1.Get_start() == connection2.Get_start() && connection1.Get_end() == connection2.Get_end()) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};
	friend bool operator >=(const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		return connection1.Get_parameters() >= connection2.Get_parameters();
	};

	friend bool operator != (const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		bool result = true;
		if (connection1.Get_parameters() != connection2.Get_parameters() || connection1.Get_start() != connection2.Get_start() || connection1.Get_end() != connection2.Get_end()) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};

	friend bool operator <=(const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		return connection1.Get_parameters() <= connection2.Get_parameters();
	};

	friend bool operator >(const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		return connection1.Get_parameters() > connection2.Get_parameters();
	};

	friend bool operator <(const Connection<Data, Parameters>& connection1, const Connection<Data, Parameters>& connection2) {
		return connection1.Get_parameters() < connection2.Get_parameters();
	};

private:
	Point<Data>* start_point;
	Point<Data>* end_point;
	Parameters parameters;
};

template <typename D, typename P> class Route
{
public:
	Route(Sequence<Connection<D, P>> seq) {
		connection_sequence = seq;
	};
	Sequence<Point<D>> Get_point_list() {
		Sequence<Point<D>> seq = Sequence<Point<D>>();
		for (int i = 0; i < connection_sequence.size(); i++)
		{
			if (seq.contains(connection_sequence.peek(i).Get_start())) {
				seq = seq.add(connection_sequence.peek(i).Get_start());
			}

			if (seq.contains(connection_sequence.peek(i).Get_end())) {
				seq = seq.add(connection_sequence.peek(i).Get_end());
			}
		}
		return seq;
	}

	Sequence<Connection<D, P>> Get_line_list() {
		return connection_sequence;
	}

	Connection<D, P> Get_connection(int i) {
		return connection_sequence.peek(i);
	}
	//~Route();

private:
	Sequence<Connection<D, P>> connection_sequence;
};


template <typename D, typename P> class Graph {
public:
	Graph() {
		connection_sequence = Sequence<Connection<D, P>>();
	}

	Graph(Sequence<Connection<D, P>> sequence) {
		connection_sequence = sequence;
	}
	void Add(Point<D>* point1, Point<D>* point2, P connection_data) {
		Connection<D, P> line1_2 = Connection<D, P>(point1, point2, connection_data);
		connection_sequence = connection_sequence.add(line1_2);
	}

	Sequence<Point<D>> Get_point_list() {
		Sequence<Point<D>> seq = Sequence<Point<D>>();
		for (int i = 0; i < connection_sequence.size(); i++)
		{
			if (seq.contains(connection_sequence.peek(i).Get_start())) {
				seq = seq.add(connection_sequence.peek(i).Get_start());
			}

			if (seq.contains(connection_sequence.peek(i).Get_end())) {
				seq = seq.add(connection_sequence.peek(i).Get_end());
			}
		}
		return seq;
	}

	Connection<D, P> Get_connection(int i) {
		return connection_sequence.peek(i);
	}
	int Graph_size() {
		return connection_sequence.size();
	}
	Route<D, P> Get_shortest_way(Point<D>* point1, Point<D>* point2) {
		Sequence<Connection<D, P>> copy_seq = connection_sequence;
		int t = 0;
		Connection<D, P> line;
		int repeat = 1;
		Point<D>* start = point1;
		P parameter;
		int k = 0;
		P length;
		Sequence<Connection<D, P>> result_sequence = Sequence<Connection<D, P>>();
		Sequence<Sequence<Connection<D, P>>> last_seq = Sequence<Sequence<Connection<D, P>>>();
		Sequence<Connection<D, P>> set_seq = Sequence<Connection<D, P>>();
		Sequence<P> weight_seq = Sequence<P>();
		int j = 0;
		int ex = 0;
		int none = 0;
		if (connection_sequence.size() == 0) {
			return Route<D, P>(Sequence<Connection<D, P>>());
		}
		else {
			while (repeat != 0) {
				t = 0;
				while (true)
				{
					line = copy_seq.peek(t);
					for (int c = 0; c < copy_seq.size(); c++) {
						if (line.Get_end() == copy_seq.peek(c).Get_end() && line.Get_start() == copy_seq.peek(c).Get_start() && line.Get_parameters() > copy_seq.peek(c).Get_parameters()) {
							copy_seq = copy_seq.swap(c, copy_seq.size() - 1);
							copy_seq = copy_seq.pop();
						}
					}
					if (t >= copy_seq.size() - 1) {
						break;
					}
					t++;
				}

				start = point1;
				k = 0;
				//length = 0;
				result_sequence = Sequence<Connection<D, P>>();
				j = 0;
				while (copy_seq.size() != 0) {
					ex = 0;
					for (int w = 0; w < copy_seq.size(); w++)
					{
						if (copy_seq.peek(w).Get_start() != start) {
							ex++;
						}
					}
					if (ex == copy_seq.size()) {
						for (int l = 0; l < copy_seq.size(); l++)
						{
							copy_seq = copy_seq.pop();
						}
					}
					if (copy_seq.peek(j).Get_start() == start) {
						result_sequence = result_sequence.add(copy_seq.peek(j));
						start = copy_seq.peek(j).Get_end();
						if (start == point2 && start != point1) {
							last_seq = last_seq.add(result_sequence);
							length = result_sequence.peek(0).Get_parameters();
							for (int b = 1; b < result_sequence.size(); b++)
							{
								length = length + result_sequence.peek(b).Get_parameters();
							}
							//std::cout << length.Get_efficiency() << "\n";
							weight_seq = weight_seq.add(length);
							//length = 0;
							for (int i = 0; i < result_sequence.size(); i++)
							{
								result_sequence = result_sequence.pop();
							}
							if (result_sequence.size() > 0) {
								result_sequence = result_sequence.pop();
							}
							start = point1;
						}

						copy_seq = copy_seq.swap(j, copy_seq.size() - 1);
						copy_seq = copy_seq.pop();
						j = 0;
					}
					else if (copy_seq.peek(j).Get_start() != start && copy_seq.size() - 1 <= j) {
						start = point1;
						j = 0;
						for (int i = 0; i < result_sequence.size(); i++)
						{
							result_sequence = result_sequence.pop();
						}
						if (result_sequence.size() > 0) {
							result_sequence = result_sequence.pop();
						}
					}
					else {
						j++;
					}
				}

				copy_seq = Sequence<Connection<D, P>>();
				start = point1;
				for (int x = 0; x < connection_sequence.size(); x++) {
					for (int y = 0; y < last_seq.size(); y++) {
						//std::cout << last_seq.peek(y).peek(0).Get_start()->Get_data().Get_town_name() << "->";
						//std::cout << last_seq.peek(y).peek(0).Get_end()->Get_data().Get_town_name() << "\n";
						if (connection_sequence.peek(x) != last_seq.peek(y).peek(0) && !(set_seq.contains(connection_sequence.peek(x)))) {
							copy_seq = copy_seq.add(connection_sequence.peek(x));
							set_seq = set_seq.add(connection_sequence.peek(x));
						}
					}
				}
				none = 0;
				for (int z = 0; z < copy_seq.size(); z++)
				{
					if (copy_seq.peek(z).Get_start() != start) {
						none++;
					}
				}
				if (none == copy_seq.size()) {// || last_seq.size() == 0) {
					repeat = 0;
				}
				else {
					repeat = 1;
				}
			}

			for (int k = 0; k < weight_seq.size(); k++) {
				for (int m = 0; m < weight_seq.size() - 1; m++) {
					if (weight_seq.peek(m) > weight_seq.peek(m + 1)) {
						weight_seq = weight_seq.swap(m, m + 1);
						last_seq = last_seq.swap(m, m + 1);
					}
				}
			}
			//std::cout << last_seq.size() << "\n";
			return Route<D, P>(last_seq.peek(0));
		}
	}

private:
	Sequence<Connection<D, P>> connection_sequence;
};