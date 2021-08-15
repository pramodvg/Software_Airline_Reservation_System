using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
   public class Reservation
    {
        private string travelDate;
        private string travelSource;
        private string travelDestination;
        private string bookingReferenceNumber;
        private string services;
        private Boolean optedCommonServices;
        private Boolean optedSpecialServices;
        private Traveller traveller;

        public string TravelDate { get => travelDate; set => travelDate = value; }
        public string TravelSource { get => travelSource; set => travelSource = value; }
        public string TravelDestination { get => travelDestination; set => travelDestination = value; }
        public string BookingReferenceNumber { get => bookingReferenceNumber; set => bookingReferenceNumber = value; }
        public Traveller Traveller { get => traveller; set => traveller = value; }
        public string Services { get => services; set => services = value; }
        public bool OptedCommonServices { get => optedCommonServices; set => optedCommonServices = value; }
        public bool OptedSpecialServices { get => optedSpecialServices; set => optedSpecialServices = value; }
    }
}
