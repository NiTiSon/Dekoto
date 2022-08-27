using System.Drawing;

namespace Dekoto.Actors;

public readonly struct MessagePart
{
	public readonly string Text;
	public readonly float TimeToShowOneChar;
	public readonly Color? TextColor; 
	public MessagePart(string text, float timeToShowOneChar, Color? color = null)
	{
		Text = text;
		this.TimeToShowOneChar = timeToShowOneChar;
		TextColor = color;
	}
}
