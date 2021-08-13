using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AirlineReservationSystem
{
    class Operations
    {
        string XmlFileName = "Reservations.xml";
        
        uint randomReferenceNo = 0;
        public Operations()
        {

        }

        public ReservationsList ReadFromXmlFile()
        {
            ReservationsList reservations = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ReservationsList));
                StreamReader reader = new StreamReader(XmlFileName);
                reservations = (ReservationsList)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (FileNotFoundException)
            {
                return reservations;
            }
            return reservations;
        }


        ReservationsList reservations = new ReservationsList();
        public ReservationsList addBookings(string dateSlot,string source,string destination, string name, uint age, uint phoneNo, uint passportNo, string emailId,/* string bookingReference,*/string travelTypeRadioGroup, int updateIndexRecord)
        {
            Reservation bookings = new Reservation();
           var rand = new Random();
             
            if (updateIndexRecord == -1)
            {
                randomReferenceNo = (uint)rand.Next(100);
            }
            else
            {
                randomReferenceNo =
                   uint.Parse( reservations[updateIndexRecord].BookingReferenceNumber);
            }

            bookings.BookingReferenceNumber = randomReferenceNo.ToString();
            bookings.TravelDate = dateSlot;
            bookings.TravelSource = source;
            bookings.TravelDestination = destination;
            try
            {
                switch (travelTypeRadioGroup)
                {
                    case "first_class":
                        FirstClassTraveller traveller = new FirstClassTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = traveller;
                        bookings.Services = traveller.ProvideServices();
                        break;
                    case "business_class":
                        BusinessClassTraveller businessTraveller = new BusinessClassTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = businessTraveller;
                        bookings.Services = businessTraveller.ProvideServices();
                        break;
                    case "economy_class":
                        EconomyTraveller economyTraveller = new EconomyTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = economyTraveller;
                        bookings.Services = economyTraveller.ProvideServices();
                        break;
                    default:
                        Console.WriteLine("None");
                        break;

                }
                //bookings.Traveller = new FirstClassTraveller(name, age, phoneNo, passportNo, emailId); 
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            if(updateIndexRecord == -1 || reservations.bookingList.Count() == 0)
            {
                reservations.add(bookings);
            }
            else
            {
                    reservations[updateIndexRecord] = bookings;
            }
            return reservations;
        }

       
        public void updateRervationList(ReservationsList reservationsList)
        {
            reservations = reservationsList;
        }

        public ReservationsList deleteRecord(int index)
        {
            reservations.RemoveAt(index);
            return reservations;
        }

    }
}
