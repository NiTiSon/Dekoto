using Dekoto.Actors;
using Dekoto.Engine;
using System.Drawing;

namespace Test_1;

internal class Program
{
	static void Main(string[] args)
	{
		Actor user = new("user", "User")
		{
			NameColor = Color.Magenta,
		};
		Actor glossy = new("glossy", "Glosyy")
		{
			NameColor = Color.FromArgb(167, 190, 54),
		};
		Writer writer = new();
		Scene scene = new(writer, user);

		//TODO: impl
		Scenario scenario = new();

		writer.Write(user, new SimpleMessage("Hello world", 20f));
		writer.Write(glossy, new SimpleMessage("How about sex?", 20f));
	}
}
