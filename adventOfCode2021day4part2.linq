<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 4512(188*24)".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input4.txt"));
}


private void Simulate(List<string> commands)
{
	commands.RemoveAll(w => w == "");
	var numbers = commands.FirstOrDefault();
	numbers.Dump();
	commands.RemoveAt(0);
	var boards = CreateBoards(commands);
	var test = "22,8,21,6,1";

	var numberList = numbers.Split(",");
	var anyWinners = false;
	var currentNumber = -1;
	var winningBoards = new List<Board>();
	foreach (var numb in numberList)
	{
		currentNumber = int.Parse(numb);
		foreach (var board in boards)
		{
			board.MarkNumber(numb);
			if (boards.All(w=>w.winner))
				{
					winningBoards.Add(board);
					//board.Dump();
					anyWinners=true;
					break;
				}
		}

		if (anyWinners)
			break;
	}

	if (winningBoards != null)
	{
		currentNumber.Dump();
		winningBoards.Last().Dump();
		(winningBoards.Last().score * currentNumber).Dump();
	}




	//boards.Dump();
}

private List<Board> CreateBoards(List<string> commands)
{
	var boards = new List<Board>();
	var noBoards = commands.Count / 5;

	for (int i = 0; i < noBoards; i++)
	{
		boards.Add(new Board(commands.Skip(i * 5).Take(5).ToList()));
	}


	return boards;
}


class Board
{
	public bool winner;
	public int[,] board;
	public bool[,] marked;
	public int score;

	public Board(List<string> strings)
	{
		marked = new bool[5, 5];
		board = GetArray(strings);
	}

	public bool IsWinner()
	{
		for (int y = 0; y < 5; y++)
		{
			var winner = true;
			for (int x = 0; x < 5; x++)
			{
				if (!marked[y, x])
				{
					winner = false;
				}
			}
			if (winner)
				return true;
		}


		for (int x = 0; x < 5; x++)
		{
			var winner = true;
			for (int y = 0; y < 5; y++)
			{
				if (!marked[y, x])
				{
					winner = false;
				}
			}
			if (winner)
				return true;
		}

		return false;
	}


	public void MarkNumber(string number)
	{
		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				if (board[y, x] == int.Parse(number))
				{
					marked[y, x] = true;
				}
			}
		}

		winner = IsWinner();
		if (winner)
		{
			CalcScore();
		}
	}
	private void CalcScore()
	{
		score = 0;

		for (int y = 0; y < 5; y++)
		{
			for (int x = 0; x < 5; x++)
			{
				if (!marked[y, x])
				{
					score += board[y, x];
				}
			}
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
	newList.Add("7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1");
	newList.Add("22 13 17 11  0");
	newList.Add(" 8  2 23  4 24");
	newList.Add("21  9 14 16  7");
	newList.Add(" 6 10  3 18  5");
	newList.Add(" 1 12 20 15 19");
	newList.Add("");
	newList.Add(" 3 15  0  2 22");
	newList.Add(" 9 18 13 17  5");
	newList.Add("19  8  7 25 23");
	newList.Add("20 11 10 24  4");
	newList.Add("14 21 16 12  6");
	newList.Add("");
	newList.Add("14 21 17 24  4");
	newList.Add("10 16 15  9 19");
	newList.Add("18  8 23 26 20");
	newList.Add("22 11 13  6  5");
	newList.Add(" 2  0 12  3  7");

	return newList;
}

// Define other methods and classes here
