using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GorillaLocomotion;
using GorillaNetworking;
using NXO.Menu;
using NXO.Mods.Categories;
using NXO.Utilities;
using Photon.Pun;
using UnityEngine;

namespace NXO.Mods;

public class ModButtons
{
	private static ButtonHandler.Button[] ModButtons_Button_01 = new ButtonHandler.Button[509]
	{
		new ButtonHandler.Button("Settings", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Presets", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Presets);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Favorites", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Favorited);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Enabled", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Enabled);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Players List", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Players);
			PlayersActionList.ResetPlayersList();
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Safety", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Safety);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Room", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Room);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("VRRig", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Player);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Movement", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Movement);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Visuals", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Visuals);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Audio", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Audio);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Gamemode", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Gamemode);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Projectiles", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Projectiles);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Fun", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Fun);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Macros", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Macros);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Nextbots", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Nextbots);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Overpowered", Category.Home, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Overpowered);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Open NXO Folder", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenFolder(Variables.Variables_Text_01);
		}),
		new ButtonHandler.Button("Default Settings", Category.Settings, isToggle: false, isActive: false, delegate
		{
			Settings.DefaultSettings();
		}),
		new ButtonHandler.Button("Auto Save", Category.Settings, isToggle: true, isActive: true, delegate
		{
			ButtonHandler.SetAutoSaveEnabled(on: true);
		}, delegate
		{
			ButtonHandler.SetAutoSaveEnabled(on: false);
		}),
		new ButtonHandler.Button("Panic (X)", Category.Settings, isToggle: true, isActive: false, delegate
		{
			Safety.Panic();
		}, delegate
		{
			Safety.ResetPanic();
		}),
		new ButtonHandler.Button("Disable All Mods", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.DisableAllMods();
		}),
		new ButtonHandler.Button("Menu", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Menu_Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Menu_Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Right Hand Menu", Category.Menu_Settings, isToggle: true, isActive: false, delegate
		{
			Settings.SetRightHandMenuEnabled(setActive: true);
		}, delegate
		{
			Settings.SetRightHandMenuEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Open & Close Sounds", Category.Menu_Settings, isToggle: true, isActive: true, delegate
		{
			Settings.SetOpenAndCloseSoundsEnabled(setActive: true);
		}, delegate
		{
			Settings.SetOpenAndCloseSoundsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Dynamic Animations", Category.Menu_Settings, isToggle: true, isActive: true, delegate
		{
			Main.CapturedVariables1950_State_09 = true;
		}, delegate
		{
			Main.CapturedVariables1950_State_09 = false;
		}),
		new ButtonHandler.Button("Menu Smoothing", Category.Menu_Settings, isToggle: true, isActive: false, delegate
		{
			Main.CapturedVariables1950_State_06 = true;
		}, delegate
		{
			Main.CapturedVariables1950_State_06 = false;
		}),
		new ButtonHandler.Button("In-Game Array List", Category.Menu_Settings, isToggle: true, isActive: false, delegate
		{
			NotificationLib.NotificationType_State_02 = true;
		}, delegate
		{
			NotificationLib.NotificationType_State_02 = false;
		}),
		new ButtonHandler.Button("Classic Buttons", Category.Menu_Settings, isToggle: true, isActive: false, delegate
		{
			Settings.SetClassicButtonsEnabled(setActive: true);
		}, delegate
		{
			Settings.SetClassicButtonsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Menu Follows When Searching", Category.Menu_Settings, isToggle: true, isActive: true, delegate
		{
			Variables.Variables_State_06 = true;
		}, delegate
		{
			Variables.Variables_State_06 = false;
		}),
		new ButtonHandler.Button("Overlapping Custom Click Sound", Category.Menu_Settings, isToggle: true, isActive: false, delegate
		{
			ButtonHandler.CapturedVariables570_State_01 = true;
		}, delegate
		{
			ButtonHandler.CapturedVariables570_State_01 = false;
		}),
		new ButtonHandler.Button("Custom Board Colors", Category.Menu_Settings, isToggle: true, isActive: true, delegate
		{
			CustomBoards.SetCustomBoardColorsEnabled(enabled: true);
		}, delegate
		{
			CustomBoards.SetCustomBoardColorsEnabled(enabled: false);
		}),
		(Settings.CapturedVariables3760_Button_64 = new ButtonHandler.Button("Menu Font : " + Settings.CurrentFontDescription, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleMenuFont(forward: true);
		}, delegate
		{
			Settings.CycleMenuFont(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_37 = new ButtonHandler.Button("Click Sound : " + Settings.ClickSoundDescription, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleClickSound(forward: true);
		}, delegate
		{
			Settings.CycleClickSound(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_16 = new ButtonHandler.Button("Outline : " + Settings.CapturedVariables3760_Text_07[Settings.CapturedVariables3760_Index_32], Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleOutline(forward: true);
		}, delegate
		{
			Settings.CycleOutline(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_39 = new ButtonHandler.Button("Menu Size : " + Settings.CapturedVariables3760_Text_30, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleMenuSize(forward: true);
		}, delegate
		{
			Settings.CycleMenuSize(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_19 = new ButtonHandler.Button("Roundness : " + Settings.CapturedVariables3760_Text_50, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleRoundness(forward: true);
		}, delegate
		{
			Settings.CycleRoundness(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_42 = new ButtonHandler.Button("Opacity : " + Settings.CapturedVariables3760_Text_62, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleOpacity(forward: true);
		}, delegate
		{
			Settings.CycleOpacity(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_01 = new ButtonHandler.Button("Accent Strip : " + Settings.CapturedVariables3760_Text_24, Category.Menu_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleAccentStrip(forward: true);
		}, delegate
		{
			Settings.CycleAccentStrip(forward: false);
		})),
		new ButtonHandler.Button("Colors", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Color_Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Pinwheel", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Pinwheel);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Outline", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Outline);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Background", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Background);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Buttons", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Button);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Title", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Title);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Boards", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.Boards);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Accent Strip", Category.Color_Settings, isToggle: false, isActive: false, delegate
		{
			Settings.SelectColorElement(Settings.ColorElement.AccentStrip);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Notifications", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Notification_Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Notification_Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Toggle Notifications", Category.Notification_Settings, isToggle: true, isActive: true, delegate
		{
			Settings.SetToggleNotificationsEnabled(setActive: true);
		}, delegate
		{
			Settings.SetToggleNotificationsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Room Notifications", Category.Notification_Settings, isToggle: true, isActive: true, delegate
		{
			Settings.SetRoomNotificationsEnabled(setActive: true);
		}, delegate
		{
			Settings.SetRoomNotificationsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Room Notifications Sound", Category.Notification_Settings, isToggle: true, isActive: true, delegate
		{
			Settings.SetRoomNotificationsSoundEnabled(setActive: true);
		}, delegate
		{
			Settings.SetRoomNotificationsSoundEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Gun", Category.Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Gun_Settings);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Gun_Settings, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Settings);
		})
		{
			isCategory = true
		},
		(Settings.CapturedVariables3760_Button_57 = new ButtonHandler.Button("Gun Animation : " + Settings.CapturedVariables3760_Text_42, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunAnimation(forward: true);
		}, delegate
		{
			Settings.CycleGunAnimation(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_30 = new ButtonHandler.Button("Wiggle Intensity : " + Settings.CapturedVariables3760_Text_04, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleWiggleIntensity(forward: true);
		}, delegate
		{
			Settings.CycleWiggleIntensity(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_07 = new ButtonHandler.Button("Wiggle Speed : " + Settings.CapturedVariables3760_Text_31, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleWiggleSpeed(forward: true);
		}, delegate
		{
			Settings.CycleWiggleSpeed(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_32 = new ButtonHandler.Button("Gun Idle Color : " + Settings.CapturedVariables3760_Text_17, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunIdleColor(forward: true);
		}, delegate
		{
			Settings.CycleGunIdleColor(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_23 = new ButtonHandler.Button("Gun Fire Color : " + Settings.CapturedVariables3760_Text_06, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunFireColor(forward: true);
		}, delegate
		{
			Settings.CycleGunFireColor(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_22 = new ButtonHandler.Button("Gun Hover Color : " + Settings.CapturedVariables3760_Text_38, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunHoverColor(forward: true);
		}, delegate
		{
			Settings.CycleGunHoverColor(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_38 = new ButtonHandler.Button("Gun Lock Color : " + Settings.CapturedVariables3760_Text_54, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunLockColor(forward: true);
		}, delegate
		{
			Settings.CycleGunLockColor(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_40 = new ButtonHandler.Button("Gun Pointer Size : " + Settings.CapturedVariables3760_Text_20, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunPointerSize(forward: true);
		}, delegate
		{
			Settings.CycleGunPointerSize(forward: false);
		})),
		(Settings.CapturedVariables3760_Button_55 = new ButtonHandler.Button("Gun Line Thickness : " + Settings.CapturedVariables3760_Text_55, Category.Gun_Settings, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleGunLineThickness(forward: true);
		}, delegate
		{
			Settings.CycleGunLineThickness(forward: false);
		})),
		new ButtonHandler.Button("Gun Line", Category.Gun_Settings, isToggle: true, isActive: true, delegate
		{
			Settings.SetGunLineEnabled(setActive: true);
		}, delegate
		{
			Settings.SetGunLineEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Left Hand Gun", Category.Gun_Settings, isToggle: true, isActive: false, delegate
		{
			GunLib.GunLib_State_02 = true;
		}, delegate
		{
			GunLib.GunLib_State_02 = false;
		}),
		new ButtonHandler.Button("Open Presets Folder", Category.Presets, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenFolder(Path.Combine(Variables.Variables_Text_01, "Presets"));
		}),
		new ButtonHandler.Button("Save Preset", Category.Presets, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.SavePreset();
		}),
		new ButtonHandler.Button("Saved Presets", Category.Presets, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Saved_Presets);
		})
		{
			isCategory = true
		},
		(Settings.CapturedVariables3760_Button_44 = new ButtonHandler.Button("Anti Report Radius : " + Settings.CapturedVariables3760_Text_47, Category.Safety, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleAntiReportRadius(forward: true);
		}, delegate
		{
			Settings.CycleAntiReportRadius(forward: false);
		})),
		new ButtonHandler.Button("Visualize Anti Report", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.VisualizeAntiReport();
		}, delegate
		{
			Safety.ResetVisualizeAntiReport();
		}),
		new ButtonHandler.Button("Anti Report", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.AntiReport(autoQueue: false, reconnect: false);
		}),
		new ButtonHandler.Button("Join Random Anti Report", Category.Safety, isToggle: true, isActive: true, delegate
		{
			Safety.AntiReport(autoQueue: true, reconnect: false);
		}),
		new ButtonHandler.Button("Reconnect Anti Report", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.AntiReport(autoQueue: false, reconnect: true);
		}),
		new ButtonHandler.Button("No Finger Movement", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Player.NoFingerMovement();
		}),
		new ButtonHandler.Button("Change Identity", Category.Safety, isToggle: false, isActive: false, delegate
		{
			Safety.ChangeIdentity();
		}),
		new ButtonHandler.Button("Change Identity On Disconnect", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.ChangeIdentityOnDisconnect(Safety.ResetChangeIdentityOnDisconnect);
		}),
		new ButtonHandler.Button("Name Spoof", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.NameSpoof();
		}),
		new ButtonHandler.Button("Color Spoof", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.ColorSpoof();
		}),
		new ButtonHandler.Button("Ranked Platform Spoof", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.SetRankedPlatformSpoofEnabled(enabled: true);
		}, delegate
		{
			Safety.SetRankedPlatformSpoofEnabled(enabled: false);
		}),
		new ButtonHandler.Button("Ranked Badge Spoof", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.SetRankedBadgeSpoofEnabled(enable: true);
		}, delegate
		{
			Safety.SetRankedBadgeSpoofEnabled(enable: false);
		}),
		new ButtonHandler.Button("Bypass Mod Checkers", Category.Safety, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_02 = true;
			Safety.BypassModCheckers();
		}, delegate
		{
			MenuPatches.GrabPatch_State_02 = false;
		}),
		new ButtonHandler.Button("Bypass Automod", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.BypassAutomod();
		}),
		new ButtonHandler.Button("Anti Stump Kick", Category.Safety, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GroupPatch.GroupPatch_State_01 = true;
		}, delegate
		{
			MenuPatches.GroupPatch.GroupPatch_State_01 = false;
		}),
		new ButtonHandler.Button("Anti Moderator", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.AntiModerator();
		}),
		new ButtonHandler.Button("Anti Grab App Quit", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Room.AntiGrabAppQuit(isActive: false);
		}, delegate
		{
			Room.AntiGrabAppQuit(isActive: true);
		}),
		new ButtonHandler.Button("Anti Cheat Notifications", Category.Safety, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_11 = true;
		}, delegate
		{
			MenuPatches.GrabPatch_State_11 = false;
		}),
		new ButtonHandler.Button("Accept TOS", Category.Safety, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_04 = true;
		}, delegate
		{
			MenuPatches.GrabPatch_State_04 = false;
		}),
		new ButtonHandler.Button("Bypass K-ID", Category.Safety, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_12 = true;
		}, delegate
		{
			MenuPatches.GrabPatch_State_12 = false;
		}),
		new ButtonHandler.Button("Fake Lag (A)", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Safety.FakeLag();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Disable Quit Box", Category.Safety, isToggle: true, isActive: false, delegate
		{
			Room.AntiGrabAppQuit(isActive: false);
		}, delegate
		{
			Room.AntiGrabAppQuit(isActive: true);
		}),
		new ButtonHandler.Button("Quit Game", Category.Room, false, false, (Action)Application.Quit, (Action)null, false, (Action)null, (Action)null),
		new ButtonHandler.Button("Disconnect", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.Disconnect();
		}),
		new ButtonHandler.Button("Reconnect", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.Reconnect();
		}),
		new ButtonHandler.Button("Join Random Public", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.JoinRandomPublic();
		}),
		new ButtonHandler.Button("Create Public", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.CreatePublic(Room.ResetCreatePublic(), isPublic: true, 0, (JoinType)0);
		}),
		new ButtonHandler.Button("Create Private", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.CreatePublic(Room.ResetCreatePublic(), isPublic: false, 0, (JoinType)0);
		}),
		new ButtonHandler.Button("Join Specific Room", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.JoinSpecificRoom();
		}),
		new ButtonHandler.Button("Unlock VIM", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.UnlockVIM();
		}),
		new ButtonHandler.Button("Reauthenticate", Category.Room, isToggle: false, isActive: false, delegate
		{
			((MothershipAuthenticator)MothershipAuthenticator.Instance).BeginLoginFlow();
		}),
		new ButtonHandler.Button("Disable Network Triggers", Category.Room, isToggle: true, isActive: false, delegate
		{
			Room.DisableNetworkTriggers(isActive: false);
		}, delegate
		{
			Room.DisableNetworkTriggers(isActive: true);
		}),
		new ButtonHandler.Button("Grab All IDs", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.GrabAllIDs();
		}),
		new ButtonHandler.Button("Grab Own ID", Category.Room, isToggle: false, isActive: false, delegate
		{
			Room.GrabOwnID();
		}),
		new ButtonHandler.Button("Anti AFK Kick", Category.Room, isToggle: true, isActive: false, delegate
		{
			((PhotonNetworkController)PhotonNetworkController.Instance).disableAFKKick = true;
		}, delegate
		{
			((PhotonNetworkController)PhotonNetworkController.Instance).disableAFKKick = false;
		}),
		new ButtonHandler.Button("US Servers", Category.Room, isToggle: false, isActive: false, delegate
		{
			PhotonNetwork.ConnectToRegion("us");
		}),
		new ButtonHandler.Button("USW Servers", Category.Room, isToggle: false, isActive: false, delegate
		{
			PhotonNetwork.ConnectToRegion("usw");
		}),
		new ButtonHandler.Button("EU Servers", Category.Room, isToggle: false, isActive: false, delegate
		{
			PhotonNetwork.ConnectToRegion("eu");
		}),
		new ButtonHandler.Button("Queue to Default", Category.Room, isToggle: false, isActive: false, delegate
		{
			((GorillaComputer)GorillaComputer.instance).currentQueue = "DEFAULT";
		}),
		new ButtonHandler.Button("Queue to Minigames", Category.Room, isToggle: false, isActive: false, delegate
		{
			((GorillaComputer)GorillaComputer.instance).currentQueue = "MINIGAMES";
		}),
		new ButtonHandler.Button("Queue to Competitive", Category.Room, isToggle: false, isActive: false, delegate
		{
			((GorillaComputer)GorillaComputer.instance).currentQueue = "COMPETITIVE";
		}),
		new ButtonHandler.Button("Ghost Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GhostMonke();
		}),
		new ButtonHandler.Button("Invisible Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.InvisibleMonke();
		}),
		new ButtonHandler.Button("Ghost & Invisibility (A/B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GhostAndInvisibility();
		}),
		(Settings.CapturedVariables3760_Button_21 = new ButtonHandler.Button("Long Arms Length : " + Settings.CapturedVariables3760_Text_05, Category.Player, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleLongArmsLength(forward: true);
		}, delegate
		{
			Settings.CycleLongArmsLength(forward: false);
		})),
		new ButtonHandler.Button("Long Arms", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetLongArmsEnabled(setActive: true);
		}, delegate
		{
			Player.SetLongArmsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Size Change (LT/RT/CS)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SizeChange();
		}),
		new ButtonHandler.Button("Networked Size Change (LT/RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Networked.SizeChanger.UpdateNetworkedSize(enable: true);
		}, delegate
		{
			Networked.SizeChanger.UpdateNetworkedSize(enable: false);
		}),
		new ButtonHandler.Button("Spin Head", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetHeadPose(null, spin: true);
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Upside Head", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetHeadPose((Vector3?)new Vector3(180f, 0f, 0f), false);
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Backwards Head", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetHeadPose((Vector3?)new Vector3(0f, 180f, 0f), false);
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Snap Neck (Left)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetHeadPose((Vector3?)new Vector3(0f, 0f, 90f), false);
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Snap Neck (Right)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SetHeadPose((Vector3?)new Vector3(0f, 0f, -90f), false);
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Freeze Rig (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.FreezeRig();
		}),
		new ButtonHandler.Button("Grab Rig (RG/LG)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GrabRig();
		}),
		new ButtonHandler.Button("Fake Body Tracking", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.FakeBodyTracking();
		}),
		new ButtonHandler.Button("Ragdoll Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.RagdollMonke();
		}, delegate
		{
			Player.ResetRagdollMonke();
		}),
		new ButtonHandler.Button("T-Pose (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.TPose();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Lay On Back (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.LayOnBack();
		}),
		new ButtonHandler.Button("Lay On Stomach (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.LayOnStomach();
		}),
		new ButtonHandler.Button("Upside Down (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.UpsideDown();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Backflip (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.Backflip();
		}, delegate
		{
			Player.ResetBackflip();
		}),
		new ButtonHandler.Button("Frontflip (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.Frontflip();
		}, delegate
		{
			Player.ResetFrontflip();
		}),
		new ButtonHandler.Button("Cartwheel (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.Cartwheel();
		}, delegate
		{
			Player.ResetCartwheel();
		}),
		new ButtonHandler.Button("Griddy (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.Griddy();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Dance Monke (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.DanceMonke();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Spaz Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SpazMonke();
		}, delegate
		{
			Player.ResetHeadPose();
		}),
		new ButtonHandler.Button("Spider Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.SpiderMonke();
		}, delegate
		{
			Player.ResetSpiderMonke();
		}),
		new ButtonHandler.Button("Glitch Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GlitchMonke();
		}, delegate
		{
			Player.ResetGlitchMonke();
		}),
		new ButtonHandler.Button("Wobbly Monke (B)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.WobblyMonke();
		}, delegate
		{
			Player.ResetWobblyMonke();
		}),
		new ButtonHandler.Button("Helicopter Monke (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.HelicopterMonke();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Ascend Monke (RT)", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.AscendMonke();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Rig Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.RigGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Chase Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.ChaseGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Chase All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.ChaseAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Fly Towards Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.FlyTowardsGun();
		}, delegate
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Orbit Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.OrbitGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Orbit All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.OrbitAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Jumpscare Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.JumpscareGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Jumpscare All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.JumpscareAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Copy Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.CopyPlayerGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Copy All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.CopyAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Mirror Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.MirrorPlayerGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Mirror All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.MirrorAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Backshot Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.BackshotPlayerGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Backshot All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.BackshotAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Gawk Gawk Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GawkGawkGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Gawk Gawk All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.GawkGawkAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Piggyback Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.PiggybackPlayerGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Piggyback All", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.PiggybackAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Only Visible To Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.OnlyVisibleToPlayerGun();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Only Invisible To Player Gun", Category.Player, isToggle: true, isActive: false, delegate
		{
			Player.OnlyInvisibleToPlayerGun();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		(Settings.CapturedVariables3760_Button_48 = new ButtonHandler.Button("Fly Speed : " + Settings.CapturedVariables3760_Text_21, Category.Movement, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleFlySpeed(forward: true);
		}, delegate
		{
			Settings.CycleFlySpeed(forward: false);
		})),
		new ButtonHandler.Button("Fly (A)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.Fly(useVelocity: false);
		}),
		new ButtonHandler.Button("Fly + Noclip (A)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.FlyPlusNoclip();
		}),
		(Settings.CapturedVariables3760_Button_41 = new ButtonHandler.Button("Platform Type : " + Settings.CapturedVariables3760_Text_35, Category.Movement, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CyclePlatformType(forward: true);
		}, delegate
		{
			Settings.CyclePlatformType(forward: false);
		})),
		new ButtonHandler.Button("Use Triggers For Platforms", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Settings.SetUseTriggersForPlatformsEnabled(setActive: true);
		}, delegate
		{
			Settings.SetUseTriggersForPlatformsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Platforms (RG/LG)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.Platforms();
		}),
		new ButtonHandler.Button("Frozone (RG/LG)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.Frozone();
		}),
		(Settings.CapturedVariables3760_Button_63 = new ButtonHandler.Button("Boost Speed : " + Settings.CapturedVariables3760_Text_10, Category.Movement, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleBoostSpeed(forward: true);
		}, delegate
		{
			Settings.CycleBoostSpeed(forward: false);
		})),
		new ButtonHandler.Button("Use Grip For Speedboost", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Settings.SetUseGripForSpeedboostEnabled(setActive: true);
		}, delegate
		{
			Settings.SetUseGripForSpeedboostEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Speed Boost", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SpeedBoost();
		}),
		new ButtonHandler.Button("Noclip (RT)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.Noclip();
		}),
		(Settings.CapturedVariables3760_Button_45 = new ButtonHandler.Button("TP To : " + Settings.MapDescription, Category.Movement, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleTPTo(forward: true);
		}, delegate
		{
			Settings.CycleTPTo(forward: false);
		})),
		new ButtonHandler.Button("Teleport To Map", Category.Movement, isToggle: false, isActive: false, delegate
		{
			Movement.TeleportToMap();
		}),
		new ButtonHandler.Button("Teleport Gun", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Player.TeleportGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		(Settings.CapturedVariables3760_Button_60 = new ButtonHandler.Button("Wall Walk Strength : " + Settings.CapturedVariables3760_Text_39, Category.Movement, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleWallWalkStrength(forward: true);
		}, delegate
		{
			Settings.CycleWallWalkStrength(forward: false);
		})),
		new ButtonHandler.Button("Wall Walk (RG/LG)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.WallWalk();
		}),
		new ButtonHandler.Button("Pull Mod", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.PullMod();
		}),
		new ButtonHandler.Button("Iron Monke (RG/LG)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.IronMonke(10);
		}),
		new ButtonHandler.Button("Accel Fly (A)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.Fly(useVelocity: true);
		}),
		new ButtonHandler.Button("Up And Down (LT/RT)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.UpAndDown();
		}),
		new ButtonHandler.Button("Low Gravity", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.LowGravity();
		}),
		new ButtonHandler.Button("Zero Gravity", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.ZeroGravity();
		}),
		new ButtonHandler.Button("High Gravity", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.HighGravity();
		}),
		new ButtonHandler.Button("Reverse Gravity", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.ReverseGravity();
		}),
		new ButtonHandler.Button("WASD Movement", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.WASDMovement();
		}),
		new ButtonHandler.Button("Joystick Fly", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.JoystickFly();
		}),
		new ButtonHandler.Button("Uncap Max Velocity", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_06.maxJumpSpeed = 9999f;
		}),
		new ButtonHandler.Button("Fast Swim Speed", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.FastSwimSpeed();
		}),
		new ButtonHandler.Button("Dash Monke (A)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.DashMonke(isDashEnabled: true, isAirJumpEnabled: false);
		}),
		new ButtonHandler.Button("Checkpoint (RG/RT)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SetCheckpointEnabled(setActive: true);
		}, delegate
		{
			Movement.SetCheckpointEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Wall Climber (RG/LG)", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SetWallClimberEnabled(enable: true);
		}, delegate
		{
			Movement.SetWallClimberEnabled(enable: false);
		}),
		new ButtonHandler.Button("Slide Control", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SetSlideControlEnabled(setActive: true);
		}, delegate
		{
			Movement.SetSlideControlEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Anti Slip", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SetAntiSlipEnabled(setActive: true);
		}, delegate
		{
			Movement.SetAntiSlipEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Slippy Hands", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Movement.SetSlippyHandsEnabled(setActive: true);
		}, delegate
		{
			Movement.SetSlippyHandsEnabled(setActive: false);
		}),
		new ButtonHandler.Button("No Tag Freeze", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_06.disableMovement = false;
		}),
		new ButtonHandler.Button("Force Tag Freeze", Category.Movement, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_06.disableMovement = true;
		}, delegate
		{
			Variables.Variables_Reference_06.disableMovement = false;
		}),
		(Settings.CapturedVariables3760_Button_25 = new ButtonHandler.Button("Nametag Type : " + Settings.CapturedVariables3760_Text_44, Category.Visuals, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleNametagType(forward: true);
		}, delegate
		{
			Settings.CycleNametagType(forward: false);
		})),
		new ButtonHandler.Button("Name Tags", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetNameTagsEnabled(enable: true);
		}, delegate
		{
			Visuals.SetNameTagsEnabled(enable: false);
		}),
		new ButtonHandler.Button("NXO Nametags", Category.Visuals, isToggle: true, isActive: true, delegate
		{
			Visuals.SetNXONametagsEnabled(enable: true);
		}, delegate
		{
			Visuals.SetNXONametagsEnabled(enable: false);
		}),
		new ButtonHandler.Button("Team Checked ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Settings.SetTeamCheckedESPEnabled(setActive: true);
		}, delegate
		{
			Settings.SetTeamCheckedESPEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Unfilled Box ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetUnfilledBoxESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetUnfilledBoxESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Unfilled Box ESP 2D", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetUnfilledBoxESP2DEnabled(enable: true);
		}, delegate
		{
			Visuals.SetUnfilledBoxESP2DEnabled(enable: false);
		}),
		new ButtonHandler.Button("Filled Box ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetFilledBoxESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetFilledBoxESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Filled Box ESP 2D", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetFilledBoxESP2DEnabled(enable: true);
		}, delegate
		{
			Visuals.SetFilledBoxESP2DEnabled(enable: false);
		}),
		(Settings.CapturedVariables3760_Button_62 = new ButtonHandler.Button("Tracer Position : " + Settings.CapturedVariables3760_Text_57, Category.Visuals, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleTracerPosition(forward: true);
		}, delegate
		{
			Settings.CycleTracerPosition(forward: false);
		})),
		new ButtonHandler.Button("Tracers ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetTracersESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetTracersESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Prediction ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetPredictionESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetPredictionESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Bone ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetBoneESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetBoneESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Skeleton ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetSkeletonESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetSkeletonESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Chams ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetChamsESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetChamsESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Trails ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetTrailsESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetTrailsESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Beacons ESP", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetBeaconsESPEnabled(enable: true);
		}, delegate
		{
			Visuals.SetBeaconsESPEnabled(enable: false);
		}),
		(Settings.CapturedVariables3760_Button_06 = new ButtonHandler.Button("FPC FOV : " + Settings.CapturedVariables3760_Text_43, Category.Visuals, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleFPCFOV(forward: true);
		}, delegate
		{
			Settings.CycleFPCFOV(forward: false);
		})),
		new ButtonHandler.Button("First Person Cam (PC)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetFirstPersonCamEnabled(enable: true);
		}, delegate
		{
			Visuals.SetFirstPersonCamEnabled(enable: false);
		}),
		new ButtonHandler.Button("Free Cam (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.FreeCam();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("VR 3rd Person (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.VR3rdPerson();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("Drunk Cam (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.DrunkCam();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("Spectate Gun", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SpectateGun();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("Orbit Cam (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.OrbitCam();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("VR 3rd Person In Front (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.VR3rdPersonInFront();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("Upside Down Cam (X)", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.UpsideDownCam();
		}, delegate
		{
			Visuals.ResetCamera();
		}),
		new ButtonHandler.Button("Monke Sense", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetMonkeSenseEnabled(enable: true);
		}, delegate
		{
			Visuals.SetMonkeSenseEnabled(enable: false);
		}),
		new ButtonHandler.Button("Mute Monke Sense", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.CapturedVariables580_State_06 = true;
		}, delegate
		{
			Visuals.CapturedVariables580_State_06 = false;
		}),
		new ButtonHandler.Button("FPS Boost", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetFPSBoostEnabled(enable: true);
		}, delegate
		{
			Visuals.SetFPSBoostEnabled(enable: false);
		}),
		new ButtonHandler.Button("X-Ray", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetXRayEnabled(enabled: true);
		}, delegate
		{
			Visuals.SetXRayEnabled(enabled: false);
		}),
		new ButtonHandler.Button("Toggle Fog", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetToggleFogEnabled(enable: true);
		}, delegate
		{
			Visuals.SetToggleFogEnabled(enable: false);
		}),
		new ButtonHandler.Button("Fuck Colors", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetFuckColorsEnabled(enable: true);
		}, delegate
		{
			Visuals.SetFuckColorsEnabled(enable: false);
		}),
		new ButtonHandler.Button("Acid Trip", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetAcidTripEnabled(enable: true);
		}, delegate
		{
			Visuals.SetAcidTripEnabled(enable: false);
		}),
		new ButtonHandler.Button("Trippy Monkes", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetTrippyMonkesEnabled(enable: true);
		}, delegate
		{
			Visuals.SetTrippyMonkesEnabled(enable: false);
		}),
		new ButtonHandler.Button("Shiny Monkes", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetShinyMonkesEnabled(enable: true);
		}, delegate
		{
			Visuals.SetShinyMonkesEnabled(enable: false);
		}),
		new ButtonHandler.Button("Shiny Self", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetShinySelfEnabled(enable: true);
		}, delegate
		{
			Visuals.SetShinySelfEnabled(enable: false);
		}),
		new ButtonHandler.Button("Ghost Rig", Category.Visuals, isToggle: true, isActive: true, delegate
		{
			Settings.SetGhostRigEnabled(setActive: true);
		}, delegate
		{
			Settings.SetGhostRigEnabled(setActive: false);
		}),
		new ButtonHandler.Button("90 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(90);
		}),
		new ButtonHandler.Button("72 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(72);
		}),
		new ButtonHandler.Button("60 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(60);
		}),
		new ButtonHandler.Button("45 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(45);
		}),
		new ButtonHandler.Button("30 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(30);
		}),
		new ButtonHandler.Button("15 FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.LimitFrameRate(15);
		}),
		new ButtonHandler.Button("Uncap FPS", Category.Visuals, isToggle: true, isActive: false, delegate
		{
			Visuals.SetUncapFPSEnabled(enabled: true);
		}, delegate
		{
			Visuals.SetUncapFPSEnabled(enabled: false);
		}),
		new ButtonHandler.Button("Enter Soundboard", Category.Audio, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Soundboard);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Audio);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Load Soundboard", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			Soundboard.LoadSoundboard();
		}),
		new ButtonHandler.Button("Disable All Sounds", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			Soundboard.SoundboardSoundsActive.Keys.ToList().ForEach(Soundboard.DisableAllSounds);
		}),
		new ButtonHandler.Button("Loop Sounds", Category.Soundboard, isToggle: true, isActive: false, delegate
		{
			Soundboard.LoopSounds();
		}, delegate
		{
			Soundboard.ResetLoopSounds();
		}),
		(Settings.CapturedVariables3760_Button_05 = new ButtonHandler.Button("Sound Input : " + Settings.CapturedVariables3760_Text_33, Category.Soundboard, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleSoundInput();
		}, delegate
		{
			Settings.CycleSoundInput(forward: false);
		})),
		new ButtonHandler.Button("Return", Category.SFX, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Soundboard);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Trolling, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Soundboard);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Return", Category.Songs, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Soundboard);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Reload Microphone", Category.Audio, isToggle: false, isActive: false, delegate
		{
			Sound.ReloadMicrophone();
		}),
		new ButtonHandler.Button("Hear Self", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_09.myRecorder.DebugEchoMode = true;
		}, delegate
		{
			Variables.Variables_Reference_09.myRecorder.DebugEchoMode = false;
		}),
		new ButtonHandler.Button("High Quality Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMicrophoneQuality(65000, 48000);
		}, delegate
		{
			Sound.SetMicrophoneQuality(20000, 16000);
		}),
		new ButtonHandler.Button("Low Quality Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMicrophoneQuality(6000, 8000);
		}, delegate
		{
			Sound.SetMicrophoneQuality(20000, 16000);
		}),
		new ButtonHandler.Button("Reverb Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetReverbMicrophoneEnabled(enable: true);
		}, delegate
		{
			Sound.SetReverbMicrophoneEnabled(enable: false);
		}),
		new ButtonHandler.Button("Loud Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetLoudMicrophoneEnabled(enable: true);
		}, delegate
		{
			Sound.SetLoudMicrophoneEnabled(enable: false);
		}),
		new ButtonHandler.Button("Mute Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMuteMicrophoneEnabled(mute: true);
		}, delegate
		{
			Sound.SetMuteMicrophoneEnabled(mute: false);
		}),
		new ButtonHandler.Button("High Pitch Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMicrophonePitch(1.2f);
		}, delegate
		{
			Sound.SetMicrophonePitch(1f);
		}),
		new ButtonHandler.Button("Low Pitch Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMicrophonePitch(0.75f);
		}, delegate
		{
			Sound.SetMicrophonePitch(1f);
		}),
		new ButtonHandler.Button("Echo Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetEchoMicrophoneEnabled(enable: true);
		}, delegate
		{
			Sound.SetEchoMicrophoneEnabled(enable: false);
		}),
		new ButtonHandler.Button("Static Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetStaticMicrophoneEnabled(enable: true);
		}, delegate
		{
			Sound.SetStaticMicrophoneEnabled(enable: false);
		}),
		new ButtonHandler.Button("Muffled Microphone", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SetMuffledMicrophoneEnabled(enable: true);
		}, delegate
		{
			Sound.SetMuffledMicrophoneEnabled(enable: false);
		}),
		new ButtonHandler.Button("Random Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(Random.Range(0, 228));
		}),
		new ButtonHandler.Button("Annoying Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(248);
		}),
		new ButtonHandler.Button("Boop Player", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.PlayContactSound(84);
		}),
		new ButtonHandler.Button("Gong Player", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.PlayContactSound(248);
		}),
		new ButtonHandler.Button("Slap Player", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.PlayContactSound(338);
		}),
		new ButtonHandler.Button("Jman HELLO! Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(337);
		}),
		new ButtonHandler.Button("Jman Slap Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(338);
		}),
		new ButtonHandler.Button("Jman Okay Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(336);
		}),
		new ButtonHandler.Button("Glass Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(28);
		}),
		new ButtonHandler.Button("Metal Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(18);
		}),
		new ButtonHandler.Button("Pop Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(84);
		}),
		new ButtonHandler.Button("Squeaky Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(75);
		}),
		new ButtonHandler.Button("Crystal Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(213);
		}),
		new ButtonHandler.Button("Turkey Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(83);
		}),
		new ButtonHandler.Button("Frog Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(91);
		}),
		new ButtonHandler.Button("AK-47 Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(203);
		}),
		new ButtonHandler.Button("Wolf Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(195);
		}),
		new ButtonHandler.Button("Cat Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(236);
		}),
		new ButtonHandler.Button("Bee Sound Spam (RT)", Category.Audio, isToggle: true, isActive: false, delegate
		{
			Sound.SpamHandTapSound(191);
		}),
		new ButtonHandler.Button("Tag All (RT)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.TagAll();
		}),
		new ButtonHandler.Button("Tag Gun", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.TagGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Flick Tag (RT)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.FlickTag();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Tag Aura", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.TagAura();
		}),
		new ButtonHandler.Button("Tag Self", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.TagSelf();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Try Un-Tag Self", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.TryRemoveTagSelf();
		}),
		new ButtonHandler.Button("Anti Tag", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.AntiTag();
		}),
		new ButtonHandler.Button("No Tag On Join", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.SetNoTagOnJoinEnabled(setActive: true);
		}, delegate
		{
			Gamemode.SetNoTagOnJoinEnabled(setActive: false);
		}),
		new ButtonHandler.Button("No Tag Limit", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_09.maxTagDistance = float.MaxValue;
		}, delegate
		{
			Variables.Variables_Reference_09.maxTagDistance = 1.2f;
		}),
		new ButtonHandler.Button("Untag All (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.UntagAll();
		}),
		new ButtonHandler.Button("Untag Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.UntagGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Tag Lag (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Overpowered.SetTagLagEnabled(enable: true);
		}, delegate
		{
			Overpowered.SetTagLagEnabled(enable: false);
		}),
		new ButtonHandler.Button("Auto Guardian", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.AutoGuardian();
		}),
		new ButtonHandler.Button("Guardian Grab All (RG)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianGrabAll();
		}),
		new ButtonHandler.Button("Guardian Release All (RT)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianReleaseAll();
		}),
		new ButtonHandler.Button("Guardian Orbit All (RT)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianOrbitAll();
		}),
		new ButtonHandler.Button("Guardian Fling All (RT)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianFlingAll();
		}),
		new ButtonHandler.Button("Guardian Fling Gun", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianFlingGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Guardian Bring All To Pointer", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianBringAllToPointer();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Guardian Self (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.GuardianSelf();
		}),
		new ButtonHandler.Button("Guardian All (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.GuardianAll();
		}),
		new ButtonHandler.Button("Guardian Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.GuardianGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Un-Guardian Self (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.RemoveGuardianSelf();
		}),
		new ButtonHandler.Button("Un-Guardian All (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.RemoveGuardianAll();
		}),
		new ButtonHandler.Button("Un-Guardian Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.RemoveGuardianGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Paintbrawl Aimbot", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			MenuPatches.SlingshotAimbot.CapturedVariables10_State_01 = true;
		}, delegate
		{
			MenuPatches.SlingshotAimbot.CapturedVariables10_State_01 = false;
		}),
		new ButtonHandler.Button("Paintbrawl Kill All", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlKillAll();
		}),
		new ButtonHandler.Button("Paintbrawl Kill Gun", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlKillGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Paintbrawl Kill Self", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlKillSelf();
		}),
		new ButtonHandler.Button("Paintbrawl Godmode (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlGodmode();
		}),
		new ButtonHandler.Button("Paintbrawl No Delay (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.SetPaintbrawlNoDelayEnabled(enable: true);
		}, delegate
		{
			Gamemode.SetPaintbrawlNoDelayEnabled(enable: false);
		}),
		new ButtonHandler.Button("Paintbrawl Mat All (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlMatAll();
		}),
		new ButtonHandler.Button("Paintbrawl Mat Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlMatGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Paintbrawl End Game (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.PaintbrawlEndGame();
		}),
		new ButtonHandler.Button("Paintbrawl Start Game (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.PaintbrawlStartGame();
		}),
		new ButtonHandler.Button("Paintbrawl Restart Game (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.PaintbrawlRestartGame();
		}),
		new ButtonHandler.Button("Paintbrawl Spam Balloon All (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlSpamBalloonAll();
		}),
		new ButtonHandler.Button("Paintbrawl Spam Balloon Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlSpamBalloonGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Paintbrawl Spam Balloon Self (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlSpamBalloonSelf();
		}),
		new ButtonHandler.Button("Paintbrawl Revive All (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.PaintbrawlReviveAll();
		}),
		new ButtonHandler.Button("Paintbrawl Revive Gun (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			Gamemode.PaintbrawlReviveGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Paintbrawl Revive Self (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			Gamemode.PaintbrawlReviveSelf();
		}),
		new ButtonHandler.Button("Float Gun (Blaster)", Category.Gamemode, isToggle: true, isActive: false, NxoSuperInfection.FloatGun),
		new ButtonHandler.Button("Complete Quests", Category.Gamemode, isToggle: false, isActive: false, NxoSuperInfection.CompleteQuests),
		new ButtonHandler.Button("Complete & Claim Quests", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			NxoSuperInfection.CompleteAndClaimQuests(SIPlayer.LocalPlayer);
		}),
		new ButtonHandler.Button("Add TechPoints", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			NxoSuperInfection.AddTechPoints((SIResource.ResourceType)0, 10);
		}),
		new ButtonHandler.Button("Give All Resources", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			NxoSuperInfection.GiveAllResources(SIPlayer.LocalPlayer);
		}),
		new ButtonHandler.Button("Unlock All Gadgets", Category.Gamemode, isToggle: false, isActive: false, NxoSuperInfection.UnlockAllGadgets),
		new ButtonHandler.Button("Unlock Full Tree", Category.Gamemode, isToggle: false, isActive: false, NxoSuperInfection.UnlockFullTree),
		new ButtonHandler.Button("No Blaster Cooldown", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			using IEnumerator<SIGadgetBlaster> enumerator = (from x in NxoSuperInfection.ResetGadgetOverrides()
				select ((Component)x).GetComponent<SIGadgetBlaster>() into x
				where (Object)(object)x != (Object)null
				select x).GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					NxoSuperInfection.ResetNoBlasterCooldown(enumerator.Current);
				}
				while (enumerator.MoveNext());
			}
		}),
		new ButtonHandler.Button("Max Charge", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			using IEnumerator<SIGadgetChargeBlaster> enumerator = (from x in NxoSuperInfection.ResetGadgetOverrides()
				select ((Component)x).GetComponent<SIGadgetChargeBlaster>() into x
				where (Object)(object)x != (Object)null
				select x).GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					NxoSuperInfection.ResetMaxCharge(enumerator.Current);
				}
				while (enumerator.MoveNext());
			}
		}),
		new ButtonHandler.Button("Fast Charge", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			using IEnumerator<SIGadgetChargeBlaster> enumerator = (from x in NxoSuperInfection.ResetGadgetOverrides()
				select ((Component)x).GetComponent<SIGadgetChargeBlaster>() into x
				where (Object)(object)x != (Object)null
				select x).GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					NxoSuperInfection.ResetFastCharge(enumerator.Current);
				}
				while (enumerator.MoveNext());
			}
		}),
		new ButtonHandler.Button("No Cooldown (Dash)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			using IEnumerator<SIGadgetDashYoyo> enumerator = (from x in NxoSuperInfection.ResetGadgetOverrides()
				select ((Component)x).GetComponent<SIGadgetDashYoyo>() into x
				where (Object)(object)x != (Object)null
				select x).GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					NxoSuperInfection.ResetNoCooldown(enumerator.Current);
				}
				while (enumerator.MoveNext());
			}
		}),
		new ButtonHandler.Button("Clear Exclusion Zones", Category.Gamemode, isToggle: false, isActive: false, NxoSuperInfection.ClearExclusionZones),
		(PropHunt.PropHunt_Button_01 = new ButtonHandler.Button("Prop : " + PropHunt.UpdatePropSelectionLabel(), Category.Gamemode, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			PropHunt.CycleProp(forward: true);
		}, delegate
		{
			PropHunt.CycleProp(forward: false);
		})),
		new ButtonHandler.Button("Become Prop", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.BecomeProp();
		}),
		new ButtonHandler.Button("Skip Seeker Blindfold", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			PropHunt.SkipSeekerBlindfold();
		}),
		new ButtonHandler.Button("Hiders ESP", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			PropHunt.SetHidersESPEnabled(enable: true);
		}, delegate
		{
			PropHunt.SetHidersESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Seekers ESP", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			PropHunt.SetSeekersESPEnabled(enable: true);
		}, delegate
		{
			PropHunt.SetSeekersESPEnabled(enable: false);
		}),
		new ButtonHandler.Button("Prop Tag All", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			PropHunt.PropTagAll();
		}),
		new ButtonHandler.Button("Force Round Start (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.ForceRoundStart();
		}),
		new ButtonHandler.Button("Force Round End (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.ForceRoundEnd();
		}),
		new ButtonHandler.Button("Spam Gamemode (<color=red>M</color>)", Category.Gamemode, isToggle: true, isActive: false, delegate
		{
			PropHunt.SpamGamemode();
		}),
		new ButtonHandler.Button("Become Seeker (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.BecomeSeeker();
		}),
		new ButtonHandler.Button("Become Hider (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.BecomeHider();
		}),
		new ButtonHandler.Button("Collapse Boundary (<color=red>M</color>)", Category.Gamemode, isToggle: false, isActive: false, delegate
		{
			PropHunt.CollapseBoundary();
		}),
		new ButtonHandler.Button("Snowball Fling Gun", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.SnowballFlingGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Snowball Fling All", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.SnowballFlingAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Touch To Snowball Fling", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.TouchToSnowballFling();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Get Touched To Snowball Fling", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.GetTouchedToSnowballFling();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Shoot Big Snowballs (RG)", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.ShootBigSnowballs();
		}),
		new ButtonHandler.Button("Shoot Big Snowballs Gun", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.ShootBigSnowballsGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Touch to Snowball Effect", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.TouchToSnowballEffect();
		}),
		new ButtonHandler.Button("Get Touched to Snowball Effect", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.GetTouchedToSnowballEffect();
		}),
		new ButtonHandler.Button("Anti Knockback", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_13 = true;
		}, delegate
		{
			MenuPatches.GrabPatch_State_13 = false;
		}),
		new ButtonHandler.Button("Always Big Growing Snowballs", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			MenuPatches.GrabPatch_State_14 = true;
		}, delegate
		{
			MenuPatches.GrabPatch_State_14 = false;
		}),
		new ButtonHandler.Button("Snowball Effect Gun", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.SnowballEffectGun();
		}),
		new ButtonHandler.Button("Snowball Effect All (RT)", Category.Projectiles, isToggle: true, isActive: false, delegate
		{
			Projectile.SnowballEffectAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		(Fun.ServerSideEquipRoutine_StateMachine101_Button_01 = new ButtonHandler.Button(Fun.ServerSideEquipRoutine_StateMachine101_Text_01 ?? "", Category.Fun, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Fun.CycleSnowballEffectAll(forward: true);
		}, delegate
		{
			Fun.CycleSnowballEffectAll(forward: false);
		})),
		new ButtonHandler.Button("Add Cosmetic To Cart", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Fun.AddCosmeticToCart();
		}),
		new ButtonHandler.Button("Jet Pack (Networked)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			StevesPlayground.UpdateNetworkedEquipment(StevesPlayground.FuncType.Jetpack, add: true);
		}, delegate
		{
			StevesPlayground.UpdateNetworkedEquipment(StevesPlayground.FuncType.Jetpack, add: false);
		}),
		new ButtonHandler.Button("Grappling Hook (RT/LT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetGrapplingHookEnabled(active: true);
		}, delegate
		{
			Fun.SetGrapplingHookEnabled(active: false);
		}),
		new ButtonHandler.Button("Displacer Cannon (Networked)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			StevesPlayground.UpdateNetworkedEquipment(StevesPlayground.FuncType.DisplacerCannon, add: true);
		}, delegate
		{
			StevesPlayground.UpdateNetworkedEquipment(StevesPlayground.FuncType.DisplacerCannon, add: false);
		}),
		new ButtonHandler.Button("Web Shooters (RT/LT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetWebShootersEnabled(active: true);
		}, delegate
		{
			Fun.SetWebShootersEnabled(active: false);
		}),
		new ButtonHandler.Button("Place Bomb (RG/RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetPlaceBombEnabled(active: true);
		}, delegate
		{
			Fun.SetPlaceBombEnabled(active: false);
		}),
		new ButtonHandler.Button("Punch Mod", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.PunchMod();
		}),
		new ButtonHandler.Button("Draw Mod (RG/LG)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetDrawModEnabled(active: true);
		}, delegate
		{
			Fun.SetDrawModEnabled(active: false);
		}),
		new ButtonHandler.Button("Disable Rain", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetRainEnabled(setActive: false);
		}, delegate
		{
			Fun.SetRainEnabled(setActive: true);
		}),
		new ButtonHandler.Button("Rain Mode", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetRainEnabled(setActive: true);
		}, delegate
		{
			Fun.SetRainEnabled(setActive: false);
		}),
		new ButtonHandler.Button("Toggle Snow", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Visuals.SetToggleSnowEnabled(enable: true);
		}, delegate
		{
			Visuals.SetToggleSnowEnabled(enable: false);
		}),
		(Settings.CapturedVariables3760_Button_17 = new ButtonHandler.Button("Time Of Day : " + Settings.CapturedVariables3760_Text_13, Category.Fun, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleTimeOfDay(forward: true);
		}, delegate
		{
			Settings.CycleTimeOfDay(forward: false);
		})),
		new ButtonHandler.Button("Disable Wind Barriers", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetDisableWindBarriersEnabled(enable: true);
		}, delegate
		{
			Fun.SetDisableWindBarriersEnabled(enable: false);
		}),
		new ButtonHandler.Button("Max Quest Score", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Variables.Variables_Reference_09.offlineVRRig.SetQuestScore(int.MaxValue);
		}),
		new ButtonHandler.Button("Spam Bracelet (G)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SpamBracelet();
		}),
		new ButtonHandler.Button("Remove Bracelets", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Fun.RemoveBracelets();
		}),
		new ButtonHandler.Button("Loud Hand Taps", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.LoudHandTaps();
		}, delegate
		{
			Fun.RestoreHandTapAudio();
		}),
		new ButtonHandler.Button("Silent Hand Taps", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SilentHandTaps();
		}, delegate
		{
			Fun.RestoreHandTapAudio();
		}),
		new ButtonHandler.Button("Instant Hand Taps", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Variables.Variables_Reference_09.tapCoolDown = 0f;
		}, delegate
		{
			Variables.Variables_Reference_09.tapCoolDown = 0.33f;
		}),
		new ButtonHandler.Button("Mute Elevator", Category.Fun, isToggle: true, isActive: true, delegate
		{
			Fun.SetMuteElevatorEnabled(enable: false);
		}, delegate
		{
			Fun.SetMuteElevatorEnabled(enable: true);
		}),
		new ButtonHandler.Button("Solid Water", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.ConfigureWater(solid: true, transparent: false);
		}, delegate
		{
			Fun.ConfigureWater(solid: false, transparent: false);
		}),
		new ButtonHandler.Button("Disable Water", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.ConfigureWater(solid: false, transparent: true);
		}, delegate
		{
			Fun.ConfigureWater(solid: false, transparent: false);
		}),
		new ButtonHandler.Button("Air Swim", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SetAirSwimEnabled(active: true);
		}, delegate
		{
			Fun.SetAirSwimEnabled(active: false);
		}),
		new ButtonHandler.Button("Water Bender (RG/LG)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.WaterBender();
		}),
		new ButtonHandler.Button("Splash Gun", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SplashGun();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Water Barrage (RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.WaterBarrage();
		}),
		new ButtonHandler.Button("Splash Aura (RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SplashAura();
		}),
		new ButtonHandler.Button("Splash Self (RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SplashSelf();
		}),
		new ButtonHandler.Button("Grab Gliders (RG)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.GrabGliders();
		}),
		new ButtonHandler.Button("Orbit Gliders (RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.OrbitGliders();
		}),
		new ButtonHandler.Button("Spaz Gliders (RT)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SpazGliders();
		}),
		new ButtonHandler.Button("Glider Gun", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.GliderGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Glider Aura", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.GliderAura();
		}),
		new ButtonHandler.Button("Fast Gliders", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.ConfigureGliders(0.4f, 0.5f);
		}, delegate
		{
			Fun.ConfigureGliders(0.1f, 0.2f);
		}),
		new ButtonHandler.Button("Slowmo Gliders", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.ConfigureGliders(0.04f, 0.05f);
		}, delegate
		{
			Fun.ConfigureGliders(0.1f, 0.2f);
		}),
		new ButtonHandler.Button("Destroy Gliders", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Fun.DestroyGliders();
		}),
		new ButtonHandler.Button("Respawn Gliders", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Fun.RespawnGliders();
		}),
		new ButtonHandler.Button("Spawn Hoverboard", Category.Fun, isToggle: false, isActive: false, delegate
		{
			Fun.SpawnHoverboard();
		}),
		new ButtonHandler.Button("Shoot Hoverboards (RG)", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.ShootHoverboards();
		}),
		new ButtonHandler.Button("Orbit Hoverboards", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.OrbitHoverboards();
		}),
		new ButtonHandler.Button("Hoverboard Aura", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.HoverboardAura();
		}),
		new ButtonHandler.Button("Hoverboard Gun", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.HoverboardGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Spaz Hoverboard", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.SpazHoverboard();
		}),
		new ButtonHandler.Button("Rainbow Hoverboard", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.RainbowHoverboard();
		}),
		new ButtonHandler.Button("Strobe Hoverboard", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.StrobeHoverboard();
		}),
		new ButtonHandler.Button("Become Hoverboard", Category.Fun, isToggle: true, isActive: false, delegate
		{
			Fun.BecomeHoverboard();
		}, delegate
		{
			GTPlayer.Instance.SetHoverActive(false);
			VRRig.LocalRig.hoverboardVisual.SetNotHeld();
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Open Macros Folder", Category.Macros, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenFolder(Path.Combine(Variables.Variables_Text_01, "Macros"));
		}),
		new ButtonHandler.Button("Record Macro (A)", Category.Macros, isToggle: true, isActive: false, delegate
		{
			Macros.RecordMacro();
		}, delegate
		{
			Macros.StopRecordingMacro();
		}),
		new ButtonHandler.Button("Recorded Macros", Category.Macros, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Recorded_Macros);
		})
		{
			isCategory = true
		},
		(Nextbots.SpawnRoutine_StateMachine22_Button_01 = new ButtonHandler.Button("Nextbot Speed : " + Nextbots.SpawnRoutine_StateMachine22_Text_01, Category.Nextbots, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Nextbots.CycleNextbotSpeed(forward: true);
		}, delegate
		{
			Nextbots.CycleNextbotSpeed(forward: false);
		})),
		(Nextbots.SpawnRoutine_StateMachine22_Button_02 = new ButtonHandler.Button($"Behaviour : {Nextbots.SpawnRoutine_StateMachine22_Reference_01}", Category.Nextbots, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Nextbots.CycleNextbotSpeed(forward: true);
		}, delegate
		{
			Nextbots.CycleNextbotSpeed(forward: false);
		})),
		new ButtonHandler.Button("Custom Nextbots", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			CustomNextbots.CapturedVariables181_Text_01 = null;
			ButtonHandler.NavigateToCategory(Category.Custom_Nextbots);
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Crash On Jumpscare", Category.Nextbots, isToggle: true, isActive: false, delegate
		{
			Nextbots.SpawnRoutine_StateMachine22_State_01 = true;
		}, delegate
		{
			Nextbots.SpawnRoutine_StateMachine22_State_01 = false;
		}),
		new ButtonHandler.Button("Clear Nextbots", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.ClearNextbots();
		}),
		new ButtonHandler.Button("Sanic", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_sanic.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/sanic.mp3", 3.5f, 2f, "Sanic");
		}),
		new ButtonHandler.Button("Obunga", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_obunga.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/obunga.mp3", 3.5f, 2f, "Obunga");
		}),
		new ButtonHandler.Button("Angry Munci", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_angrymunci.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/angry%20munci.mp3", 3.5f, 2f, "Angry Munci");
		}),
		new ButtonHandler.Button("Selene Delgado", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_delgado.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/selene%20delgado.mp3", 3.5f, 2f, "Selene Delgado");
		}),
		new ButtonHandler.Button("Aheno", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_aheno.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/aheno.mp3", 3.5f, 2f, "Aheno");
		}),
		new ButtonHandler.Button("Yoshie", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_yoshie.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/yoshie.mp3", 3.5f, 2f, "Yoshie");
		}),
		new ButtonHandler.Button("This Man", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_thisman.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/this%20man.mp3", 3.5f, 2f, "This Man");
		}),
		new ButtonHandler.Button("IShowSpeed", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_speed.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/ishowspeed.mp3", 3.5f, 2f, "Speed");
		}),
		new ButtonHandler.Button("Jungler", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_jungler.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/jungler.mp3", 3.5f, 2f, "Jungler");
		}),
		new ButtonHandler.Button("PBJ Banana", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Gif_pbj.gif", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/peanutbutterjelly.mp3", 3.5f, 2f, "PBJ Banana");
		}),
		new ButtonHandler.Button("Imposter", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_impostor.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/imposter.mp3", 3.5f, 2f, "Imposter");
		}),
		new ButtonHandler.Button("Geoffrey", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_geoffery.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/geoffrey.mp3", 3.5f, 2f, "Geoffrey");
		}),
		new ButtonHandler.Button("The Boiled One", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_theboiledone.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/theboiledone.mp3", 3.5f, 2f, "The Boiled One");
		}),
		new ButtonHandler.Button("Afton", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_afton.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/afton.mp3", 3.5f, 2f, "Afton");
		}),
		new ButtonHandler.Button("Trollge", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Gif_trollge.gif", "https://us-tuna-sounds-files.voicemod.net/53ede07c-9315-4f31-aa65-1daeb19a3883-1655088334406.mp3", 3.5f, 2f, "Trollge", "https://us-tuna-sounds-files.voicemod.net/4bd5aaf2-4a7c-4730-b582-53b1ecdc5a2b-1776298464475.mp3");
		}),
		new ButtonHandler.Button("Glimpse", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_glimpse.png", "", 3.5f, 2f, "Glimpse", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/glimpsejumpscare.mp3");
		}),
		new ButtonHandler.Button("Happy", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_happy.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/happy.mp3", 3.5f, 2f, "Happy");
		}),
		new ButtonHandler.Button("Nerd Emoji", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_nerd.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/nerd.mp3", 3.5f, 2f, "Nerd Emoji");
		}),
		new ButtonHandler.Button("Firebrand", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_firebrand.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/firebrand.mp3", 3.5f, 2f, "Firebrand");
		}),
		new ButtonHandler.Button("Pinhead", Category.Nextbots, isToggle: false, isActive: false, delegate
		{
			Nextbots.SpawnNextbot("https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Images/Nextbot_pinhead.png", "https://raw.githubusercontent.com/NuggetGT/NXO-Resources/main/Nextbots/Nextbot%20Sounds/pinhead.mp3", 3.5f, 2f, "Pinhead");
		}),
		(Settings.CapturedVariables3760_Button_49 = new ButtonHandler.Button("Lag Type : " + Settings.CapturedVariables3760_Text_52, Category.Overpowered, isToggle: false, isActive: false, null, null, incremental: true, delegate
		{
			Settings.CycleLagType(forward: true);
		}, delegate
		{
			Settings.CycleLagType(forward: false);
		})),
		new ButtonHandler.Button("Lag All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.LagAll();
		}),
		new ButtonHandler.Button("Lag Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.LagGun();
		}),
		new ButtonHandler.Button("Touch To Lag", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TouchToLag();
		}),
		new ButtonHandler.Button("Get Touched To Lag", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GetTouchedToLag();
		}),
		new ButtonHandler.Button("Lag Aura", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.LagAura();
		}),
		new ButtonHandler.Button("Stump Kick To Specific Room", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (!SearchAndKeyboard.KeyCollider_State_02)
			{
				Overpowered.StumpKickToSpecificRoom();
			}
		}, delegate
		{
			Overpowered.ResetStumpKickToSpecificRoom();
		}),
		new ButtonHandler.Button("Fast Stump Kick", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Room.ForceCreateRoom_StateMachine11_State_01 = true;
		}, delegate
		{
			Room.ForceCreateRoom_StateMachine11_State_01 = false;
		}),
		new ButtonHandler.Button("Stump Kick Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.StumpKickGun();
		}),
		new ButtonHandler.Button("Stump Kick All", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			Overpowered.StumpKickAll();
		}),
		new ButtonHandler.Button("Destroy Player Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.DestroyPlayerGun();
		}),
		new ButtonHandler.Button("Destroy All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.DestroyAll();
		}),
		new ButtonHandler.Button("Deafen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.DeafenGun();
		}),
		new ButtonHandler.Button("Deafen All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.DeafenAll();
		}),
		new ButtonHandler.Button("Touch To Deafen", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TouchToDeafen();
		}),
		new ButtonHandler.Button("Get Touched To Deafen", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GetTouchedToDeafen();
		}),
		new ButtonHandler.Button("Earrape Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.EarrapeGun();
		}, delegate
		{
			Sound.SetSquareWaveMicrophoneEnabled(enable: false);
			Overpowered.StumpKickDelay_StateMachine68_State_01 = false;
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Earrape All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.EarrapeAll();
		}, delegate
		{
			Sound.SetSquareWaveMicrophoneEnabled(enable: false);
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}),
		new ButtonHandler.Button("Add Barrel To Cart", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			Fun.AddBarrelToCart();
		}),
		new ButtonHandler.Button("Barrel Fling Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.BarrelFlingGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Barrel Fling All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.BarrelFlingAll();
		}),
		new ButtonHandler.Button("Touch To Barrel Fling", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TouchToBarrelFling();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Get Touched To Barrel Fling", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GetTouchedToBarrelFling();
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Fling Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabFlingGun();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Fling All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabFlingAll();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Crash Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabCrashGun();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Crash All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabCrashAll();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Metro Crash Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabMetroCrashGun();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Metro Crash All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabMetroCrashAll();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Bring Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabBringGun();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Bring All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabBringAll();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Break Movement Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabBreakMovementGun();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Grab Break Movement All", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GrabBreakMovementAll();
		}, delegate
		{
			Overpowered.ResetGrabFlingGun();
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Splash Annoy Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.SplashAnnoyGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Splash Annoy All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.SplashAnnoyAll();
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}),
		new ButtonHandler.Button("Seizure Screen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (!(Time.time % 0.2f > 0.1f))
			{
				Fun.ApplyScreenEffectGun(Color.cyan);
			}
			else
			{
				Fun.ApplyScreenEffectGun(Color.red);
			}
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Seizure Screen All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (!(Time.time % 0.2f > 0.1f))
			{
				Fun.ApplyScreenEffectToAll(Color.cyan);
			}
			else
			{
				Fun.ApplyScreenEffectToAll(Color.red);
			}
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Strobe Screen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectGun(Variables.RandomColor());
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Strobe Screen All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectToAll(Variables.RandomColor());
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Flash Screen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (!(Time.time % 0.2f > 0.1f))
			{
				Fun.ApplyScreenEffectGun(Color.black);
			}
			else
			{
				Fun.ApplyScreenEffectGun(Color.white);
			}
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Flash Screen All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (!(Time.time % 0.2f > 0.1f))
			{
				Fun.ApplyScreenEffectToAll(Color.black);
			}
			else
			{
				Fun.ApplyScreenEffectToAll(Color.white);
			}
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Black Screen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectGun(Color.black);
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Black Screen All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectToAll(Color.black);
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("White Screen Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectGun(Color.white);
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("White Screen All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.ApplyScreenEffectToAll(Color.white);
		}, delegate
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			Player.SetLocalRigEnabled(rigStatus: true);
		}),
		new ButtonHandler.Button("Glider Annoy All (RT)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.GliderAnnoyAll();
		}),
		new ButtonHandler.Button("Glider Annoy Gun", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.GliderAnnoyGun();
		}, delegate
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}),
		new ButtonHandler.Button("Lock Room", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			Overpowered.LockRoom(status: false);
		}),
		new ButtonHandler.Button("Unlock Room", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			Overpowered.LockRoom(status: true);
		}),
		new ButtonHandler.Button("Try-On Cosmetics Anywhere", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.SetTryOnCosmeticsAnywhereEnabled(enable: true);
		}, delegate
		{
			Fun.SetTryOnCosmeticsAnywhereEnabled(enable: false);
		}),
		new ButtonHandler.Button("SS CosmetiX (WIP/City)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Fun.SetSSCosmetiXEnabled(enable: true);
		}, delegate
		{
			Fun.SetSSCosmetiXEnabled(enable: false);
		}),
		new ButtonHandler.Button("Enable Event (<color=red>M</color>)", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			if (Variables.IsMasterClient())
			{
				((GreyZoneManager)GreyZoneManager.Instance).ActivateGreyZoneAuthority();
			}
		}),
		new ButtonHandler.Button("Disable Event (<color=red>M</color>)", Category.Overpowered, isToggle: false, isActive: false, delegate
		{
			if (Variables.IsMasterClient())
			{
				((GreyZoneManager)GreyZoneManager.Instance).DeactivateGreyZoneAuthority();
			}
		}),
		new ButtonHandler.Button("Spam Event (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SpamEvent();
		}),
		new ButtonHandler.Button("Infinite Event (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			if (Variables.IsMasterClient())
			{
				ReflectionCompat.SetField(GreyZoneManager.Instance, "greyZoneActiveDuration", float.MaxValue);
			}
		}, delegate
		{
			if (Variables.IsMasterClient())
			{
				ReflectionCompat.SetField(GreyZoneManager.Instance, "greyZoneActiveDuration", 90f);
			}
		}),
		new ButtonHandler.Button("Slow All (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SlowAll();
		}),
		new ButtonHandler.Button("Slow Gun (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SlowGun();
		}),
		new ButtonHandler.Button("Slow Aura (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SlowAura();
		}),
		new ButtonHandler.Button("Slow On Touch (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SlowOnTouch();
		}),
		new ButtonHandler.Button("Vibrate All (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.VibrateAll();
		}),
		new ButtonHandler.Button("Vibrate Gun (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.VibrateGun();
		}),
		new ButtonHandler.Button("Vibrate Aura (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.VibrateAura();
		}),
		new ButtonHandler.Button("Vibrate On Touch (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.VibrateOnTouch();
		}),
		new ButtonHandler.Button("Mat Spam All (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.MatSpamAll();
		}),
		new ButtonHandler.Button("Mat Spam Gun (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.MatSpamGun();
		}),
		new ButtonHandler.Button("Touch To Mat Spam (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TouchToMatSpam();
		}),
		new ButtonHandler.Button("Get Touched To Mat Spam (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.GetTouchedToMatSpam();
		}),
		new ButtonHandler.Button("Spaz All Targets (<color=red>M</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.SpazAllTargets();
		}),
		new ButtonHandler.Button("Tagged Sound (<color=red>M/RT</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TaggedSound(0);
		}),
		new ButtonHandler.Button("Round End Sound (<color=red>M/RT</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TaggedSound(2);
		}),
		new ButtonHandler.Button("Bonk Sound (<color=red>M/RT</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TaggedSound(4);
		}),
		new ButtonHandler.Button("Count Sound (<color=red>M/RT</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TaggedSound(1);
		}),
		new ButtonHandler.Button("Brawl Sound (<color=red>M/RT</color>)", Category.Overpowered, isToggle: true, isActive: false, delegate
		{
			Overpowered.TaggedSound(7);
		}),
		new ButtonHandler.Button("Return", Category.Gear_Menu, isToggle: false, isActive: false, delegate
		{
			if (ButtonHandler.CapturedVariables570_Button_01 != null)
			{
				ButtonHandler.NavigateToCategory(ButtonHandler.CapturedVariables570_Button_01.Page);
			}
			else
			{
				ButtonHandler.NavigateToCategory(Category.Home);
			}
		})
		{
			isCategory = true
		},
		new ButtonHandler.Button("Tooltip", Category.Gear_Menu, isToggle: false, isActive: false, delegate
		{
			if (ButtonHandler.CapturedVariables570_Button_01?.tooltip != null)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Info, ButtonHandler.CapturedVariables570_Button_01.tooltip);
			}
		})
	};

	public static ButtonHandler.Button[] buttons
	{
		get
		{
			return ModButtons_Button_01;
		}
		set
		{
			ModButtons_Button_01 = value;
			Main.ShowTooltip();
		}
	}

	public ModButtons()
	{
	}
}
