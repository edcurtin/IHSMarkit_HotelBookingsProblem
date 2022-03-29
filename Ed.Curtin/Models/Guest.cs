namespace Hotel.Booking.API.Models
{
    public class Guest
    {
        public string Surname {get;private set;}
        public Guest(string guestSurname)
        {
            Surname = guestSurname;
        }
    }
}
