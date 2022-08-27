using System.Drawing;

namespace Dekoto.Actors;

public class Actor : ITalking
{
	private readonly string id;
	public string ID => id;
	public Actor(string id, string name, bool showMyName = true, Color? nameColor = null)
	{
		this.id = id;
		Name = name;
		ShowMyName = showMyName;
		NameColor = nameColor;
	}

	public string Name { get; set; } = string.Empty;
	public bool ShowMyName { get; set; }
	public Color? NameColor { get; set; }
}
