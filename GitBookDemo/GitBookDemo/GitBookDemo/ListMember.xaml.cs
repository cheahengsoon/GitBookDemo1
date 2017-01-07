using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GitBookDemo
{
    public partial class ListMember : ContentPage
    {
        private Member currentUser;
        private List<Member> memberList;
        private MemberManager memberManager;
        public ListMember()
        {
            InitializeComponent();

            currentUser = (Member)Application.Current.Properties["user"];

            memberList = new List<Member>();
            memberManager = new MemberManager();

            memberListView.ItemTemplate = new DataTemplate(typeof(MemberCell));

            LoadMember();

        }

        private async void LoadMember()
        {
            memberList = await memberManager.ListUserWhere(userSelect => userSelect.membername != currentUser.membername);
            if(memberList.Count!=0)
            {
                memberListView.ItemsSource = memberList;
            }
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                memberListView.ItemsSource=
                    memberList.Where(
                      userSelect=>userSelect.membername.ToLower().Contains(e.NewTextValue.ToLower()));
            }
            else
            {
                memberListView.ItemsSource = memberList;
            }
        }
    }
}
