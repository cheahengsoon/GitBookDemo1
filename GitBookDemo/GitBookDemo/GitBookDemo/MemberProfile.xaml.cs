using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GitBookDemo
{
    public partial class MemberProfile : ContentPage
    {
        private Member currentUser;
        MemberManager manager;
        public MemberProfile()
        {
            InitializeComponent();

            manager = new MemberManager();
            currentUser = (Member)Application.Current.Properties["user"];

            loadData();
        }

         void loadData()
        {
          if(currentUser!=null)
            {
                if(!string.IsNullOrEmpty(currentUser.membername))
                {
                    txtMemberName.Text = currentUser.membername;
                }
            }
        }

        async Task UpdateUser(Member member)
        {
            Member userResponse = await manager.SaveGetUserAsync(member);
            Application.Current.Properties["user"] = userResponse;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            string membername = this.txtMemberName.Text;
            if(string.IsNullOrEmpty(membername))
            {
                await DisplayAlert("Error", "Fill blank fields", "Accept");
            }
            else
            {
                var member = new Member
                {
                    ID=currentUser.ID,
                    username=currentUser.username,
                    password=currentUser.password,
                    membername=membername
                };
                await UpdateUser(member);
                await DisplayAlert("Error", "Success Update", "Accept");
            }
        }
    }
}
