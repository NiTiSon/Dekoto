using Silk.NET.OpenAL;
using System;
using System.Buffers.Binary;
using System.Text;

namespace Dekoto.Engine.Sound;

public unsafe class SoundEngine
{
	public readonly AL AL;
	public readonly ALContext ALC;
	public readonly Device* pDevice;
	public readonly Context* pContext;
	public SoundEngine(AL al, ALContext context)
	{
		AL = al;
		ALC = context;
		pDevice = context.OpenDevice("");
		if (pDevice is null) // && !GlobalOptions.DisableSound
		{
			throw new Exception("Could not create device");
		}

		pContext = ALC.CreateContext(pDevice, null);
		ALC.MakeContextCurrent(pContext);

		AudioError err = AL.GetError();

		if (err is not AudioError.NoError)
			throw new Exception(err.ToString());
	}
	public SoundBuffer Wav(ReadOnlySpan<byte> file)
	{
		uint buff = AL.GenBuffer();

		int index = 0;

		if (file[index++] != 'R' || file[index++] != 'I' || file[index++] != 'F' || file[index++] != 'F')
			throw new Exception("Isnt riff format");

		int chunkSize = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(index, 4));
		index += 4;

		if (file[index++] != 'W' || file[index++] != 'A' || file[index++] != 'V' || file[index++] != 'E')
			throw new Exception("Isnt wav format");

		short numChannels = -1;
		int sampleRate = -1;
		int byteRate = -1;
		short blockAlign = -1;
		short bitsPerSample = -1;
		BufferFormat format = 0;

		while (index + 4 < file.Length)
		{
			string identifier = "" + (char)file[index++] + (char)file[index++] + (char)file[index++] + (char)file[index++];
			int size = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(index, 4));
			index += 4;
			if (identifier == "fmt ")
			{
				if (size != 16)
				{
					Console.WriteLine($"Unknown Audio Format with subchunk1 size {size}");
				}
				else
				{
					short audioFormat = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(index, 2));
					index += 2;
					if (audioFormat != 1)
					{
						Console.WriteLine($"Unknown Audio Format with ID {audioFormat}");
					}
					else
					{
						numChannels = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(index, 2));
						index += 2;
						sampleRate = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(index, 4));
						index += 4;
						byteRate = BinaryPrimitives.ReadInt32LittleEndian(file.Slice(index, 4));
						index += 4;
						blockAlign = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(index, 2));
						index += 2;
						bitsPerSample = BinaryPrimitives.ReadInt16LittleEndian(file.Slice(index, 2));
						index += 2;

						if (numChannels == 1)
						{
							if (bitsPerSample == 8)
								format = BufferFormat.Mono8;
							else if (bitsPerSample == 16)
								format = BufferFormat.Mono16;
							else
							{
								Console.WriteLine($"Can't Play mono {bitsPerSample} sound.");
							}
						}
						else if (numChannels == 2)
						{
							if (bitsPerSample == 8)
								format = BufferFormat.Stereo8;
							else if (bitsPerSample == 16)
								format = BufferFormat.Stereo16;
							else
							{
								Console.WriteLine($"Can't Play stereo {bitsPerSample} sound.");
							}
						}
						else
						{
							Console.WriteLine($"Can't play audio with {numChannels} sound");
						}
					}
				}
			}
			else if (identifier == "data")
			{
				ReadOnlySpan<byte> data = file.Slice(44, size);
				index += size;

				fixed (byte* pData = data)
					AL.BufferData(buff, format, pData, size, sampleRate);
			}
			else if (identifier == "iXML")
			{
				ReadOnlySpan<byte> v = file.Slice(index, size);
				string str = Encoding.ASCII.GetString(v);
				index += size;
			}
			else
			{
				index += size;
			}
		}

		return new(this, buff);
	}
	public SoundSource CreateSource()
		=> new(this, AL.GenSource());
}
