using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework2
{
    /// <summary>
    /// Interaction logic for NewUserSignUp.xaml
    /// </summary>
    public partial class NewUserSignUp : Window
    {
        // make objects internal to be able to receive data from
        // other windows.
        internal Cook newCook = new Cook();
        internal Eater newEater = new Eater();
        internal Address newAddress = new Address();
        internal PVG newPVG = new PVG();
        internal FoodHygieneCertificate newFoodCert = new FoodHygieneCertificate();
        internal FriendsAndFamilyDetails newFnf = new FriendsAndFamilyDetails();

        // initialise cooks and eaters lists
        internal List<Cook> cooksList = new List<Cook>();
        internal List<Eater> eatersList = new List<Eater>();

        private string foodSelection { get; set; }
        private string travelSelection { get; set; }

        
        public NewUserSignUp()
        {
            InitializeComponent();
            
            // In design time the window is expanded to allow working with the additional
            // sign up forms. This sets the width of the window to contain just the visible
            // elements
            signUpScreen.Width = 415;

            // hide forms that are not needed yet
            lblFoodPrefs.Visibility = Visibility.Hidden;
            chckbx1.Visibility = Visibility.Hidden;
            chckbx2.Visibility = Visibility.Hidden;
            chckbx3.Visibility = Visibility.Hidden;
            txtbxOther.Visibility = Visibility.Hidden;

            lblTransport.Visibility = Visibility.Hidden;
            radBttn1.Visibility = Visibility.Hidden;
            radBttn2.Visibility = Visibility.Hidden;
            radBttn3.Visibility = Visibility.Hidden;
            radBttn4.Visibility = Visibility.Hidden;

            btnCookNext.Visibility = Visibility.Hidden;
            btnEaterNext.Visibility = Visibility.Hidden;
        }

        private void combobxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // take the selected value and add it to a variable
            string selection = combobxUser.SelectedValue.ToString();

            // expand the width of the window to fit the new forms
            signUpScreen.Width = 814;
                        
            if (selection == "Cook")
            {
                // make cook's forms visible
                lblFoodPrefs.Visibility = Visibility.Visible;
                chckbx1.Visibility = Visibility.Visible;
                chckbx2.Visibility = Visibility.Visible;
                chckbx3.Visibility = Visibility.Visible;
                txtbxOther.Visibility = Visibility.Visible;

                lblTransport.Visibility = Visibility.Visible;
                radBttn1.Visibility = Visibility.Visible;
                radBttn2.Visibility = Visibility.Visible;
                radBttn3.Visibility = Visibility.Visible;
                radBttn4.Visibility = Visibility.Visible;

                btnCookNext.Visibility = Visibility.Visible;

                // hide eater's next button
                btnEaterNext.Visibility = Visibility.Hidden;

            } else if (selection == "Eater")
            {
                // make eater's forms visible
                lblFoodPrefs.Visibility = Visibility.Visible;
                chckbx1.Visibility = Visibility.Visible;
                chckbx2.Visibility = Visibility.Visible;
                chckbx3.Visibility = Visibility.Visible;
                txtbxOther.Visibility = Visibility.Visible;

                // hide cook's forms if they are visible
                lblTransport.Visibility = Visibility.Hidden;
                radBttn1.Visibility = Visibility.Hidden;
                radBttn2.Visibility = Visibility.Hidden;
                radBttn3.Visibility = Visibility.Hidden;
                radBttn4.Visibility = Visibility.Hidden;

                // hide cook's next button
                btnCookNext.Visibility = Visibility.Hidden;
                btnEaterNext.Visibility = Visibility.Visible;

            }
        }

        private void btnCookNext_Click(object sender, RoutedEventArgs e)
        {
            // check if all required fields are filled
            if (string.IsNullOrEmpty(txtbxFirstName.Text) ||
                string.IsNullOrEmpty(txtbxLastName.Text) ||
                string.IsNullOrEmpty(txtbxEmail.Text) ||
                string.IsNullOrEmpty(txtbxAddressLn1.Text) ||
                string.IsNullOrEmpty(txtbxCity.Text) ||
                string.IsNullOrEmpty(txtbxPostCode.Text) ||
                string.IsNullOrEmpty(pswrdbx1.Password.ToString()) ||
                string.IsNullOrEmpty(foodSelection) ||
                string.IsNullOrEmpty(travelSelection))
            {
                MessageBox.Show("Please fill in all the required fields that are marked with an asterisk (*)");
            } else
            {
                // check if passwords match
                if (pswrdbx1.Password.ToString() == pswrdbx2.Password.ToString())
                {
                    // when the cook's next button is pressed this takes the completed
                    // form details and adds them to a cook object. The object is then added
                    // to the list of cooks.
                    newCook.firstName = txtbxFirstName.Text;
                    newCook.lastName = txtbxLastName.Text;
                    newCook.email = txtbxEmail.Text;

                    newAddress.firstLine = txtbxAddressLn1.Text;
                    newAddress.secondLine = txtbxAddressLn2.Text;
                    newAddress.postCode = txtbxPostCode.Text;
                    newAddress.city = txtbxCity.Text;

                    newCook.address = newAddress;
                    if (foodSelection == "Please Fill")
                    {
                        foodSelection = txtbxOther.Text;
                    }
                    newCook.cookPreferences = foodSelection;

                    newCook.travelMethod = travelSelection;
                    newCook.password = pswrdbx1.Password.ToString();

                    // initialise food certificate and pvg to avoid bugs
                    newPVG.scan = "";
                    newPVG.status = "";
                    newFoodCert.scan = "";
                    newCook.pvg = newPVG;
                    newCook.foodHygiene = newFoodCert;

                    SecurityChecksPrompt checksWin = new SecurityChecksPrompt();

                    // pass newCook to the new window
                    checksWin.passedCook = newCook;

                    // pass the current cook and eater list
                    checksWin.cooksList = cooksList;
                    checksWin.eatersList = eatersList;

                    // open the next window
                    checksWin.ShowDialog();
                }
                else
                {
                    // display pop-up if passwords don't match
                    MessageBox.Show("Passwords don't match. Please make sure you enter the same password on both firelds");
                }
            }   
        }

        private void food_Checked(object sender, RoutedEventArgs e)
        {
            // checked radio button returns it's content as a string

            // if the user selects the Other Option, pass on the text in
            // the text box. Otherwise pass on the text on the radio button
            if ((string)(sender as RadioButton).Content == "Other")
            {
                foodSelection = txtbxOther.Text; // Returns default value. This is updated later on when the next button is pressed             
            } else
            {
                foodSelection = (string)(sender as RadioButton).Content;
            }
            
        }

        private void travel_Checked(object sender, RoutedEventArgs e)
        {
            // checked radio button returns it's content as a string
            travelSelection = (string)(sender as RadioButton).Content;            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToLogInWin = new MainWindow();
            backToLogInWin.Show();
            this.Close();
        }

        private void btnEaterNext_Click(object sender, RoutedEventArgs e)
        {
            // check if all required fields are filled
            if (string.IsNullOrEmpty(txtbxFirstName.Text) ||
                string.IsNullOrEmpty(txtbxLastName.Text) ||
                string.IsNullOrEmpty(txtbxEmail.Text) ||
                string.IsNullOrEmpty(txtbxAddressLn1.Text) ||
                string.IsNullOrEmpty(txtbxCity.Text) ||
                string.IsNullOrEmpty(txtbxPostCode.Text) ||
                string.IsNullOrEmpty(pswrdbx1.Password.ToString()) ||
                string.IsNullOrEmpty(foodSelection) ||
                foodSelection == "Please Fill") 
            {
                MessageBox.Show("Please fill in all the required fields that are marked with an asterisk (*)");
            }
            else
            {
                // check if passwords match
                if (pswrdbx1.Password.ToString() == pswrdbx2.Password.ToString())
                {
                    // pass the Eater details to Eater Object and open next Eater window
                    // when the eater's's next button is pressed this takes the completed
                    // form details and adds them to an eater object. The object is then added
                    // to the list of cooks.
                    newEater.firstName = txtbxFirstName.Text;
                    newEater.lastName = txtbxLastName.Text;
                    newEater.email = txtbxEmail.Text;

                    newAddress.firstLine = txtbxAddressLn1.Text;
                    newAddress.secondLine = txtbxAddressLn2.Text;
                    newAddress.postCode = txtbxPostCode.Text;
                    newAddress.city = txtbxCity.Text;

                    newEater.address = newAddress;
                    if (foodSelection == "Please Fill")
                    {
                        foodSelection = txtbxOther.Text;
                    }
                    newEater.foodPreferences = foodSelection;

                    newEater.password = pswrdbx1.Password.ToString();

                    // initialise friends and family details to avoid bugs
                    newFnf.name = "";
                    newFnf.email = "";
                    newFnf.phone = "";
                    newEater.friendsAndFamily = newFnf;

                    FriendsAndFamily fnfWindow = new FriendsAndFamily();

                    // pass newEater to the new window
                    fnfWindow.passedEater = newEater;

                    // pass the current cook and eater list
                    fnfWindow.cooksList = cooksList;
                    fnfWindow.eatersList = eatersList;

                    // open the next window and close this one            
                    fnfWindow.ShowDialog();
                }
                else
                {
                    // display pop-up if passwords don't match
                    MessageBox.Show("Passwords don't match. Please make sure you enter the same password on both firelds");
                }
            }
        }
    }
}
