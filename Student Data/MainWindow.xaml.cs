using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private bool ValidEmail(string address)
        {
            // Sources: https://stackoverflow.com/a/17513022/11244896
            //          https://stackoverflow.com/a/719543/11244896
            string regex = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$";

            if (Regex.IsMatch(address, regex))
            {
                if (Regex.Replace(address, regex, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Validate()
        {
            bool textboxes = false;
            bool age = false;
            bool email = false;
            try
            {
                if (string.IsNullOrWhiteSpace(txtForename.Text)
                    || string.IsNullOrWhiteSpace(txtSurname.Text)
                    || string.IsNullOrWhiteSpace(txtAddress1.Text)
                    || string.IsNullOrWhiteSpace(txtCity.Text)
                    || string.IsNullOrWhiteSpace(txtPostcode.Text))
                {
                    throw new System.FormatException("Field cannot be empty!");
                    
                } else
                {
                    textboxes = true;
                }

                if (int.Parse(txtAge.Text) < 16
                    || int.Parse(txtAge.Text) > 101)
                {
                    throw new System.Exception("Numeric value cannot be outwith range! {16 <= n <= 101}");
                } else
                {
                    age = true;
                }

                if (ValidEmail(txtEmail.Text))
                {
                    email = true;
                }
                else
                {
                    throw new System.Exception("Numeric value cannot be outwith range! {16 <= n <= 101}");
                }
            } catch (Exception e)
            {
                MessageBox.Show("Please check the following:\n\nAll fields are not empty/whitespace\nAge is within range (16-101 inclusive)\nA valid email address is present", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return (email && textboxes && age);
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
        }

        private void chkInterStudent_Checked(object sender, RoutedEventArgs e)
        {
            if (chkInterStudent.IsChecked == true)
            {
                cboCountry.Visibility = Visibility.Visible;
                lblCountry.Visibility = Visibility.Visible;
            }

            if (chkInterStudent.IsChecked == false)
            {
                cboCountry.Visibility = Visibility.Hidden;
                lblCountry.Visibility = Visibility.Hidden;
            }
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == true)
            {
                MessageBox.Show("All information entered is correct!\n\nPushing to database...", "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
