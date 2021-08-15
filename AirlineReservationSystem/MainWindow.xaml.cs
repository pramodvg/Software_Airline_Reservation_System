using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  public enum RadioTypes
        {
            first_class,
            business_class,
            economy_class,
        }
        string XmlFileName = "Reservations.xml";
        string travelTypeRadioGroup = string.Empty;
        uint readAge, readPhoneNumber;
        ReservationsList reservations = new ReservationsList();
        Operations operations = new Operations();
        int selectedIndexEditRecord = -1;

        private ObservableCollection<Reservation> reservationlist = null;

        public ObservableCollection<Reservation> Reservations { get => reservationlist; set => reservationlist = value; }

        public MainWindow()
        {
            InitializeComponent();
            Reservations = new ObservableCollection<Reservation>();
        }
        
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name)
            {
                case "firstClass":
                    travelTypeRadioGroup = "first_class";
                    s1.IsEnabled = true;
                    s4.IsEnabled = true;
                    s2.IsEnabled = false;
                    s3.IsEnabled = false;
                    s2.IsChecked = false;
                    s3.IsChecked = false;
                    break;
                case "businessClass":
                    travelTypeRadioGroup = "business_class";
                    s1.IsEnabled = true;
                    s3.IsEnabled = true;
                    s2.IsEnabled = false;
                    s2.IsChecked = false;
                    s4.IsEnabled = false;
                    s4.IsChecked = false;
                    break;
                case "economyClass":
                    travelTypeRadioGroup = "economy_class";
                    s1.IsEnabled = true;
                    s2.IsEnabled = true;
                    s4.IsEnabled = false;
                    s4.IsChecked = false;
                    s3.IsChecked = false;
                    s3.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
        //add reservations to the observablelist
        private void addAppointmentButton(object sender, RoutedEventArgs e)
        {
            if (addDataToReservationList())
            {
                resetForm();
            }
            
        }

        //validate fields and add data to the observablelist
        private bool addDataToReservationList()
        {
            bool optedCommonServices = false;
            bool optedSpecialServices = false;
            bool flag = validateFields(dateSlotMenu.Text, sourceSlotMenu.Text, destinationSlotMenu.Text, nameInput.Text, ageInput.Text, phoneInput.Text, passportInput.Text, emailInput.Text);
            if (flag)
            {
                if(s1.IsChecked == true)
                {
                    optedCommonServices = true;
                }
           
                switch (travelTypeRadioGroup)
                {
                    case nameof(RadioTypes.first_class):
                        if (s4.IsChecked == true)
                        {
                            optedSpecialServices = true;
                        }
                        break;
                    case nameof(RadioTypes.business_class):
                        if (s3.IsChecked == true)
                        {
                            optedSpecialServices = true;
                        }
                        break;
                    case nameof(RadioTypes.economy_class):
                        if (s2.IsChecked == true)
                        {
                            optedSpecialServices = true;
                        }
                        break;
                    default:
                        Console.WriteLine("None");
                        break;

                }
                reservations = operations.addBookings(dateSlotMenu.Text, sourceSlotMenu.Text, destinationSlotMenu.Text, nameInput.Text, readAge, readPhoneNumber, passportInput.Text, emailInput.Text,travelTypeRadioGroup, selectedIndexEditRecord,optedCommonServices,optedSpecialServices);
                if(selectedIndexEditRecord == -1)
                MessageBox.Show("Reservation Added");    
            }
            return flag;
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
            if (sourceMenu.Equals("") || sourceMenu.Equals(destinationMenu))
            {
                sourceSlotErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
            else
            {
                sourceSlotErrorLabel.Visibility = Visibility.Hidden;
            }
            if (destinationMenu.Equals("") || destinationMenu.Equals(sourceMenu))
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
            if (!uint.TryParse(phoneNo, out readPhoneNumber) || (phoneNo.Length != 10))
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
            if (passportNo.Equals(""))
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
               
            }
            else
            {
                emailInput.BorderBrush = Brushes.Red;
                emailInput.Foreground = Brushes.Red;
                emailErrorLabel.Visibility = Visibility.Visible;
                flag = false;
            }
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

        //display records in the grid
        private void displayReservations_Click(object sender, RoutedEventArgs e)
        {
            ReservationsList reservationsList = operations.ReadFromXmlFile();
            displayDataInGrid(reservationsList);
        }
        //search record from grid based on reference number
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
        //edit selected record 
        private void Edit_Record_Button(object sender, RoutedEventArgs e)
        {
            reservations = operations.ReadFromXmlFile();
            Reservation reservation = (Reservation)displayGrid.SelectedItem;
            if (reservation != null)
            {
               // selectedIndexEditRecord = displayGrid.SelectedIndex;
                selectedIndexEditRecord = reservations.bookingList.FindIndex(delegate (Reservation reservationObject)
                {
                    return reservationObject.BookingReferenceNumber.Equals(reservation.BookingReferenceNumber, StringComparison.Ordinal);
                });

                // MessageBox.Show(selectedIndexEditRecord.ToString());
                 dateSlotMenu.SelectedIndex = getIndexOfComboBox(dateSlotMenu, reservation.TravelDate);
                sourceSlotMenu.SelectedIndex = getIndexOfComboBox(sourceSlotMenu, reservation.TravelSource);
                destinationSlotMenu.SelectedIndex = getIndexOfComboBox(destinationSlotMenu,reservation.TravelDestination);
                nameInput.Text = reservation.Traveller.TravellerName;
                ageInput.Text = reservation.Traveller.TravellerAge.ToString();
                phoneInput.Text = reservation.Traveller.PhoneNumber.ToString();
                passportInput.Text = reservation.Traveller.PassportNumber.ToString();
                string type = reservation.Traveller.GetType().Name;
                if (reservation.OptedCommonServices)
                {
                    s1.IsChecked = true;
                    s1.IsEnabled = true;
                }
                else
                {
                    s1.IsChecked = false;
                    s1.IsEnabled = true;
                }
                if (type == "EconomyTraveller")
                {
                    economyClass.IsChecked = true;
                    if (reservation.OptedSpecialServices)
                    {
                        s2.IsChecked = true;
                    }
                    s2.IsEnabled = true;
                }
                else if(type == "BusinessClassTraveller")
                {
                    businessClass.IsChecked = true;
                    if (reservation.OptedSpecialServices)
                    {
                        s3.IsChecked = true;
                    }
                    s3.IsEnabled = true;
                }
                else {
                    firstClass.IsChecked = true;
                    if (reservation.OptedSpecialServices)
                    {
                        s4.IsChecked = true;
                    }
                      s4.IsEnabled = true;
                }
                emailInput.Text = reservation.Traveller.EmailId.ToString();
                editErrorLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                editErrorLabel.Visibility = Visibility.Visible;
            }

        }
        
        private int getIndexOfComboBox(ComboBox comboBox,string matchValue)
        {
            var comboBoxItem = comboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == matchValue);
            return comboBox.SelectedIndex = comboBox.Items.IndexOf(comboBoxItem);
        }

        //delete selected record from grid
        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            reservations = operations.ReadFromXmlFile();
            if (reservations.Count() > 0)
            {
                
                Reservation reservation = (Reservation)displayGrid.SelectedItem;
                if(reservations != null && reservation != null)
                {
                    int indexOfSelectedRecord = reservations.bookingList.FindIndex(delegate (Reservation Object)
                    {
                        return Object.BookingReferenceNumber.Equals(reservation.BookingReferenceNumber, StringComparison.Ordinal);
                    });

                    reservations = operations.deleteRecord(indexOfSelectedRecord);
                    saveDataToXmlFile();
                    displayDataInGrid(reservations);

   
                    deleteErrorLabel.Visibility = Visibility.Hidden;
                }
                else
                {
                    deleteErrorLabel.Visibility = Visibility.Visible;

                }


            }
            else
            {
                deleteErrorLabel.Visibility = Visibility.Visible;
            }

        }

        private void displayDataInGrid(ReservationsList reservationsList)
        {
            operations.updateRervationList(reservationsList);

            if (reservationsList != null)
            {
                var fetchQuery = from reservation in reservationsList.bookingList
                                 orderby reservation.BookingReferenceNumber
                                 select reservation;
                displayGrid.ItemsSource = fetchQuery;

            }
        
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
            if (selectedIndexEditRecord != -1)
            {
                if (addDataToReservationList())
                {
                     selectedIndexEditRecord = -1;
                    resetForm();
                }
            }
           
            saveDataToXmlFile();
           
            displayDataInGrid(reservations);
            if (reservations.Count() == 0)
            {
                noBookingErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                noBookingErrorLabel.Visibility = Visibility.Hidden;
            }
        }

        

        private void saveDataToXmlFile()
        {
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
        }
 
        private void resetForm()
        {
            dateSlotMenu.SelectedIndex = 0;
            sourceSlotMenu.SelectedIndex = 0;
            destinationSlotMenu.SelectedIndex = 0;
            nameInput.Text = string.Empty;
            ageInput.Text = string.Empty;
            phoneInput.Text = string.Empty;
            passportInput.Text = string.Empty;
            emailInput.Text = string.Empty;
            economyClass.IsChecked = false;
            firstClass.IsChecked = false;
            businessClass.IsChecked = false;
            s1.IsChecked = false;
            s2.IsChecked = false;
            s3.IsChecked = false;
            s4.IsChecked = false;
            s1.IsEnabled = false;
            s2.IsEnabled = false;
            s3.IsEnabled = false;
            s4.IsEnabled = false;
            editErrorLabel.Visibility = Visibility.Hidden;
            deleteErrorLabel.Visibility = Visibility.Hidden;
            noBookingErrorLabel.Visibility = Visibility.Hidden;
        }
    }
}
