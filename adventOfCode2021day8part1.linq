<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 26".Dump();
	Simulate(GetSampleList());

	"Real ".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input8.txt"));

}
private void Simulate(List<string> commands)
{
	var dic = GetClock();

	var matches = 0;
	foreach (var command in commands)
	{
		var split = command.Split('|');
		var wires = split[0].Split(' ').ToList();
		wires.Remove("");
		//wires.Dump();
		var outputs = split[1].Split(' ').ToList();
		outputs.Remove("");
		//outputs.Dump();
		matches += SearchWires(outputs, dic).Dump();
	}


	matches.Dump();

	//dic.Dump();


}

private int SearchWires(List<string> wires, Dictionary<int, Digit> dic)
{
	var matches = 0;
	foreach (var seq in wires)
	{
		var matching = dic.Where(w => w.Value.number == seq.Length);

		if (matching.Count() == 1)
		{
			foreach (var match in matching)
			{
				matches++;
				$"testing {seq} {seq.Length} found {match.Value.command}, number {match.Key}".Dump();
			}
		}
	}

	return matches;
}


private Dictionary<int, Digit> GetClock()
{
	Dictionary<int, Digit> dic = new Dictionary<int, Digit>();
	dic.Add(0, new Digit("aaaa,bb,cc,ee,ff,gggg"));
	dic.Add(1, new Digit("cc,ff"));
	dic.Add(2, new Digit("aaaa,cc,dddd,ee,gggg"));
	dic.Add(3, new Digit("aaaa,cc,dddd,ff,gggg"));
	dic.Add(4, new Digit("bb,cc,dddd,ff"));
	dic.Add(5, new Digit("aaaa,bb,dddd,ff,gggg"));
	dic.Add(6, new Digit("aaaa,bb,dddd,ee,ff,gggg"));
	dic.Add(7, new Digit("aaaa,cc,ff"));
	dic.Add(8, new Digit("aaaa,bb,cc,dddd,ee,ff,gggg"));
	dic.Add(9, new Digit("aaaa,bb,cc,dddd,ff,gggg"));
	return dic;
}

class Digit
{
	public int number => pattern.Count;
	public List<string> pattern;
	public string command;


	public Digit(string patterns)
	{
		command = patterns;
		pattern = new List<string>();
		pattern.AddRange(patterns.Split(','));
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