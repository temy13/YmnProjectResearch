using System;

using Xamarin.Forms;


namespace PresentationApp
{
    public class Account
    {
        private string username="";
        private string password="";
        private string image="";

        public string Username
        {
            set { this.username = value; }
            get { return this.username; }
        }

        public string Password
        {
            set { this.password = value; }
            get { return this.password; }
        }

        public string Image
        {
            set { this.image = value; }
            get { return this.image; }
        }
    }
}
