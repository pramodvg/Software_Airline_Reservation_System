using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    public class BusinessClassTraveller : Traveller
    {
        public BusinessClassTraveller()
        {

        }
        public BusinessClassTraveller(string travellerName, uint travellerAge, uint phoneNumber, uint passportNumber, string emailId) : base(travellerName, travellerAge, phoneNumber, passportNumber, emailId)
        {

        }

        public override string PrioritySeats()
        {
             return "Business class priority seats availed";
        }

        public string ExclusiveRefreshingKit()
        {
            return "Exclusive Refreshing kits availed";
        }

        public override string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PassengerMasks() + PassengerSanitisers() + PrioritySeats() + ExclusiveRefreshingKit());
            return availedServices.ToString();
        }

       
    }
}
