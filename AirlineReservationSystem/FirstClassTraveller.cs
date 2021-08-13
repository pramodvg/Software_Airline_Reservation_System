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

        public FirstClassTraveller(string travellerName, uint travellerAge, uint phoneNumber, uint passportNumber, string emailId) : base(travellerName, travellerAge, phoneNumber, passportNumber, emailId)
        {
            
        }

        public override string PrioritySeats()
        {
            return "First class priority seats availed";
        }

        public string FirstClassCoupon()
        {
            return "Discount Coupon Availed";
        }

        public override string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PrioritySeats() + FirstClassCoupon() + PassengerMasks() + PassengerSanitisers());
            return availedServices.ToString();
        }
    }
}
