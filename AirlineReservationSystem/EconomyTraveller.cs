using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    public class EconomyTraveller : Traveller
    {
        public EconomyTraveller()
        {

        }
        public EconomyTraveller(string travellerName, uint travellerAge, uint phoneNumber, string passportNumber, string emailId) : base(travellerName, travellerAge, phoneNumber, passportNumber, emailId)
        {

        }
        //overriding abstract method
        public override string PrioritySeats()
        {
            throw new NotImplementedException();
        }

        public string Desserts()
        { 
             return "Desserts Offered ";
        }
        //overriding virtual method from baseclass
        public override string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PassengerMasks() + PassengerSanitisers() + Desserts());
            return availedServices.ToString();
        }
        //overriding abstract method
        public override string SpecialServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(Desserts());
            return availedServices.ToString();
        }
    }
}
