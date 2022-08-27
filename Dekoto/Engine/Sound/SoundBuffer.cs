using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Silk.NET.OpenAL;

namespace Dekoto.Engine.Sound;

public readonly struct SoundBuffer : IEquatable<SoundBuffer>, IDisposable
{
	private readonly SoundEngine engine;
	public readonly uint Name;
	public SoundBuffer(SoundEngine engine, uint buffer)
	{
		this.engine = engine;
		Name = buffer;
	}
	public bool Equals(SoundBuffer other)
		=> this.Name == other.Name;
	public override bool Equals(object? obj)
		=> obj is SoundBuffer buffer
		&& Equals(buffer);
	public override int GetHashCode()
		=> (int)Name;
	public void Dispose()
		=> engine.AL.DeleteBuffer(Name);
}
