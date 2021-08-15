using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    public class FirstClassTraveller : Traveller
    {
        
        public FirstClassTraveller()
        {

        }

        public FirstClassTraveller(string travellerName, uint travellerAge, uint phoneNumber, string passportNumber, string emailId) : base(travellerName, travellerAge, phoneNumber, passportNumber, emailId)
        {
            
        }
        //overriding abstract method
        public override string PrioritySeats()
        {
            return "First class priority seats availed ";
        }

        public string FirstClassCoupon()
        {
            return "Discount Coupon Availed ";
        }
        //overriding virtual method from baseclass
        public override string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PrioritySeats() + FirstClassCoupon() + PassengerMasks() + PassengerSanitisers());
            return availedServices.ToString();
        }

        //overriding abstract method
        public override string SpecialServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PrioritySeats() + FirstClassCoupon());
            return availedServices.ToString();
        }
    }
}
