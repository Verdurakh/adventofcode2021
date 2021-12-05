<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 5".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input5.txt"));
}


private void Simulate(List<string> commands)
{
	var size = 1000;
	if (commands.Count < 11)
		size = 10;
	var array = new int[size, size];
	foreach (var com in commands)
	{
		//com.Dump();
		var command = ParseCommand(com);
		var points = GetPointsOnLine(command.from.x, command.from.y, command.to.x, command.to.y);
		//points.Dump();

		foreach (var point in points)
		{
			array[point.y, point.x] = array[point.y, point.x] + 1;
		}
	}

	//array.Dump();
	CountPointsToAvoid(array,size).Dump();

}

private int CountPointsToAvoid(int[,] array, int size){
	int counter=0;
	
	for (int y = 0; y < size; y++)
	{
		for (int x = 0; x < size; x++)
		{
			if(array[y,x]>1)
			counter++;
		}
	}
	
	return counter;
}

private ((int x, int y) from, (int x, int y) to) ParseCommand(string command)
{
	var comm = command.Replace("->", ",").Replace(" ", "");
	var split = comm.Split(",");
	return ((int.Parse(split[0]), int.Parse(split[1])), (int.Parse(split[2]), int.Parse(split[3])));
}

private IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
{
	if (x0 == x1 || y0 == y1)
	{
		bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
		if (steep)
		{
			int t;
			t = x0; // swap x0 and y0
			x0 = y0;
			y0 = t;
			t = x1; // swap x1 and y1
			x1 = y1;
			y1 = t;
		}
		if (x0 > x1)
		{
			int t;
			t = x0; // swap x0 and x1
			x0 = x1;
			x1 = t;
			t = y0; // swap y0 and y1
			y0 = y1;
			y1 = t;
		}
		int dx = x1 - x0;
		int dy = Math.Abs(y1 - y0);
		int error = dx / 2;
		int ystep = (y0 < y1) ? 1 : -1;
		int y = y0;
		for (int x = x0; x <= x1; x++)
		{
			yield return new Point((steep ? y : x), (steep ? x : y));
			error = error - dy;
			if (error < 0)
			{
				y += ystep;
				error += dx;
			}
		}
		yield break;
	}
	else
	{
		yield break;
	}

}

class Point
{
	public int x;
	public int y;
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

private int[,] GetArray(List<string> commands)
{
	int width = commands.Count;
	int height = commands.Count;
	var array = new int[height, width];

	int x = 0;
	int y = 0;
	foreach (var command in commands)
	{
		var str = command.Split(" ");
		foreach (var by in str)
		{
			if (by == "")
				continue;
			array[y, x] = int.Parse(by);
			x++;
		}
		x = 0;
		y++;
	}

	return array;
}

private List<string> ReadMyFile(string uri)
{
	var newList = new List<string>();
	string line;
	System.IO.StreamReader reader = new StreamReader(uri);
	while ((line = reader.ReadLine()) != null)
	{
		newList.Add(line);

	}

	return newList;
}



private List<string> GetSampleList()
{
	var newList = new List<string>();
	newList.Add("0,9 -> 5,9");
	newList.Add("8,0 -> 0,8");
	newList.Add("9,4 -> 3,4");
	newList.Add("2,2 -> 2,1");
	newList.Add("7,0 -> 7,4");
	newList.Add("6,4 -> 2,0");
	newList.Add("0,9 -> 2,9");
	newList.Add("3,4 -> 1,4");
	newList.Add("0,0 -> 8,8");
	newList.Add("5,5 -> 8,2");

	return newList;
}

// Define other methods and classes here
