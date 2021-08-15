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
    public abstract class Traveller 
    {
        private string travellerName;
        private uint travellerAge;
        private uint phoneNumber;
        private string passportNumber;
        private string emailId;
      
        
        protected Traveller()
        {
        }

        protected Traveller(string travellerName, uint travellerAge, uint phoneNumber, string passportNumber, string emailId)
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
        public string PassportNumber { get => passportNumber; set => passportNumber = value; }
        public string EmailId { get => emailId; set => emailId = value; }

        //BaseClass Method
        public string PassengerMasks()
        {
            return "Masks provided ";
        }
        //BaseClass Method
        public string PassengerSanitisers()
        {
            return "Hand Sanitizers provided ";
        }
        //virtual provide services method
        public virtual string ProvideServices()
        {
            StringBuilder availedServices = new StringBuilder();
            availedServices.Append(PassengerMasks() + PassengerSanitisers());
            return availedServices.ToString();
         
        }
        //abstract method overriden in the derived class
        public abstract string SpecialServices();
        //abstract method overriden in the derived class
        public abstract string PrioritySeats();
    }
}
