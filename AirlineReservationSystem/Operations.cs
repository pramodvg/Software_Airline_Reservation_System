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
        public enum RadioTypes
        {
            first_class,
            business_class,
            economy_class,
        }

        string XmlFileName = "Reservations.xml";
        
        uint randomReferenceNo = 0;
        public Operations()
        {

        }

        //read from xml file
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
        public ReservationsList addBookings(string dateSlot,string source,string destination, string name, uint age, uint phoneNo, string passportNo, string emailId,string travelTypeRadioGroup, int updateIndexRecord,bool optedCommonServices,bool optedSpecialServices)
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
            bookings.OptedCommonServices = optedCommonServices;
            bookings.OptedSpecialServices = optedSpecialServices;
            try
            {
                switch (travelTypeRadioGroup)
                {
                    case nameof(RadioTypes.first_class):
                        FirstClassTraveller traveller = new FirstClassTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = traveller;
                        if(optedCommonServices == true && optedSpecialServices == false)
                        {
                            bookings.Services = traveller.PassengerMasks() + traveller.PassengerSanitisers();
                        }
                        if (optedCommonServices == true && optedSpecialServices == true)
                        {
                            bookings.Services = traveller.ProvideServices();
                        }
                        if (optedCommonServices == false && optedSpecialServices == false)
                        {
                            bookings.Services = "No Services Opted";
                        }
                        if (optedCommonServices == false && optedSpecialServices == true)
                        {
                            bookings.Services = traveller.SpecialServices();
                        }
                        break;
                    case nameof(RadioTypes.business_class):
                        BusinessClassTraveller businessTraveller = new BusinessClassTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = businessTraveller;
                        if (optedCommonServices == true && optedSpecialServices == false)
                        {
                            bookings.Services = businessTraveller.PassengerMasks() + businessTraveller.PassengerSanitisers();
                        }
                        if (optedCommonServices == true && optedSpecialServices == true)
                        {
                            bookings.Services = businessTraveller.ProvideServices();
                        }
                        if (optedCommonServices == false && optedSpecialServices == false)
                        {
                            bookings.Services = "No Services Opted";
                        }
                        if (optedCommonServices == false && optedSpecialServices == true)
                        {
                            bookings.Services = businessTraveller.SpecialServices();
                        }
                        break;
                    case nameof(RadioTypes.economy_class):
                        EconomyTraveller economyTraveller = new EconomyTraveller(name, age, phoneNo, passportNo, emailId);
                        bookings.Traveller = economyTraveller;
                        if (optedCommonServices == true && optedSpecialServices == false)
                        {
                            bookings.Services = economyTraveller.PassengerMasks() + economyTraveller.PassengerSanitisers();
                        }
                        if (optedCommonServices == true && optedSpecialServices == true)
                        {
                            bookings.Services = economyTraveller.ProvideServices();
                        }
                        if (optedCommonServices == false && optedSpecialServices == false)
                        {
                            bookings.Services = "No Services Opted";
                        }
                        if (optedCommonServices == false && optedSpecialServices == true)
                        {
                            bookings.Services = economyTraveller.SpecialServices();
                        }
                        break;
                    default:
                        Console.WriteLine("None");
                        break;

                }

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

       //update the reservation list
        public void updateRervationList(ReservationsList reservationsList)
        {
            reservations = reservationsList;
        }

        //delete the record
        public ReservationsList deleteRecord(int index)
        {
            reservations.RemoveAt(index);
            return reservations;
        }

    }
}
