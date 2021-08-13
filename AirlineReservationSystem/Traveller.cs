using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AirlineReservationSystem
{
    [Serializable]
    [XmlInclude(typeof(BusinessClassTraveller)), XmlInclude(typeof(EconomyTraveller)), XmlInclude(typeof(FirstClassTraveller))]
    public abstract class Traveller : ITraveller
    {
        private string travellerName;
        private uint travellerAge;
        private uint phoneNumber;
        private uint passportNumber;
        private string emailId;
      

        protected Traveller()
        {
        }

        protected Traveller(string travellerName, uint travellerAge, uint phoneNumber, uint passportNumber, string emailId)
        {
            this.travellerName = travellerName;
            this.travellerAge = travellerAge;
            this.phoneNumber = phoneNumber;
            this.passportNumber = passportNumber;
            this.emailId = emailId;
           
        }

        public string TravellerName { get => travellerName; set => travellerName = value; }
        public uint TravellerAge { get => travellerAge; set => travellerAge = value; }
        public uint PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public uint PassportNumber { get => passportNumber; set => passportNumber = value; }
        public string EmailId { get => emailId; set => emailId = value; }

        public string PassengerMasks()
        {
            return "Masks provided";
        }
        public string PassengerSanitisers()
        {
            return "Hand Sanitizers provided";
        }

        public virtual string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PassengerMasks() + PassengerSanitisers());
            return availedServices.ToString();
         
        }

        public abstract string PrioritySeats();
    }
}
