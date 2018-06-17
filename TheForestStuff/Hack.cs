using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheForestStuff
{
	class Hack : MonoBehaviour
	{
		//UI
		//Menu
		public bool _menuVisible = true;
		public Rect _menuRect = new Rect(10, 10, 300, 300);
		//Items
		public bool _itemMenuVisible = false;
		public Rect _itemMenuRect = new Rect(350, 10, 250, 500);
		public Vector2 _itemScrollPosition = Vector2.zero;

		//Options
		//Cheats
		public bool _enableCheats = false;
		public bool _debugConsole = false;
		public bool _godMode = false;
		public bool _infiniteEnergy = false;

		void Start()
		{

		}

		void Update()
		{
			//Menu Toggle
			if (Input.GetKeyDown(KeyCode.Insert))
				_menuVisible = !_menuVisible;

			//Stuff
			Cheats.SetAllowed(_enableCheats);
			Cheats.DebugConsole = _debugConsole;
			Cheats.GodMode = _godMode;
			Cheats.InfiniteEnergy = _infiniteEnergy;
		}

		void OnGUI()
		{
			GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
			if (_menuVisible)
			{
				_menuRect = GUI.Window(1337, _menuRect, MenuFunction, "Unity is shit");

				if (_itemMenuVisible)
					_itemMenuRect = GUI.Window(1338, _itemMenuRect, ItemMenuFunction, "Items");
			}
		}

		void MenuFunction(int windowID)
		{
			//Drag Queen
			GUI.DragWindow(new Rect(0, 0, _menuRect.width, 20));

			_enableCheats = GUI.Toggle(new Rect(10, 20, 200, 20), _enableCheats, "Enable Cheats");
			_debugConsole = GUI.Toggle(new Rect(10, 40, 200, 20), _debugConsole, "Debug Console");
			_godMode = GUI.Toggle(new Rect(10, 60, 200, 20), _godMode, "God Mode");
			_infiniteEnergy = GUI.Toggle(new Rect(10, 80, 200, 20), _infiniteEnergy, "Infinite Energy");

			_itemMenuVisible = GUI.Toggle(new Rect(10, 100, 200, 20), _itemMenuVisible, "Show Item Menu");

			//Unload
			if (GUI.Button(new Rect(10, _menuRect.height - 25, _menuRect.width - 20, 20), "Unload"))
			{
				Class1.Unload();
			}
		}

		void ItemMenuFunction(int windowID)
		{
			GUI.DragWindow(new Rect(0, 0, _itemMenuRect.width, 20));

			TheForest.Items.Item[] items = TheForest.Items.ItemDatabase.Items;
			_itemScrollPosition = GUI.BeginScrollView(new Rect(10, 10, _itemMenuRect.width, _itemMenuRect.height), _itemScrollPosition, new Rect(0, 0, _itemMenuRect.width - 10, items.Length * 20));
			var y = 0;
			foreach (TheForest.Items.Item item in items)
			{
				if (GUI.Button(new Rect(5, y++ * 25, _itemMenuRect.width, 20), item._name))
				{
					TheForest.Utils.LocalPlayer.Inventory.AddItem(item._id);
					//Reload maybe?
				}
			}
			GUI.EndScrollView();
		}

		Dictionary<string,TheForest.Player.AchievementData> GetAchievements(object obj)
		{
				return obj.GetType().
						GetFields(BindingFlags.Public | BindingFlags.Static).
						Where(f => f.FieldType == typeof(TheForest.Player.AchievementData)).
						ToDictionary(f => f.Name,
									f => (TheForest.Player.AchievementData)f.GetValue(null));
		}
	}
}


