using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GitBookDemo
{
    public partial class AddMember : ContentPage
    {
        private MemberManager manager;
        public AddMember()
        {
            InitializeComponent();

            manager = new MemberManager();
        }

        async Task AddNewMember(Member member)
        {
            Member userResponse = await manager.SaveGetUserAsync(member);
            Application.Current.Properties["user"] = userResponse;

        }

        public async void OnAdd(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
           
         if(!string.IsNullOrEmpty(username)&&!!string.IsNullOrEmpty(password))
            {
                var member = new Member
                {
                    username = username,
                    password = password
                   
                };
                await AddNewMember(member);

                await Navigation.PushModalAsync(new MemberProfile());
                await Navigation.PopAsync();
             
            }
         
        }
    }
}
