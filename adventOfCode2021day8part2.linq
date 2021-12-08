<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 61229".Dump();
	Simulate(GetSampleList());

	"Real ".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input8.txt"));

}
private void Simulate(List<string> commands)
{
	var result = 0;
	var clock = GetClock();
	foreach (var command in commands)
	{
		var split = command.Split('|');
		var wires = split[0].Split(' ').ToList();
		wires.Remove("");
		var outputs = split[1].Split(' ').ToList();
		outputs.Remove("");
		result += SearchWires(wires, clock, outputs);
	}

	result.Dump();



}


private int SearchWires(List<string> wires, Dictionary<int, Digit> clock, List<string> outputs)
{
	List<(int number, string pattern, bool safe)> foundPatterns = new List<(int, string, bool)>();
	foreach (var seq in wires)
	{
		var matching = clock.Where(w => w.Value.number == seq.Length);


		foreach (var match in matching)
		{
			if (!foundPatterns.Contains((match.Key, seq, matching.Count() == 1)))
				foundPatterns.Add((match.Key, seq, matching.Count() == 1));

		}

	}
	UpdateClock(clock, foundPatterns.Where(w => w.safe).ToList());
	var correct = FindCorrectPositions(foundPatterns, clock);
	var res = "";
	
	foreach (var outs in outputs)
	{
		var sorted =String.Concat(outs.OrderBy(c => c));
		var number = clock.FirstOrDefault(w => w.Value.command == sorted).Value.position;
		res += number.ToString();
	}
	return int.Parse(res);
}

private string[] FindCorrectPositions(List<(int number, string pattern, bool safe)> foundPatterns, Dictionary<int, Digit> clock)
{
	string[] positions = new string[7];
	for (int i = 0; i < 7; i++)
	{
		positions[i] = "_";
	}
	var one = foundPatterns.FirstOrDefault(w => w.number == 1);
	var six = foundPatterns.Where(w => w.number == 6 && (!w.pattern.Contains(one.pattern[0]) || !w.pattern.Contains(one.pattern[1]))).FirstOrDefault();
	UpdateClock(clock, six);
	if (six.pattern.Contains(one.pattern[0]))
	{
		positions[5] = one.pattern[0].ToString();
		positions[2] = one.pattern[1].ToString();
	}
	else
	{
		positions[5] = one.pattern[1].ToString();
		positions[2] = one.pattern[0].ToString();
	}

	var seven = foundPatterns.FirstOrDefault(w => w.number == 7);
	foreach (var el in positions)
	{
		seven.pattern = seven.pattern.Replace(el, "");
	}
	positions[0] = seven.pattern;

	var four = foundPatterns.FirstOrDefault(w => w.number == 4);
	foreach (var el in positions)
	{
		four.pattern = four.pattern.Replace(el, "");
	}
	var zero = foundPatterns.Where(w => w.number == 0 && (!w.pattern.Contains(four.pattern[0]) || !w.pattern.Contains(four.pattern[1]))).FirstOrDefault();
	UpdateClock(clock, zero);
	if (zero.pattern.Contains(four.pattern[0]))
	{
		positions[1] = four.pattern[0].ToString();
		positions[3] = four.pattern[1].ToString();
	}
	else
	{
		positions[1] = four.pattern[1].ToString();
		positions[3] = four.pattern[0].ToString();
	}

	var three = foundPatterns.Where(w => w.number == 3);
	var two = foundPatterns.Where(w => w.number == 2);
	var realThree = three.Where(w => w.pattern.Contains(positions[0]) && !w.pattern.Contains(positions[1]) && w.pattern.Contains(positions[2]) && w.pattern.Contains(positions[3]) && w.pattern.Contains(positions[5])).FirstOrDefault();
	UpdateClock(clock, realThree);
	foreach (var element in positions)
	{
		realThree.pattern = realThree.pattern.Replace(element, "");
	}
	positions[6] = realThree.pattern;
	var str = "abcdefg";
	foreach (var element in positions)
	{
		str = str.Replace(element, "");
	}
	positions[4] = str;
	var realTwo = foundPatterns.Where(w => w.number == 2 && w.pattern.Contains(positions[0]) && !w.pattern.Contains(positions[1]) && w.pattern.Contains(positions[2]) && w.pattern.Contains(positions[3]) && w.pattern.Contains(positions[4]) && !w.pattern.Contains(positions[5]) && w.pattern.Contains(positions[6])).FirstOrDefault();
	UpdateClock(clock, realTwo);
	var realFive = foundPatterns.Where(w => w.number == 5 && w.pattern.Contains(positions[0]) && w.pattern.Contains(positions[1]) && !w.pattern.Contains(positions[2]) && w.pattern.Contains(positions[3]) && !w.pattern.Contains(positions[4]) && w.pattern.Contains(positions[5]) && w.pattern.Contains(positions[6])).FirstOrDefault();
	UpdateClock(clock, realFive);
	var realNine = foundPatterns.Where(w => w.number == 9 && w.pattern.Contains(positions[0]) && w.pattern.Contains(positions[1]) && w.pattern.Contains(positions[2]) && w.pattern.Contains(positions[3]) && !w.pattern.Contains(positions[4]) && w.pattern.Contains(positions[5]) && w.pattern.Contains(positions[6])).FirstOrDefault();
	UpdateClock(clock, realNine);
	return positions;
}

private void UpdateClock(Dictionary<int, Digit> clock, List<(int, string, bool)> newValues)
{
	foreach (var val in newValues)
	{
		UpdateClock(clock, (val.Item1, val.Item2, val.Item3));
	}
}
private void UpdateClock(Dictionary<int, Digit> clock, (int, string, bool) newValues)
{

	clock[newValues.Item1].Update(newValues.Item2,newValues.Item1);

}


private Dictionary<int, Digit> GetClock()
{
	Dictionary<int, Digit> dic = new Dictionary<int, Digit>();
	dic.Add(0, new Digit("aaaa,bb,cc,ee,ff,gggg",0));
	dic.Add(1, new Digit("cc,ff",1));
	dic.Add(2, new Digit("aaaa,cc,dddd,ee,gggg",2));
	dic.Add(3, new Digit("aaaa,cc,dddd,ff,gggg",3));
	dic.Add(4, new Digit("bb,cc,dddd,ff",4));
	dic.Add(5, new Digit("aaaa,bb,dddd,ff,gggg",5));
	dic.Add(6, new Digit("aaaa,bb,dddd,ee,ff,gggg",6));
	dic.Add(7, new Digit("aaaa,cc,ff",7));
	dic.Add(8, new Digit("aaaa,bb,cc,dddd,ee,ff,gggg",8));
	dic.Add(9, new Digit("aaaa,bb,cc,dddd,ff,gggg",9));
	return dic;
}



class Digit
{
	public int position;
	public int number => pattern.Count;
	public List<string> pattern;
	public string command;
	public bool safe;


	public Digit(string patterns, int position)
	{
		this.position=position;
		command = patterns.Replace(",", "");
		pattern = new List<string>();
		pattern.AddRange(patterns.Split(','));
	}

	public void Update(string patterns, int position)
	{
		this.position=position;
		pattern = new List<string>();
		pattern.AddRange(patterns.ToCharArray().Select(c => c.ToString()).ToList());
		command = String.Concat(string.Join("", pattern).OrderBy(c => c));
		safe = true;
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
	//newList.Add("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

	
	newList.Add("be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe");
	newList.Add("edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc");
	newList.Add("fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg");
	newList.Add("fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb");
	newList.Add("aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea");
	newList.Add("fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb");
	newList.Add("dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe");
	newList.Add("bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef");
	newList.Add("egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb");
	newList.Add("gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce");

	return newList;
}

// Define other methods and classes here