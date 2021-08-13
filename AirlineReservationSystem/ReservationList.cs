sing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservationSystem
{
    class ReservationList
    {
        public List<Reservation> reservationList = null;

        public ReservationList()
        {
            reservationList = new List<Reservation>();
        }

        public void Add(Reservation appointment)
        {
            reservationList.Add(appointment);
        }
    }
}
