using Dekoto.Engine.Sound;
using Silk.NET.OpenAL;

namespace Test_2;

internal class Program
{
	static void Main(string[] args)
	{
		SoundEngine engine = new(AL.GetApi(), ALContext.GetApi());

		SoundSource source = engine.CreateSource();

		SoundBuffer w = engine.Wav(File.ReadAllBytes("a.wav"));

		

		Console.ReadLine();
	}
}
