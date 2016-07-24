using System;

using Xamarin.Forms;


namespace PresentationApp
{
    public class Account
    {
        private string id="";
        private string password="";
        private string image="";

        public string ID
        {
            set { this.id = value; }
            get { return this.id; }
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
