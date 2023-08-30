﻿using System;
using System.Xml;
using System.Xml.Linq;

namespace Pandora.Patch.Patchers.Skyrim.Hkx
{
	public class RemoveElementChange : IPackFileChange
	{
		public IPackFileChange.ChangeType Type { get; } = IPackFileChange.ChangeType.Insert;

		public XmlNodeType AssociatedType { get; } = XmlNodeType.Element;

		public string Path { get; private set; }

		private XElement? element { get; set; }

		public RemoveElementChange(string path)
		{
			Path = path;

		}
		public bool Apply(PackFile packFile)
		{
			if (!packFile.Map.PathExists(Path)) return false;
			element = PackFileEditor.RemoveElement(packFile, Path);
			return !packFile.Map.PathExists(Path);

		}

		public bool Revert(PackFile packFile)
		{
			if (element == null) return false;
			string newPath = PackFileEditor.InsertElement(packFile, Path, element);
			Path = String.IsNullOrEmpty(newPath) ? Path : newPath;
			return packFile.Map.PathExists(Path);
		}
	}





}
