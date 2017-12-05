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
    /// Interaction logic for MainSystemScreen.xaml
    /// </summary>
    public partial class MainSystemScreen : Window
    {
        // initialised passed cooks and eaters
        internal Cook passedCook = new Cook();
        internal Eater passedEater = new Eater();
        
        //initialise cooks and eaters lists
        internal List<Cook> cooksList = new List<Cook>();
        internal List<Eater> eatersList = new List<Eater>();

        string eaterSelection;

        public MainSystemScreen()
        {
            InitializeComponent();
            lstviewEaters.Visibility = Visibility.Hidden;
            btnSlctEater.Visibility = Visibility.Hidden;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToLogIn = new MainWindow();

            // pass the current cooks and eaters lists
            backToLogIn.cooksList = cooksList;
            backToLogIn.eatersList = eatersList;

            backToLogIn.Show();
            this.Close();
        }

        private void btnSecurityChks_Click(object sender, RoutedEventArgs e)
        {
            SecurityChecksPrompt checksWin = new SecurityChecksPrompt();

            // pass cook
            checksWin.passedCook = passedCook;
            
            // display stored files if any
            checksWin.lblFilePVG.Content = checksWin.passedCook.pvg.scan;
            checksWin.lblFileFoodCert.Content = checksWin.passedCook.foodHygiene.scan;
            
            checksWin.ShowDialog();

            // update the status text if it has changed in the next window
            lblPVGStat.Content = checksWin.pvgStatusStr;
            lblCertStat.Content = checksWin.foodCertStatusStr;
        }

        private void btnViewEaters_Click(object sender, RoutedEventArgs e)
        {
            lstviewEaters.Visibility = Visibility.Visible;
            lstviewEaters.ItemsSource = eatersList;
        }

        private void lstviewEaters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSlctEater.Visibility = Visibility.Visible;
            eaterSelection = lstviewEaters.SelectedItem.ToString();
        }

        private void btnFriendsAndFamily_Click(object sender, RoutedEventArgs e)
        {
            FriendsAndFamily fnfWindow = new FriendsAndFamily();

            // pass eater
            fnfWindow.passedEater = passedEater;

            // display stored details if any
            fnfWindow.txtbxName.Text = passedEater.friendsAndFamily.name;
            fnfWindow.txtbxEmail.Text = passedEater.friendsAndFamily.email;
            fnfWindow.txtbxPhone.Text = passedEater.friendsAndFamily.phone;

            fnfWindow.ShowDialog();

        }

        private void btnSlctEater_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Share a meal with eater\n" + eaterSelection + " ?");
        }

        private void btnViewMealShares_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("There are no meal shares to display.\nCome back later");
        }
    }
}
