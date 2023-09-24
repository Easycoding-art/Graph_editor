import random
name = input()

transport = ["car", "plane", "train", "ship"]
points = [{"x" : str(random.randint(0, 500)), "y" : str(random.randint(100, 500)), "name":"Town_"+str(i)} for i in range(0, 20)]
lines = []
for j in range(0, 30) :
    p1 = random.randint(0, 19)
    p2 = random.randint(0, 19)
    lines.append({"x_a" : points[p1].get("x"), "y_a" : points[p1].get("y"), 
    "x_b" : points[p2].get("x"), 
    "y_b" : points[p2].get("y"), 
    "length":str(random.randint(1, 100)),
    "cost":str(random.randint(1, 100)),
    "transport":str(transport[random.randint(0, len(transport) - 1)])})
file = open(name+".txt", "w")
for i in range(0, 20) :
    file.write(points[i].get("x")+" " + points[i].get("y") + " 60 " + points[i].get("name") + " 1" + " #\n")

for j in range(0, 30) :
    file.write("/ " + lines[j].get("x_a")+" " + lines[j].get("y_a")+ " " + lines[j].get("x_b")+ " " + lines[j].get("y_b") + " " + lines[j].get("length")+ " " + lines[j].get("cost")+ " " + lines[j].get("transport") + " #\n")
file.close()