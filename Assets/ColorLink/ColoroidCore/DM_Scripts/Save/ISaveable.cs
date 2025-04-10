/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dotmob
{
	public interface ISaveable
	{
		string SaveId { get; }
		Dictionary<string, object> Save();
	}
}
