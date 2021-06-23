﻿using Leagueoflegends.Data.Main;
using Leagueoflegends.ExampleData.Friends;
using Leagueoflegends.Home.General.ViewModels;
using Leagueoflegends.Home.General.Views;
using Leagueoflegends.Main.Local.Collection;
using Leagueoflegends.Main.Local.Work;
using Leagueoflegends.Main.UI.Units;
using Leagueoflegends.MyShop.ViewModels;
using Leagueoflegends.MyShop.Views;
using Leagueoflegends.TeamFight.ViewModels;
using Leagueoflegends.TeamFight.Views;
using Leagueoflegends.Windowbase.Mvvm;
using Leagueoflegends.Windowbase.Riotcore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Leagueoflegends.Main.Local.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		#region Variables

		private readonly WindowWork _winWork;
		private readonly ModalWork _modalWork;

		private object _modalContent;
		private IRiotUI _currentUI;
		private List<SubMenuModel> _subMenus;
		private SubMenuModel _currentSubMenu;
		private MainMenuModel _mainMenu;
		private FriendsCollection _friends;

		private Dictionary<string, IRiotUI> UIs { get; set; }
		#endregion

		#region ViewModels

		public MenuWork MainMenu { get; }
		public FriendsSortWork Options { get; }
		#endregion

		#region ICommands

		public ICommand ModalCommand { get; }
		public ICommand CloseCommand { get; }
		public ICommand MinimizeCommand { get; }
		#endregion

		#region ModalContent

		public object ModalContent
		{
			get => _modalContent;
			set { _modalContent = value; OnPropertyChanged(); }
		}
		#endregion

		#region CurrentUI

		public IRiotUI CurrentUI
		{
			get => _currentUI;
			set { _currentUI = value; OnPropertyChanged(); }
		}
		#endregion

		#region SubMenus

		public SubMenuModel CurrentSubMenu
		{
			get => _currentSubMenu;
			set { _currentSubMenu = value; OnPropertyChanged(); SubMenuChanged(value); }
		}

		public List<SubMenuModel> SubMenus
		{
			get => _subMenus;
			set { _subMenus = value; OnPropertyChanged(); }
		}
		#endregion

		#region Friends

		public FriendsCollection Friends
		{
			get => _friends;
			set { _friends = value; OnPropertyChanged(); }
		}
		#endregion

		#region Constructor

		public MainViewModel()
		{
			_winWork = new(this);
			_modalWork = new(this);

			UIs = new();

			CloseCommand = new RelayCommand<object>(_winWork.DoClosing);
			MinimizeCommand = new RelayCommand<object>(_winWork.DoMinizing);
			ModalCommand = new RelayCommand<Type>(_modalWork.SwitchModal);

			MainMenu = new(MenuSelected);
			Options = new();
			Friends = new(ExamFriends.GetFriendsList());
		}
		#endregion

		// Private

		#region MenuSelected

		private void MenuSelected(MainMenuModel menu, List<SubMenuModel> subMenus)
		{
			_mainMenu = menu;
			SubMenus = subMenus;
			CurrentSubMenu = SubMenus.FirstOrDefault();
		}
		#endregion

		#region SubMenuChanged

		private void SubMenuChanged(SubMenuModel value)
		{
			IRiotUI content;
			string key;

			if (value != null)
			{
				key = value.Name;
				content = value.Seq switch
				{
					0 => new GeneralView().SetVM(new GeneralViewModel()),
					_ => new EmptyContent()
				};
			}
			else
			{
				key = _mainMenu.Name;
				content = _mainMenu.Seq switch
				{
					1 => new TeamFightView().SetVM(new TeamFightViewModel()),
					6 => new MyShopView().SetVM(new MyShopViewModel()),
					_ => new EmptyContent()
				};
			}

			if (!UIs.ContainsKey(key))
			{
				UIs.Add(key, content);
			}

			CurrentUI = UIs[key];
		}
		#endregion
	}
}