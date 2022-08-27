using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dekoto.Actors.Messages;

public sealed class MulticoloredMessage : Message
{
	private MessagePart[] messageParts;
	public override IEnumerable<MessagePart> GetAllText()
		=> messageParts;

	public MulticoloredMessage(float speed, Color?[] colors, string[] text)
	{
		if (colors?.Length == text?.Length && colors is not null && text is not null)
		{
			List<MessagePart> parts = new();
			float timeToShow = 0;
			if (speed > 0)
			{
				timeToShow = 1 / speed;
			}
			else timeToShow = 0f;
			for (int i = 0; i < colors.Length; i++)
			{
				parts.Add(new(text[i], timeToShow, colors[i]));
			}

			messageParts = parts.ToArray();
		}
		else throw new ArgumentException("Arrays size not equal or null");
	}
}
