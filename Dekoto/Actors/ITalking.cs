using System.Drawing;

namespace Dekoto.Actors;

public interface ITalking
{
	public string Name { get; }
	public bool ShowMyName { get; }
	public Color? NameColor { get; }
	public string ID { get; }
}
