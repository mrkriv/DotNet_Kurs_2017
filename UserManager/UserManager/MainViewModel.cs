using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UserManager
{
    public class MainViewModel : DependencyObject
    {
        public ICollectionView UserList
        {
            get { return (ICollectionView)GetValue(UserListProperty); }
            set { SetValue(UserListProperty, value); }
        }

        public static readonly DependencyProperty UserListProperty =
            DependencyProperty.Register(
                "UserList", typeof(ICollectionView),
                typeof(MainViewModel), new PropertyMetadata(null));



        public string FilterString
        {
            get { return (string)GetValue(FilterStringProperty); }
            set { SetValue(FilterStringProperty, value); }
        }

        public static readonly DependencyProperty FilterStringProperty =
            DependencyProperty.Register(
                "FilterString", typeof(string),
                typeof(MainViewModel), new PropertyMetadata("", OnFiltreStringChange));

        public static void OnFiltreStringChange(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            ((MainViewModel)d).UserList.Refresh();
        }

        public SimpleCommand CreateUserCommand { get; set; }
        public SimpleCommand LoadUsersCommand { get; set; }

        public void CreateUser()
        {
            var obs_ls = UserList.SourceCollection as ObservableCollection<User>;
            obs_ls.Add(new User() { Name = "New User" });
        }

        public void LoadUsers()
        {
            var ls = new ObservableCollection<User>(User.GetUsers());
            UserList = CollectionViewSource.GetDefaultView(ls);
            UserList.Filter += FiltreUser;
        }

        public MainViewModel()
        {
            var ls = new ObservableCollection<User>();
            UserList = CollectionViewSource.GetDefaultView(ls);
            UserList.Filter += FiltreUser;

            CreateUserCommand = new SimpleCommand(CreateUser);
            LoadUsersCommand = new SimpleCommand(LoadUsers);
        }

        private bool FiltreUser(object obj)
        {
            var user = obj as User;
            
            if(user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(FilterString) || user.Name.Contains(FilterString))
            {
                return true;
            }

            return false;
        }
    }
}