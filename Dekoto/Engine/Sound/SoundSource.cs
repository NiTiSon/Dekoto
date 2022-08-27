using System;
using System.Numerics;

namespace Dekoto.Engine.Sound;

public readonly struct SoundSource : IEquatable<SoundSource>, IDisposable
{
	private readonly SoundEngine engine;
	public readonly uint Name;
	public SoundSource(SoundEngine engine, uint name)
	{
		this.engine = engine;
		Name = name;
	}
	public override bool Equals(object? obj)
		=> obj is SoundSource source
		&& Equals(source);
	public bool Equals(SoundSource other)
		=> this.Name == other.Name;
	public override int GetHashCode()
		=> (int)Name;
	public void Dispose()
		=> engine.AL.DeleteSource(Name);
	public bool Loop
	{
		get
		{
			engine.AL.GetSourceProperty(Name, Silk.NET.OpenAL.SourceBoolean.Looping, out bool value);
			return value;
		}
		set => engine.AL.SetSourceProperty(Name, Silk.NET.OpenAL.SourceBoolean.Looping, value);
	}
	public Vector3 Position
	{
		get
		{
			engine.AL.GetSourceProperty(Name, Silk.NET.OpenAL.SourceVector3.Position, out Vector3 value);
			return value;
		}
		set => engine.AL.SetSourceProperty(Name, Silk.NET.OpenAL.SourceVector3.Position, value);
	}
	public float Pitch
	{
		get
		{
			engine.AL.GetSourceProperty(Name, Silk.NET.OpenAL.SourceFloat.Pitch, out float value);
			return value;
		}
		set => engine.AL.SetSourceProperty(Name, Silk.NET.OpenAL.SourceFloat.Pitch, value);
	}
	public float Gain
	{
		get
		{
			engine.AL.GetSourceProperty(Name, Silk.NET.OpenAL.SourceFloat.Gain, out float value);
			return value;
		}
		set => engine.AL.SetSourceProperty(Name, Silk.NET.OpenAL.SourceFloat.Gain, value);
	}
	public void Play(SoundBuffer sound)
	{
		engine.AL.SetSourceProperty(Name, Silk.NET.OpenAL.SourceInteger.Buffer, sound.Name);
		engine.AL.SourcePlay(Name);
	}
	public void Stop()
	{
		engine.AL.SourceStop(Name);
	}
	public static bool operator ==(SoundSource left, SoundSource right) => left.Equals(right);
	public static bool operator !=(SoundSource left, SoundSource right) => !(left == right);
}