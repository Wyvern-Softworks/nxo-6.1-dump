using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;

namespace NXO.Mods.Categories;

public static class CustomNextbots
{
	public class Entry
	{
		public string Name;

		public string Image;

		public string Sound;

		public string Jumpscare;

		public bool HasCustomName;
	}

	[CompilerGenerated]
	private sealed class CapturedVariables100
	{
		public Entry e;

		public Func<Entry, bool> CachedDelegate1;

		internal bool Load_Lambda1(Entry x)
		{
			return x.Name == e.Name;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables130
	{
		public Entry entry;

		internal void BuildButtons_Lambda3()
		{
			SelectCustomNextbot(entry.Name);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables140
	{
		public string name;

		internal void GetActionButtons_Lambda0()
		{
			SpawnCustomNextbot(name);
		}

		internal void GetActionButtons_Lambda1()
		{
			RenameCustomNextbot(name);
		}

		internal void GetActionButtons_Lambda2()
		{
			DeleteCustomNextbot(name);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables160
	{
		public string name;

		internal bool Spawn_Lambda0(Entry x)
		{
			return x.Name == name;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables170
	{
		public string name;

		internal bool Delete_Lambda0(Entry x)
		{
			return x.Name == name;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables180
	{
		public string name;

		public Entry e;

		internal bool BeginRename_Lambda0(Entry x)
		{
			return x.Name == name;
		}

		internal void BeginRename_Lambda1(string newName)
		{
			CapturedVariables181 LocalScope8 = new CapturedVariables181();
			LocalScope8.newName = newName;
			LocalScope8.newName = LocalScope8.newName.Trim();
			if (!string.IsNullOrEmpty(LocalScope8.newName) && !(LocalScope8.newName == name))
			{
				if (CapturedVariables181_Items_01.Any((Entry x) => x.Name == LocalScope8.newName))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Name Already Exists");
					return;
				}
				e.Name = LocalScope8.newName;
				e.HasCustomName = true;
				SaveCustomNextbots();
				CapturedVariables181_Text_01 = null;
				RebuildCustomNextbotButtons();
				Main.RebuildMenu();
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed → " + LocalScope8.newName);
			}
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables181
	{
		public string newName;

		internal bool BeginRename_Lambda2(Entry x)
		{
			return x.Name == newName;
		}
	}

	public static readonly List<Entry> CapturedVariables181_Items_01 = new List<Entry>();

	public static string CapturedVariables181_Text_01;

	private const string SeedText = "# Add one nextbot per line using this format:\r\n#\r\n# imageurl : soundurl\r\n#\r\n# There must be a space, a colon, then a space ( : ) between the two links.\r\n# Optionally give it a name:   name : imageurl : soundurl\r\n# Optionally add a jumpscare sound at the end (played when it catches you):\r\n#     imageurl : soundurl : jumpscareurl\r\n#\r\n# After editing, go in-game to Nextbots > Custom Nextbots > Reload Custom Nextbots.\r\n";

	private const string InstructionsText = "=== HOW TO ADD CUSTOM CapturedVariables181_Reference_01 ===\r\n\r\n1. Open Nextbots.txt in this folder.\r\n\r\n2. Add one nextbot per line in this format:\r\n\r\n       imageurl : soundurl\r\n\r\n   - The image is what the nextbot looks like (a direct link to a .png / .jpg).\r\n   - The sound is the audio it plays / jumpscares with (a direct link to a .mp3 / .wav / .ogg).\r\n   - IMPORTANT: there must be a SPACE, a COLON, then a SPACE ( : ) between the two links.\r\n\r\n   Example:\r\n       https://site.com/troll.png : https://site.com/scream.mp3\r\n\r\n3. (Optional) Give it a custom name by adding one more field in front:\r\n\r\n       name : imageurl : soundurl\r\n\r\n   Example:\r\n       Troll : https://site.com/troll.png : https://site.com/scream.mp3\r\n\r\n4. (Optional) Give it a separate jumpscare sound by adding one more link at the end:\r\n\r\n       imageurl : soundurl : jumpscareurl\r\n       name : imageurl : soundurl : jumpscareurl\r\n\r\n   - The jumpscare sound plays when the nextbot catches you.\r\n   - Leave it off and it just uses its regular sound for the jumpscare instead.\r\n\r\n   Example:\r\n       Troll : https://site.com/troll.png : https://site.com/chase.mp3 : https://site.com/scream.mp3\r\n\r\n5. In-game go to: Nextbots > Custom Nextbots > Reload Custom Nextbots.\r\n\r\n6. Click a custom nextbot to Spawn, Rename, or Delete it.\r\n\r\nTIPS:\r\n- Use DIRECT image/audio links (the link should end in .png, .mp3, etc).\r\n- Renaming a nextbot in-game will save its name back to Nextbots.txt.\r\n- You can add as many as you want, one per line.\r\n";

	private static string Folder
	{
		get
		{
			return Path.Combine(Variables.Variables_Text_01, "Custom Nextbots");
		}
	}

	private static string ListPath
	{
		get
		{
			return Path.Combine(Folder, "Nextbots.txt");
		}
	}

	private static string InstructionsPath
	{
		get
		{
			return Path.Combine(Folder, "Instructions.txt");
		}
	}

	private static void SelectCustomNextbot(string name)
	{
		CapturedVariables181_Text_01 = name;
		ButtonHandler.NavigateToCategory(Category.Custom_Nextbots);
	}

	private static void DeleteCustomNextbot(string name)
	{
		CapturedVariables170 LocalScope3 = new CapturedVariables170();
		LocalScope3.name = name;
		CapturedVariables181_Items_01.RemoveAll((Entry x) => x.Name == LocalScope3.name);
		SaveCustomNextbots();
		CapturedVariables181_Text_01 = null;
		RebuildCustomNextbotButtons();
		Main.RebuildMenu();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, LocalScope3.name);
	}

	public static void SaveCustomNextbots()
	{
		try
		{
			List<string> list = new List<string> { "# imageurl : soundurl        (optional:  name : ...  and  ... : jumpscareurl)" };
			list.AddRange(CapturedVariables181_Items_01.Select(delegate(Entry e)
			{
				string text;
				if (!string.IsNullOrEmpty(e.Jumpscare))
				{
					text = e.Image + " : " + e.Sound + " : " + e.Jumpscare;
					if (!e.HasCustomName)
					{
						goto Branch_0092;
					}
				}
				else
				{
					text = e.Image + " : " + e.Sound;
					if (!e.HasCustomName)
					{
						goto Branch_0092;
					}
				}
				return e.Name + " : " + text;
				Branch_0092:
				return text;
			}));
			File.WriteAllLines(ListPath, list);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("CustomNextbots save error: " + ex.Message));
		}
	}

	public static void RebuildCustomNextbotButtons()
	{
		List<ButtonHandler.Button> list = ModButtons.buttons.Where((ButtonHandler.Button b) => b != null && b.Page != Category.Custom_Nextbots).ToList();
		list.Add(new ButtonHandler.Button("Return", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Nextbots);
		})
		{
			isCategory = true
		});
		list.Add(new ButtonHandler.Button("Open Custom Nextbots Folder", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenFolder(Folder);
		}));
		using (List<Entry>.Enumerator enumerator = CapturedVariables181_Items_01.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				do
				{
					Entry current = enumerator.Current;
					CapturedVariables130 LocalScope3 = new CapturedVariables130();
					LocalScope3.entry = current;
					list.Add(new ButtonHandler.Button(LocalScope3.entry.Name, Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
					{
						SelectCustomNextbot(LocalScope3.entry.Name);
					})
					{
						isCategory = true
					});
				}
				while (enumerator.MoveNext());
			}
		}
		list.Add(new ButtonHandler.Button("Reload Custom Nextbots", Category.Custom_Nextbots, isToggle: false, isActive: false, ReloadCustomNextbots));
		ModButtons.buttons = list.ToArray();
	}

	private static void RenameCustomNextbot(string name)
	{
		CapturedVariables180 LocalScope15 = new CapturedVariables180();
		LocalScope15.name = name;
		LocalScope15.e = CapturedVariables181_Items_01.FirstOrDefault((Entry x) => x.Name == LocalScope15.name);
		if (LocalScope15.e == null)
		{
			return;
		}
		SearchAndKeyboard.OpenTextInput(LocalScope15.name, "Enter nextbot name...");
		SearchAndKeyboard.KeyCollider_Text_01 = delegate(string newName)
		{
			CapturedVariables181 LocalScope17 = new CapturedVariables181();
			LocalScope17.newName = newName;
			LocalScope17.newName = LocalScope17.newName.Trim();
			if (!string.IsNullOrEmpty(LocalScope17.newName) && !(LocalScope17.newName == LocalScope15.name))
			{
				if (CapturedVariables181_Items_01.Any((Entry x) => x.Name == LocalScope17.newName))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Name Already Exists");
				}
				else
				{
					LocalScope15.e.Name = LocalScope17.newName;
					LocalScope15.e.HasCustomName = true;
					SaveCustomNextbots();
					CapturedVariables181_Text_01 = null;
					RebuildCustomNextbotButtons();
					Main.RebuildMenu();
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed → " + LocalScope17.newName);
				}
			}
		};
	}

	public static List<ButtonHandler.Button> BuildCustomNextbotActions()
	{
		CapturedVariables140 LocalScope4 = new CapturedVariables140();
		LocalScope4.name = CapturedVariables181_Text_01;
		return new List<ButtonHandler.Button>
		{
			new ButtonHandler.Button("Return", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
			{
				CapturedVariables181_Text_01 = null;
				Main.RedrawMenu(-1);
			})
			{
				isCategory = true
			},
			new ButtonHandler.Button("Spawn", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
			{
				SpawnCustomNextbot(LocalScope4.name);
			}),
			new ButtonHandler.Button("Rename", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
			{
				RenameCustomNextbot(LocalScope4.name);
			}),
			new ButtonHandler.Button("Delete", Category.Custom_Nextbots, isToggle: false, isActive: false, delegate
			{
				DeleteCustomNextbot(LocalScope4.name);
			})
		};
	}

	public static void InitializeCustomNextbots()
	{
		try
		{
			Directory.CreateDirectory(Folder);
			if (!File.Exists(InstructionsPath))
			{
				File.WriteAllText(InstructionsPath, "=== HOW TO ADD CUSTOM CapturedVariables181_Reference_01 ===\r\n\r\n1. Open Nextbots.txt in this folder.\r\n\r\n2. Add one nextbot per line in this format:\r\n\r\n       imageurl : soundurl\r\n\r\n   - The image is what the nextbot looks like (a direct link to a .png / .jpg).\r\n   - The sound is the audio it plays / jumpscares with (a direct link to a .mp3 / .wav / .ogg).\r\n   - IMPORTANT: there must be a SPACE, a COLON, then a SPACE ( : ) between the two links.\r\n\r\n   Example:\r\n       https://site.com/troll.png : https://site.com/scream.mp3\r\n\r\n3. (Optional) Give it a custom name by adding one more field in front:\r\n\r\n       name : imageurl : soundurl\r\n\r\n   Example:\r\n       Troll : https://site.com/troll.png : https://site.com/scream.mp3\r\n\r\n4. (Optional) Give it a separate jumpscare sound by adding one more link at the end:\r\n\r\n       imageurl : soundurl : jumpscareurl\r\n       name : imageurl : soundurl : jumpscareurl\r\n\r\n   - The jumpscare sound plays when the nextbot catches you.\r\n   - Leave it off and it just uses its regular sound for the jumpscare instead.\r\n\r\n   Example:\r\n       Troll : https://site.com/troll.png : https://site.com/chase.mp3 : https://site.com/scream.mp3\r\n\r\n5. In-game go to: Nextbots > Custom Nextbots > Reload Custom Nextbots.\r\n\r\n6. Click a custom nextbot to Spawn, Rename, or Delete it.\r\n\r\nTIPS:\r\n- Use DIRECT image/audio links (the link should end in .png, .mp3, etc).\r\n- Renaming a nextbot in-game will save its name back to Nextbots.txt.\r\n- You can add as many as you want, one per line.\r\n");
				if (!File.Exists(ListPath))
				{
					goto Branch_0087;
				}
			}
			else if (!File.Exists(ListPath))
			{
				goto Branch_0087;
			}
			Load();
			RebuildCustomNextbotButtons();
			return;
			Branch_0087:
			File.WriteAllText(ListPath, "# Add one nextbot per line using this format:\r\n#\r\n# imageurl : soundurl\r\n#\r\n# There must be a space, a colon, then a space ( : ) between the two links.\r\n# Optionally give it a name:   name : imageurl : soundurl\r\n# Optionally add a jumpscare sound at the end (played when it catches you):\r\n#     imageurl : soundurl : jumpscareurl\r\n#\r\n# After editing, go in-game to Nextbots > Custom Nextbots > Reload Custom Nextbots.\r\n");
			Load();
			RebuildCustomNextbotButtons();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("CustomNextbots init error: " + ex.Message));
		}
	}

	public static void ReloadCustomNextbots()
	{
		CapturedVariables181_Text_01 = null;
		Load();
		RebuildCustomNextbotButtons();
		Main.RebuildMenu();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Loaded, $"Custom Nextbots `{CapturedVariables181_Items_01.Count}`");
	}

	public static void Load()
	{
		CapturedVariables181_Items_01.Clear();
		if (!File.Exists(ListPath))
		{
			return;
		}
		int num = 1;
		string[] array = File.ReadAllLines(ListPath);
		int num2 = 0;
		while (num2 < array.Length)
		{
			string text = array[num2];
			string text2 = text.Trim();
			CapturedVariables100 LocalScope10 = new CapturedVariables100();
			if (text.Trim().Length != 0 && !text2.StartsWith("#") && !text2.StartsWith("//"))
			{
				string[] array2 = (from p in text2.Split(new string[1] { " : " }, StringSplitOptions.None)
					select p.Trim()).ToArray();
				bool flag = !array2[0].StartsWith("http", StringComparison.OrdinalIgnoreCase);
				int num3 = 0;
				int num4 = num3;
				LocalScope10.e = new Entry
				{
					HasCustomName = flag
				};
				Entry entry = LocalScope10.e;
				object name;
				if (!flag)
				{
					name = $"Custom Nextbot {num}";
				}
				else
				{
					int num5 = num4;
					num3 = num5 + 1;
					num4 = num3;
					name = array2[num5];
				}
				entry.Name = (string)name;
				Entry entry2 = LocalScope10.e;
				object image;
				if (num4 >= array2.Length)
				{
					image = "";
				}
				else
				{
					int num6 = num4;
					num3 = num6 + 1;
					num4 = num3;
					image = array2[num6];
				}
				entry2.Image = (string)image;
				Entry entry3 = LocalScope10.e;
				object sound;
				if (num4 >= array2.Length)
				{
					sound = "";
				}
				else
				{
					int num7 = num4;
					num3 = num7 + 1;
					num4 = num3;
					sound = array2[num7];
				}
				entry3.Sound = (string)sound;
				LocalScope10.e.Jumpscare = ((num4 < array2.Length) ? array2[num4] : "");
				if (!string.IsNullOrEmpty(LocalScope10.e.Image))
				{
					if (CapturedVariables181_Items_01.Any((Entry x) => x.Name == LocalScope10.e.Name))
					{
						do
						{
							LocalScope10.e.Name += " *";
						}
						while (CapturedVariables181_Items_01.Any((Entry x) => x.Name == LocalScope10.e.Name));
					}
					CapturedVariables181_Items_01.Add(LocalScope10.e);
					num++;
					num2++;
					continue;
				}
			}
			num2++;
		}
	}

	private static void SpawnCustomNextbot(string name)
	{
		CapturedVariables160 LocalScope2 = new CapturedVariables160();
		LocalScope2.name = name;
		Entry entry = CapturedVariables181_Items_01.FirstOrDefault((Entry x) => x.Name == LocalScope2.name);
		if (entry != null)
		{
			Nextbots.SpawnNextbot(entry.Image, entry.Sound, 3.5f, 2f, entry.Name, entry.Jumpscare);
		}
	}
}
