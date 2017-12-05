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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // initialise sample cooks and eaters 
        internal Cook sampleCook1 = new Cook();
        internal Eater sampleEater1 = new Eater();
        internal Eater sampleEater2 = new Eater();

        internal Address sampleAddress = new Address();
        internal PVG newPVG = new PVG();
        internal FoodHygieneCertificate newFoodCert = new FoodHygieneCertificate();

        internal FriendsAndFamilyDetails newFnf = new FriendsAndFamilyDetails();

        // initialise cooks and eaters lists
        internal List<Cook> cooksList = new List<Cook>();
        internal List<Eater> eatersList = new List<Eater>();


        public MainWindow()
        {
            InitializeComponent();

            // when application starts initialise sample objects
            sampleAddress.firstLine = "";
            sampleAddress.secondLine = "";
            sampleAddress.city = "Edinburgh";
            sampleAddress.postCode = "EH16 4GZ";

            newPVG.scan = "";
            newPVG.status = "";

            newFoodCert.scan = "";

            newFnf.name = "Rena Silver";
            newFnf.email = "rena@email.com";
            newFnf.phone = "+40 1234 56789";

            sampleCook1.firstName = "Rena";
            sampleCook1.lastName = "Silver";
            sampleCook1.email = "rena@email.com";
            sampleCook1.password = "rena";
            sampleCook1.pvg = newPVG;
            sampleCook1.foodHygiene = newFoodCert;

            sampleEater1.firstName = "Katie";
            sampleEater1.lastName = "Baker";
            sampleEater1.email = "katie@email.com";
            sampleEater1.password = "katie";
            sampleEater1.foodPreferences = "Freegan";
            sampleEater1.address = sampleAddress;
            sampleEater1.friendsAndFamily = newFnf;

            sampleEater2.firstName = "Aristotle";
            sampleEater2.lastName = "The Axolotl";
            sampleEater2.email = "aristotle@email.com";
            sampleEater2.password = "aristotle";
            sampleEater2.foodPreferences = "Worms!!!";
            sampleEater2.address = sampleAddress;
            sampleEater2.friendsAndFamily = newFnf;

            // add objects to lists
            cooksList.Add(sampleCook1);
            eatersList.Add(sampleEater1);
            eatersList.Add(sampleEater2);
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            // pressing the sign up button shows the sign up window and closes this window
            NewUserSignUp signUpWindow = new NewUserSignUp();
            signUpWindow.cooksList = cooksList;
            signUpWindow.eatersList = eatersList;
            signUpWindow.Show();
            this.Close();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            

            // check if given password and email match the ones saved in the lists
            for (var i = 0; i < cooksList.Count; i++)
            {
                if (cooksList[i].email == txtBoxEmail.Text && cooksList[i].password == passwordBox.Password.ToString())
                {
                    MainSystemScreen mainScrn = new MainSystemScreen();

                    // pass on the lists
                    mainScrn.cooksList = cooksList;
                    mainScrn.eatersList = eatersList;

                    // pass loged in cook
                    mainScrn.passedCook = cooksList[i];
                    // mainScrn.passedCook.pvg = newPVG;

                    // initialise main screen elements
                    mainScrn.lblUsrName.Content = cooksList[i].firstName + " " + cooksList[i].lastName;
                    mainScrn.btnFriendsAndFamily.Visibility = Visibility.Hidden;

                    // open next window
                    mainScrn.Show();
                    this.Close();
                    break;
                }
            }
            for (var i = 0; i < eatersList.Count; i++)
            {
                if (eatersList[i].email == txtBoxEmail.Text && eatersList[i].password == passwordBox.Password.ToString())
                {
                    MainSystemScreen mainScrn = new MainSystemScreen();

                    // pass on the lists
                    mainScrn.cooksList = cooksList;
                    mainScrn.eatersList = eatersList;

                    // pass logged in eater
                    mainScrn.passedEater = eatersList[i];

                    // initialise main screen elements
                    mainScrn.lblUsrName.Content = eatersList[i].firstName + " " + eatersList[i].lastName;
                    mainScrn.btnSecurityChks.Visibility = Visibility.Hidden;
                    mainScrn.btnViewEaters.Visibility = Visibility.Hidden;
                    mainScrn.lblChckStatus.Visibility = Visibility.Hidden;
                    mainScrn.lblCertStat.Visibility = Visibility.Hidden;
                    mainScrn.lblPVGStat.Visibility = Visibility.Hidden;

                    // open next window
                    mainScrn.Show();
                    this.Close();
                    break;
                }
            }
            
        }
    }
}
