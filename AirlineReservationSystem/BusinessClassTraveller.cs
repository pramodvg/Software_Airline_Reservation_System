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
        public BusinessClassTraveller(string travellerName, uint travellerAge, uint phoneNumber, string passportNumber, string emailId) : base(travellerName, travellerAge, phoneNumber, passportNumber, emailId)
        {

        }
        //overriding abstract method
        public override string PrioritySeats()
        {
             return "Business class priority seats availed ";
        }

        public string ExclusiveRefreshingKit()
        {
            return "Exclusive Refreshing kits availed ";
        }
        //overriding virtual method from baseclass 
        public override string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PassengerMasks() + PassengerSanitisers() + PrioritySeats() + ExclusiveRefreshingKit());
            return availedServices.ToString();
        }
        //overriding abstract method 
        public override string SpecialServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PrioritySeats() + ExclusiveRefreshingKit());
            return availedServices.ToString();
        }
    }
}
