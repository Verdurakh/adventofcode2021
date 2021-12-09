<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 1134".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input9.txt"));

}
private void Simulate(List<string> commands)
{
	var arr = GetArray(commands);
	//arr.Dump();

	//CheckPoint(arr[6, 1], arr, arr[6, 1].value).Dump();

	Dictionary<string, Point> points = new Dictionary<string, UserQuery.Point>();

	for (int x = 0; x < arr.GetLength(0); x++)
	{
		for (int y = 0; y < arr.GetLength(1); y++)
		{
			var point = CheckPoint(arr[x, y], arr, arr[x, y].value);
			if (!points.ContainsKey($"{point.x},{point.y}"))
				points.Add($"{point.x},{point.y}", point);
		}
	}
	//points.Sum(w => w.Value.value + 1).Dump();
	//points.Dump();

	var res = new List<int>();
	//points.Dump();
	foreach (var po in points)
	{
		res.Add(CheckBasin(po.Value, arr, po.Value.value));
	}

	var tt = 1;
	foreach (var num in res.OrderByDescending(w => w).Take(3))
	{
		tt *= num;
	}
	tt.Dump();
	//res.Dump();

}

private int CheckBasin(Point point, Point[,] arr, int targetValue)
{
	var counter = 1;

	if (point.x + 1 < arr.GetLength(0))
	{
		var right = arr[point.x + 1, point.y];
		if (right.value >= point.value + 1 && right.value != 9)
		{
			counter += CheckBasin(right, arr, point.value);
			if (right.counted)
				counter--;
			right.counted = true;
		}

	}

	if (point.x - 1 >= 0)
	{
		var left = arr[point.x - 1, point.y];
		if (left.value >= point.value + 1 && left.value != 9)
		{
			counter += CheckBasin(left, arr, point.value);
			if (left.counted)
				counter--;
			left.counted = true;
		}

	}

	if (point.y + 1 < arr.GetLength(1))
	{
		var down = arr[point.x, point.y + 1];
		if (down.value >= point.value + 1 && down.value != 9)
		{
			counter += CheckBasin(down, arr, point.value);
			if (down.counted)
				counter--;
			down.counted = true;
		}

	}

	if (point.y - 1 >= 0)
	{
		var up = arr[point.x, point.y - 1];
		if (up.value >= point.value + 1 && up.value != 9)
		{
			
			counter += CheckBasin(up, arr, point.value);
			if (up.counted)
				counter--;
			up.counted = true;
		}

	}




	return counter;
}

private Point CheckPoint(Point point, Point[,] arr, int targetValue)
{
	if (point.isLowest)
	{
		return point;
	}
	if (point.lowest != null)
	{
		return point.lowest;
	}

	Point checkRight = null;
	Point checkLeft = null;
	Point checkDown = null;
	Point checkUp = null;



	if (point.x + 1 < arr.GetLength(0))
	{
		var right = arr[point.x + 1, point.y];
		if (right.isLowest)
			checkRight = right;
		else if (right.lowest != null)
			checkRight = right.lowest;
		else if (right.value < point.value)
			checkRight = CheckPoint(right, arr, point.value);
	}

	if (point.x - 1 >= 0)
	{
		var left = arr[point.x - 1, point.y];
		if (left.isLowest)
			checkLeft = left;
		else if (left.lowest != null)
			checkLeft = left.lowest;
		else if (left.value < point.value)
			checkLeft = CheckPoint(left, arr, point.value);
	}

	if (point.y + 1 < arr.GetLength(1))
	{
		var down = arr[point.x, point.y + 1];
		if (down.isLowest)
			checkDown = down;
		else if (down.lowest != null)
			checkDown = down.lowest;
		else if (down.value < point.value)
			checkDown = CheckPoint(down, arr, point.value);
	}

	if (point.y - 1 >= 0)
	{
		var up = arr[point.x, point.y - 1];
		if (up.isLowest)
			checkUp = up;
		else if (up.lowest != null)
			checkUp = up.lowest;
		else if (up.value < point.value)
			checkUp = CheckPoint(up, arr, point.value);
	}


	Point lowest = point;
	if (checkRight != null && lowest.value > checkRight.value)
	{
		lowest = checkRight;
	}
	if (checkLeft != null && lowest.value > checkLeft.value)
	{
		lowest = checkLeft;
	}
	if (checkUp != null && lowest.value > checkUp.value)
	{
		lowest = checkUp;
	}
	if (checkDown != null && lowest.value > checkDown.value)
	{
		lowest = checkDown;
	}

	lowest.isLowest = true;
	point.lowest = lowest;


	return lowest;
}

class Point
{
	public int value;
	public bool isLowest;
	public Point lowest;
	public bool counted;
	public int x;
	public int y;
	public Point(int val, int x, int y)
	{
		this.x = x;
		this.y = y;
		value = val;
	}
}

private Point[,] GetArray(List<string> commands)
{
	int width = commands.FirstOrDefault().Length;
	int height = commands.Count;
	var array = new Point[width, height];

	int x = 0;
	int y = 0;
	foreach (var command in commands)
	{
		var str = command.ToCharArray();
		foreach (var by in str)
		{
			if (by.ToString() == "")
				continue;
			array[x, y] = new Point(int.Parse(by.ToString()), x, y);
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
	//newList.Add("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");


	newList.Add("2199943210");
	newList.Add("3987894921");
	newList.Add("9856789892");
	newList.Add("8767896789");
	newList.Add("9899965678");

	return newList;
}

// Define other methods and classes here