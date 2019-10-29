using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Student_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Using the file reader functions from System.IO, this places all the information from text files into arrays.
        private readonly string[] countries = System.IO.File.ReadAllLines("countries.txt");
        private readonly string[] courses = System.IO.File.ReadAllLines("courses.txt");

        public MainWindow()
        {
            InitializeComponent();
            AddCountries();
            AddCourses();

            cboCountry.Visibility = Visibility.Hidden;
            lblCountry.Visibility = Visibility.Hidden;
        }

        // This method is responsible for adding all the countries to the countries drop-down menu.
        private void AddCountries()
        {
            foreach(string c in countries)
            {
                cboCountry.Items.Add(c);
            }
        }

        // This method is responsible for adding all the courses to the courses drop-down menu.
        private void AddCourses()
        {
            foreach(string c in courses)
            {
                cboCourse.Items.Add(c);
            }
        }

        // This function validates the e-mail using RegEx
        private bool ValidateEmail(string address)
        {
            // Sources: https://stackoverflow.com/a/17513022/11244896
            string regex = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$";

            if (Regex.IsMatch(address, regex))
            {
                // Checks if any characters are left over aftr being replaced. 
                if (Regex.Replace(address, regex, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        // This method valudates all the text boxes and combo boxes.
        private bool ValidateText()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtForename.Text)
                    || string.IsNullOrWhiteSpace(txtSurname.Text)
                    || string.IsNullOrWhiteSpace(txtAddress1.Text)
                    || string.IsNullOrWhiteSpace(txtCity.Text)
                    || string.IsNullOrWhiteSpace(txtPostcode.Text)
                    || string.IsNullOrWhiteSpace(cboCourse.Text)
                    || (chkInterStudent.IsChecked == true && string.IsNullOrWhiteSpace(cboCountry.Text)))
                {
                    throw new Exception("Fields marked \"*\" cannot be empty!");

                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // This method validates the age.
        private bool ValidateAge()
        {
            try
            {

                if (int.Parse(txtAge.Text) < 16
                    || int.Parse(txtAge.Text) > 101)
                {
                    throw new Exception("Age cannot be outwith range! {16 < n < 101}");
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        
        // This event handler handles when the clear button is clicked.
        // It removes all the content from the screen.
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtForename.Text = "";
            txtSurname.Text = "";
            txtAge.Text = "";
            cboCourse.SelectedIndex = -1;
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtPostcode.Text = "";
            txtEmail.Text = "";
            cboCountry.SelectedIndex = -1;
            chkInterStudent.IsChecked = false;

            cboCountry.Visibility = Visibility.Hidden;
            lblCountry.Visibility = Visibility.Hidden;
        }

        private void chkInterStudent_Checked(object sender, RoutedEventArgs e)
        {
            cboCountry.Visibility = Visibility.Visible;
            lblCountry.Visibility = Visibility.Visible;
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateAge() && ValidateEmail() && ValidateText())
            {
                MessageBox.Show("All information entered is correct!", "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void chkInterStudent_Unchecked(object sender, RoutedEventArgs e)
        {
            cboCountry.Visibility = Visibility.Hidden;
            lblCountry.Visibility = Visibility.Hidden;
        }
    }
}
