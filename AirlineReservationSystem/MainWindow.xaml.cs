using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Serialization;

namespace AirlineReservationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        string XmlFileName = "Reservations.xml";
        string travelTypeRadioGroup = string.Empty;
        uint readAge,readPhone,readPassport;
        ReservationsList reservations = new ReservationsList();
        Operations operations = new Operations();
        int selectedIndexEditRecord = -1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name)
            {
                case "firstClass":
                    travelTypeRadioGroup = "first_class";
                    break;
                case "businessClass":
                    travelTypeRadioGroup = "business_class";
                    break;
                case "economyClass":
                    travelTypeRadioGroup = "economy_class";
                    break;
                default:
                    break;
            }
        }

        private void addAppointmentButton(object sender, RoutedEventArgs e)
        {
           bool flag = validateFields(dateSlotMenu.Text, sourceSlotMenu.Text, destinationSlotMenu.Text , nameInput.Text, ageInput.Text, phoneInput.Text, passportInput.Text, emailInput.Text);
            if (flag)
            {
              
                
                reservations =  operations.addBookings(dateSlotMenu.Text, sourceSlotMenu.Text, destinationSlotMenu.Text, nameInput.Text, readAge, readPhone, readPassport, emailInput.Text,/* randomReferenceNo.ToString(),*/travelTypeRadioGroup,selectedIndexEditRecord);
            }
        }



        // Function To validate every inputs
        private bool validateFields(string dateSlot, string sourceMenu, string destinationMenu ,string name, string age,string phoneNo, string passportNo, string emailId )
        {

            bool flag = true;
            if (dateSlot.Equals(""))
            {
                datelSotErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                datelSotErrorLabel.Visibility = Visibility.Hidden;
            }
            if (sourceMenu.Equals(""))
            {
                sourceSlotErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                sourceSlotErrorLabel.Visibility = Visibility.Hidden;
            }
            if (destinationMenu.Equals(""))
            {
                destinationSlotErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                destinationSlotErrorLabel.Visibility = Visibility.Hidden;
            }
            if ((name.Length == 0) || (name.Length > 50))
            {
                nameInput.BorderBrush = Brushes.Red;
                nameInput.Foreground = Brushes.Red;
                nameErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                nameInput.BorderBrush = Brushes.LightGray;
                nameErrorLabel.Visibility = Visibility.Hidden;

            }
            if (!uint.TryParse(phoneNo, out readPhone) || (phoneNo.Length != 10))
            {
                phoneInput.BorderBrush = Brushes.Red;
                phoneInput.Foreground = Brushes.Red;
                phoneErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                phoneInput.BorderBrush = Brushes.LightGray;
                phoneErrorLabel.Visibility = Visibility.Hidden;

            }
            if (!uint.TryParse(passportNo, out readPassport) && passportNo.Equals(""))
            {
                passportInput.BorderBrush = Brushes.Red;
                passportInput.Foreground = Brushes.Red;
                passportErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                passportInput.BorderBrush = Brushes.LightGray;
                passportErrorLabel.Visibility = Visibility.Hidden;

            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailId);
            if (match.Success)
            {
               
                emailInput.BorderBrush = Brushes.LightGray;
                emailErrorLabel.Visibility = Visibility.Hidden;
                flag = true;
            }
            else
            {
                emailInput.BorderBrush = Brushes.Red;
                emailInput.Foreground = Brushes.Red;
                emailErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
               

            if (!uint.TryParse(age, out readAge) || (readAge < 1) || (readAge > 100))
            {
                ageInput.BorderBrush = Brushes.Red;
                ageInput.Foreground = Brushes.Red;
                ageErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                ageInput.BorderBrush = Brushes.LightGray;
                ageErrorLabel.Visibility = Visibility.Hidden;

            }
           
           /* if ((s1.IsChecked == false) && (s1.IsChecked == false) && (s1.IsChecked == false))
            {
                serviceErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                serviceErrorLabel.Visibility = Visibility.Hidden;
            }*/
            if(travelTypeRadioGroup == String.Empty)
            {
                travelTypeErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                travelTypeErrorLabel.Visibility = Visibility.Hidden;
            }
            return flag;
        }

       // ReservationsList commonreservationsList;
        private void displayReservations_Click(object sender, RoutedEventArgs e)
        {
            ReservationsList reservationsList = operations.ReadFromXmlFile();
            operations.updateRervationList(reservationsList);
            
            if (reservationsList != null)
            {
                var fetchQuery = from reservation in reservationsList.bookingList
                                 orderby reservation.BookingReferenceNumber
                                 select reservation;
                displayGrid.ItemsSource = fetchQuery;
                
            }
            else
            {
                //noRecordsLabel.Visibility = Visibility.Visible;
            }
        }

        private void searchReservation_click(object sender, RoutedEventArgs e)
        {
            ReservationsList reservationsList = operations.ReadFromXmlFile();
            if (reservationsList != null)
            {
                if (SearchField.Text == "")
                {
                    var fetchQuery = from reservation in reservationsList.bookingList
                                     orderby reservation.BookingReferenceNumber
                                     select reservation;
                    displayGrid.ItemsSource = fetchQuery;
                }
                else
                {
                    var query = from reservation in reservationsList.bookingList
                                where reservation.BookingReferenceNumber == SearchField.Text
                                select reservation;
                    displayGrid.ItemsSource = query;
                }
            }
            else
            {
               // noRecordsLabel.Visibility = Visibility.Visible;
            }
        }
        private void Edit_Record_Button(object sender, RoutedEventArgs e)
        {
            reservations = operations.ReadFromXmlFile();
            Reservation reservation = (Reservation)displayGrid.SelectedItem;
            if (reservation != null)
            {
               // selectedIndexEditRecord = displayGrid.SelectedIndex;
                selectedIndexEditRecord = reservations.bookingList.FindIndex(delegate (Reservation r)
                {
                    return r.BookingReferenceNumber.Equals(reservation.BookingReferenceNumber, StringComparison.Ordinal);
                });

               // MessageBox.Show(selectedIndexEditRecord.ToString());
               // dateSlotMenu.SelectedIndex = dateSlotMenu.Items.IndexOf(reservation.TravelDate);
               // sourceSlotMenu.SelectedIndex = sourceSlotMenu.Items.IndexOf(reservation.TravelSource);
               // destinationSlotMenu.SelectedIndex = destinationSlotMenu.Items.IndexOf(reservation.TravelDestination);
                nameInput.Text = reservation.Traveller.TravellerName;
                ageInput.Text = reservation.Traveller.TravellerAge.ToString();
                phoneInput.Text = reservation.Traveller.PhoneNumber.ToString();
                passportInput.Text = reservation.Traveller.PassportNumber.ToString();
                emailInput.Text = reservation.Traveller.EmailId.ToString();
                businessClass.IsChecked = true;
            }
          
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            int indexOfSelectedRecord = displayGrid.SelectedIndex;
           
                reservations = operations.deleteRecord(indexOfSelectedRecord);
         
               
        }

        private void onTextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = Brushes.Black;
            textBox.BorderBrush = Brushes.LightGray;
        }

        private void saveBooking(object sender, RoutedEventArgs e)
        {

            // Write data into Xml File
            selectedIndexEditRecord = -1;
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ReservationsList));
                    TextWriter t = new StreamWriter(XmlFileName);
                    serializer.Serialize(t, reservations);
                    t.Close();
                }
                catch (Exception)
                {

                }
                // resetForm();
                
            if (reservations.Count() == 0)
            {             
                noBookingErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Reservation Done ✔");
            }
        }
    }
}
