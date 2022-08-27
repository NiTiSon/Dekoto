using Dekoto.Engine;
using System.Collections.Generic;
using System.Drawing;

namespace Dekoto.Actors.Messages;

public sealed class SimpleMessage : Message
{
    private readonly string text;
    private readonly float timeToShowOneChar;
    private readonly Color? color;
    public override IEnumerable<MessagePart> GetAllText()
        => new Single<MessagePart>(new(text, timeToShowOneChar, color));
    public SimpleMessage(string text, float speed = 0)
    {
        this.text = text;

        if (speed > 0)
        {
            timeToShowOneChar = 1 / speed;
        }
        else timeToShowOneChar = 0f;
    }
    public SimpleMessage(string text, Color textColor, float speed = 0) : this(text, speed)
    {
        color = textColor;
    }
}
