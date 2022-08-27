namespace Dekoto.Engine;

public record WriterOptions(float TextSpeedMultiplier)
{
	public static readonly WriterOptions Default = new(1f);
}
