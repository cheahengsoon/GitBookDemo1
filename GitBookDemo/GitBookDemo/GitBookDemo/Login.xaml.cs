using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GitBookDemo
{
    public partial class Login : ContentPage
    {
        private Member currentUser;
        private MemberManager memberManager;
        public Login()
        {
            InitializeComponent();

            memberManager = new MemberManager();

        }

        async void SignIn_Clicked(object sender, EventArgs e)
        {
            string username = this.txtusername.Text;
            string password = this.txtpassword.Text;

            var user = new Member { username = username, password = password };
            if(!string.IsNullOrEmpty(username)&&!string.IsNullOrEmpty(password))
            {
                Member userResponse = await memberManager.GetUserWhere(userSelect => userSelect.username == user.username && userSelect.password == user.password);

              

                if (userResponse != null && userResponse.username.Equals(username, StringComparison.Ordinal) && userResponse.password.Equals(password, StringComparison.Ordinal))
                {
                    Application.Current.Properties["user"] = userResponse;
                    Application.Current.MainPage = new NavigationPage(new ListMember());
                }
                else
                {
                    await DisplayAlert("Incorrect", "Your username or password is incorrect, please try again.", "Close");
                   
                }

            }
            else
            {
                await DisplayAlert("Incorrect", "The fields username or Password can't be empty, please insert valid values.", "Close");
             
            }
        }
    }
}