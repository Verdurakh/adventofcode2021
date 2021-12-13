<Query Kind="Program" />

Stack<char> stack;
void Main()
{
	stack = new Stack<char>();
	var inputList = new List<int>();
	"Sample should be 26397".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"C:\Users\Andreas Andersson\Downloads\input10.txt"));

}
private void Simulate(List<string> commands)
{
	var test = commands[2];
	var points = 0;
	foreach (var line in commands)
	{
		points += TestString(line);
	}
	points.Dump();
	//TestString(test);
}

private int TestString(string line)
{
	//line.Dump();
	var ponts = 0;
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
			ponts += GetPointFor(st);
			stack.Pop();
			break;
		}

		if (IsOpen(st))
		{
			stack.Push(st);
		}
	}

	//stack.Dump();

	return ponts;
}

private int GetPointFor(char ch)
{

	switch (ch)
	{
		case ')':
			return 3;
		case ']':
			return 57;
		case '}':
			return 1197;
		case '>':
			return 25137;
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
