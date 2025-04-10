/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bitberry.ColorLink
{
	#region Enums

	public enum PackUnlockType
	{
		None,
		Stars,
		IAP
	}

	#endregion

	[System.Serializable]
	public class PackInfo
	{
		#region Inspector Variables

		public string			packId;
		public string			packName;
		public string			packDescription;
		public PackUnlockType	unlockType;
		public int				unlockStarsAmount;
		public string			unlockIAPProductId;
		public List<TextAsset>	levelFiles;
		public int time = 60;

		public Sprite imgBgPack, imgLightPack;

		#endregion

		#region Member Variables

		private List<LevelData> levelDatas;

		#endregion

		#region Properties

		public bool HasLevelDats { get { return levelDatas != null; } }

		public List<LevelData> LevelDatas
		{
			get
			{
				if (levelDatas == null)
				{
					CreateLevelDatas();
				}

				return levelDatas;
			}
		}

		#endregion

		#region Private Methods

		private void CreateLevelDatas()
		{
			levelDatas = new List<LevelData>();

			for (int i = 0; i < levelFiles.Count; i++)
			{
				levelDatas.Add(new LevelData(levelFiles[i], packId, i));
			}
		}

		#endregion
	}
}
