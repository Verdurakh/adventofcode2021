<Query Kind="Program" />

Stack<char> stack;
void Main()
{
	stack = new Stack<char>();
	var inputList = new List<int>();
	"Sample should be 288957".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"C:\Users\Andreas Andersson\Downloads\input10.txt"));

}
private void Simulate(List<string> commands)
{
	var test = commands[2];
	var points = new List<long>();
	foreach (var line in commands)
	{
		var pp = TestString(line);
		if (pp > 0)
			points.Add(pp);
	}
	points=points.OrderBy(w => w).ToList();
	points[points.Count/2].Dump();
	//TestString(test);
}

private long TestString(string line)
{
	stack.Clear();
	//line.Dump();
	long ponts = 0;
	foreach (var st in line)
	{
		if (IsEnd(st) && st == GetEndingFor(stack.Peek()))
		{
			//$"{stack.Peek()}{st}".Dump();

			stack.Pop();

		}
		else if (IsEnd(st))
		{
			//$"found wrong ending expected {GetEndingFor(stack.Peek())} but found {st}".Dump();
			//ponts += GetPointFor(st);
			stack.Pop();
			return 0;
		}

		if (IsOpen(st))
		{
			stack.Push(st);
		}
	}

	//stack.Dump();
	foreach (var open in stack)
	{
		ponts *= 5;
		ponts += GetPointFor(GetEndingFor(open));
	}

	return ponts;
}

private int GetPointFor(char ch)
{

	switch (ch)
	{
		case ')':
			return 1;
		case ']':
			return 2;
		case '}':
			return 3;
		case '>':
			return 4;
	}


	throw new ArgumentException("Invalid open " + ch);
}

private bool IsEnd(char end)
{
	switch (end)
	{
		case ')':
		case '}':
		case ']':
		case '>':
			return true;
	}

	return false;
}

private bool IsOpen(char open)
{
	switch (open)
	{
		case '(':
		case '{':
		case '[':
		case '<':
			return true;
	}

	return false;
}

private char GetEndingFor(char open)
{
	switch (open)
	{
		case '(':
			return ')';
		case '{':
			return '}';
		case '[':
			return ']';
		case '<':
			return '>';
	}

	throw new ArgumentException("Invalid open " + open);
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

	newList.Add("[({(<(())[]>[[{[]{<()<>>");
	newList.Add("[(()[<>])]({[<{<<[]>>(");
	newList.Add("{([(<{}[<>[]}>{[]{[(<()>");
	newList.Add("(((({<>}<{<{<>}{[]{[]{}");
	newList.Add("[[<[([]))<([[{}[[()]]]");
	newList.Add("[{[{({}]{}}([{[{{{}}([]");
	newList.Add("{<[[]]>}<{[{[{[]{()[[[]");
	newList.Add("[<(<(<(<{}))><([]([]()");
	newList.Add("<{([([[(<>()){}]>(<<{{");
	newList.Add("<{([{{}}[<[[[<>{}]]]>[]]");

	return newList;
}
