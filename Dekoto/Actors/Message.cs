﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dekoto.Actors;

public abstract class Message
{
	public abstract IEnumerable<MessagePart> GetAllText();
}