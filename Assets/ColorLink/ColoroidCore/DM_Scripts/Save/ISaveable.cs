/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bitberry
{
	public interface ISaveable
	{
		string SaveId { get; }
		Dictionary<string, object> Save();
	}
}
