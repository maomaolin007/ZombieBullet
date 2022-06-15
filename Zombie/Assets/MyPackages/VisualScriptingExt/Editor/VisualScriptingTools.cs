// using Fight.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
	public class VisualScriptingTools
	{
		[MenuItem("VisualScripting/RegenerateNodes", false, 1)]
		private static void RegenerateNodes()
		{
			UnitBase.Rebuild();
			Debug.Log("Regenerate Nodes completed");
		}

		public static void UpdateSettings()
		{
			var coreConfig = BoltCore.Configuration;
			coreConfig.GetMetadata(nameof(coreConfig.typeOptions)).Save();

			Codebase.UpdateSettings();
		}

		[MenuItem("VisualScripting/InsertAllTypes", false, 2)]
		private static void InsertAllTypes()
		{
			//var assemblyOptions = new MyBoltCoreConfiguration().assemblyOptions;
			var coreConfig = BoltCore.Configuration;
			var assemblyOptions = coreConfig.GetMetadata(nameof(coreConfig.assemblyOptions)).defaultValue as List<LooseAssemblyName>;
			var names = BoltCore.Configuration.assemblyOptions.Where(a =>
			{
				if (a.name.StartsWith("Unity.VisualScripting"))
				{
					return false;
				}

				var t = assemblyOptions.FirstOrDefault(a2 => a2.name == a.name);
				if (t.name != null)
				{
					return false;
				}
				return true;
			}).ToArray();
			var alls = AppDomain.CurrentDomain.GetAssemblies();
			var asses = names.Select(name =>
			{
				var ass = alls.FirstOrDefault(a => a.GetName().Name == name);
				return ass;
			}).Where(ass => ass != null).ToArray();
			foreach (var assembly in asses)
			{
				foreach (var def in assembly.ExportedTypes)
				{
					var t = def;
					if (t != null && !BoltCore.Configuration.typeOptions.Contains(t))
					{
						BoltCore.Configuration.typeOptions.Add(t);
					}
				}
			}

			UpdateSettings();
		}

		// [MenuItem("VisualScripting/InsertFightTypes", false, 3)]
		// private static void InsertFightTypes()
		// {
		// 	var assembly = Assembly.GetAssembly(typeof(UBattleManager));
		// 	foreach (var def in assembly.ExportedTypes)
		// 	{
		// 		var t = def;
		// 		if (t != null && !BoltCore.Configuration.typeOptions.Contains(t))
		// 		{
		// 			BoltCore.Configuration.typeOptions.Add(t);
		// 		}
		// 	}

		// 	UpdateSettings();
		// }

	}
}
