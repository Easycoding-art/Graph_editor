#pragma once
#include <string>
using namespace std;
class Way
{
public:
	Way(int l, int money) {
		length = l;
		cost = money;
	}
	Way() {}

	int Get_efficiency() const{
		return length *cost;
	}

	int Get_length() const{
		return length;
	}

	int Get_cost() const{
		return cost;
	}

	friend bool operator == (const Way& connection1, const Way& connection2) {
		bool result = true;
		if (connection1.Get_efficiency() == connection2.Get_efficiency()) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};
	friend bool operator >=(const Way& connection1, const Way& connection2) {
		return connection1.Get_efficiency() >= connection2.Get_efficiency();
	};

	friend bool operator != (const Way& connection1, const Way& connection2) {
		bool result = true;
		if (connection1.Get_efficiency() != connection2.Get_efficiency()) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};

	friend bool operator <=(const Way& connection1, const Way& connection2) {
		return connection1.Get_efficiency() <= connection2.Get_efficiency();
	};

	friend bool operator >(const Way& connection1, const Way& connection2) {
		return connection1.Get_efficiency() > connection2.Get_efficiency();
	};

	friend bool operator <(const Way& connection1, const Way& connection2) {
		return connection1.Get_efficiency() < connection2.Get_efficiency();
	};

	friend Way operator +(const Way& connection1, const Way& connection2) {
		return Way(connection1.Get_length() + connection2.Get_length(), connection1.Get_cost() + connection2.Get_cost());
	};
private:
	int length;
	int cost;
};

class Town
{
public:
	Town(int x, int y) {
		X = x;
		Y = y;
	}
	Town() {}

	int Get_X() {
		return X;
	}

	int Get_Y() {
		return Y;
	}

	friend bool operator == (const Town& town1, const Town& town2) {
		bool result = true;
		if (town1.X == town2.X && town1.Y == town2.Y) {
			result = true;
		}
		else {
			result = false;
		}
		return result;
	};
private:
	int X;
	int Y;
};