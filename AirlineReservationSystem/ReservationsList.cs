using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AirlineReservationSystem
{
    public class ReservationsList
    {

        public List<Reservation> bookingList = null;

        public ReservationsList()
        {
            bookingList = new List<Reservation>();
        }

        public void add(Reservation bookings)
        {
            bookingList.Add(bookings);
        }

        public void RemoveAt(int index)
        {
            bookingList.RemoveAt(index);
        }

       
        public int Count()
        {
          return bookingList.Count;
        }

        [XmlIgnore]
        public Reservation this[int i]
        {
            get { return bookingList[i]; }
            set { bookingList[i] = value; }
        }
    }
}
