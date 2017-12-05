using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework2
{
    /// <summary>
    /// Interaction logic for FriendsAndFamily.xaml
    /// </summary>
    public partial class FriendsAndFamily : Window
    {
        // create internal Eater object to pass eater from previous window
        internal Eater passedEater = new Eater();

        // initialise cooks and eaters lists
        internal List<Cook> cooksList = new List<Cook>();
        internal List<Eater> eatersList = new List<Eater>();

        // make internal FriendsAndFamily Object to store and pass friends and family details
        internal FriendsAndFamilyDetails newFnF = new FriendsAndFamilyDetails();

        public FriendsAndFamily()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            newFnF.name  = txtbxName.Text;
            newFnF.email = txtbxEmail.Text;
            newFnF.phone = txtbxPhone.Text;

            // add the new friends and family details to the Eater
            passedEater.friendsAndFamily = newFnF;

            // add the eater to the list and pass it on
            if (!eatersList.Contains(passedEater))
            {
                eatersList.Add(passedEater);
            }

            MainSystemScreen mainScrn = new MainSystemScreen();

            mainScrn.cooksList = cooksList;
            mainScrn.eatersList = eatersList;

            // initialise main screen elements
            mainScrn.lblUsrName.Content = passedEater.firstName + " " + passedEater.lastName;
            mainScrn.btnSecurityChks.Visibility = Visibility.Hidden;
            mainScrn.btnViewEaters.Visibility = Visibility.Hidden;

            // if this window was opened by the main system screen
            // and not from the new user sign up then just close this
            // otherwise close this, the window that created it and
            // open a new main screen window
            if (App.Current.Windows[0].Title == "Meal Sharers")
            {
                this.Close();
            }
            else
            {
                mainScrn.passedEater = passedEater;
                mainScrn.Show();
                this.Close();
                App.Current.Windows[0].Close();
            }
        }
    }
}
