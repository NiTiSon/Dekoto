using Dekoto.Engine;
using NiTiS.Collections.Generic;

namespace Dekoto.Actors;
public class Scene
{
	private readonly Writer writer;
	public ITalking user;
	public readonly SmartDictonary<string, ITalking> Actors;
	public Scene(Writer writer, ITalking user)
	{
		this.writer = writer;
		this.user = user;
		Actors = new(k => k.ID, 16);
	}
}
