using Dekoto.Actors;
using Dekoto.Actors.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dekoto.Engine;
public class Writer
{
	private const int MilisecondInOneSecond = 1_000;
	public readonly WriterOptions Options;
	public Writer(WriterOptions? options = default)
	{
		Options = options ?? WriterOptions.Default;
	}
	private int FromSecondsToMiliseconds(float seconds)
		=> (int)(seconds * MilisecondInOneSecond);

	private ITalking lastGuy;
	private int lastGuyOffset;
	public void Write(ITalking sender, Message msg)
	{
		if (sender.Equals(lastGuy))
		{
			CC.Write(String.Concat(Enumerable.Repeat(" ", lastGuyOffset)));
			CC.Write("| ");

			goto SKIP_USER;
		}

		if (sender.ShowMyName)
		{
			CC.Write("[");
#if DEBUG
			CC.Write(sender.ID + ":");
#endif
			if(sender.NameColor is not null)
				CC.ForegroundColor = sender.NameColor.Value;

			CC.Write(sender.Name);

			CC.ResetColor();
			CC.Write("] ");

			lastGuy = sender;
			lastGuyOffset
				=
				#if DEBUG
				sender.ID.Length + 1 + 
#endif
				sender.Name.Length + 1
				;
		}
		SKIP_USER:
		//Color? lastColor = null;
		foreach (MessagePart part in msg.GetAllText())
		{
			string text = part.Text;
			float showTime = part.TimeToShowOneChar;
			Color? textColor = part.TextColor;

			foreach (char c in text)
			{
				if (showTime != 0)
					Task.Delay(FromSecondsToMiliseconds(showTime * Options.TextSpeedMultiplier)).Wait();

				if (textColor.HasValue)
					CC.Write(c, textColor.Value);
				else
					CC.Write(c);
			}
		}
		CC.WriteLine();
	}
}
