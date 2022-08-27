using System;
using System.Drawing;

namespace Dekoto.Actors;

public interface ITalking
{
	public string Name { get; }
	public bool ShowMyName { get; }
	public Color? NameColor { get; }
	public string ID { get; }

	public bool Equals(ITalking other)
		=> other?.ID == this.ID;
}
