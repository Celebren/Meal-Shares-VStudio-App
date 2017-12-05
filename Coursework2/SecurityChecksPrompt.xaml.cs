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
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;


namespace Coursework2
{
    /// <summary>
    /// Interaction logic for SecurityChecksPrompt.xaml
    /// </summary>
    public partial class SecurityChecksPrompt : Window
    {
        // create internal Cook object to pass cook from previous window
        internal Cook passedCook = new Cook();

        // initialise cooks and eaters lists
        internal List<Cook> cooksList = new List<Cook>();
        internal List<Eater> eatersList = new List<Eater>();

        // create internal objects to store and pass PVG and Food Hygiene certificate data
        internal PVG newPVG = new PVG();
        internal FoodHygieneCertificate newFHCert = new FoodHygieneCertificate();

        // public strings to be read by main system screen when calling this window
        public string pvgStatusStr = "";
        public string foodCertStatusStr = "";

        public SecurityChecksPrompt()
        {
            InitializeComponent();
        }

        private void btnBrowsePVG_Click(object sender, RoutedEventArgs e)
        {
            // open a file browser dialog and display selected file title in label
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                lblFilePVG.Content = openFileDialog.SafeFileName;

                // add file to PVG object and add PVG to Cook
                newPVG.scan = openFileDialog.SafeFileName;
                passedCook.pvg = newPVG;
            }
        }

        private void btnBrowseFoodCert_Click(object sender, RoutedEventArgs e)
        {
            // open a file browser dialog and display selected file title in label
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                lblFileFoodCert.Content = openFileDialog.SafeFileName;

                // add file to FooHygieneCertificate object and add FoodHygieneCertificate to Cook
                newFHCert.scan = openFileDialog.SafeFileName;
                passedCook.foodHygiene = newFHCert;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {   
            this.Close();
        }

        private void btnPVG_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("When fully implemented, this will link to the appropriate website");
        }

        private void btnCert_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("When fully implemented, this will link to the appropriate website");
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {   
            // if this window was opened by the main system screen
            // and not from the new user sign up then just close this
            // otherwise close this, the window that created it and
            // open a new main screen window
            if (App.Current.Windows[0].Title == "Meal Sharers")
            {
                if (lblFileFoodCert.Content.ToString() == "")
                {                    
                    foodCertStatusStr = "Pending";
                }
                else
                {
                    foodCertStatusStr = "OK!";
                }

                if (string.IsNullOrEmpty(lblFilePVG.Content.ToString()))
                {
                    pvgStatusStr = "Pending";
                }
                else
                {
                    pvgStatusStr = "OK!";
                }

                this.Close();
            } else
            {
                MainSystemScreen mainScrn = new MainSystemScreen();

                mainScrn.lblUsrName.Content = passedCook.firstName + " " + passedCook.lastName;
                mainScrn.btnFriendsAndFamily.Visibility = Visibility.Hidden;

                // pass the current cook
                mainScrn.passedCook = passedCook;

                // pass the current cooks and eaters lists
                mainScrn.cooksList = cooksList;
                mainScrn.eatersList = eatersList;

                if (lblFileFoodCert.Content.ToString() == "")
                {
                    mainScrn.lblCertStat.Content = "Pending";
                }
                else
                {
                    mainScrn.lblCertStat.Content = "OK!";
                }

                if (string.IsNullOrEmpty(lblFilePVG.Content.ToString()))
                {
                    mainScrn.lblPVGStat.Content = "Pending";
                }
                else
                {
                    mainScrn.lblCertStat.Content = "OK!";
                }

                // add the current cook to the list if it's not already in it
                if (!cooksList.Contains(passedCook))
                {
                    cooksList.Add(passedCook);
                }
                
                mainScrn.Show();
                this.Close();
                App.Current.Windows[0].Close();
            }            
        }
    }
}
