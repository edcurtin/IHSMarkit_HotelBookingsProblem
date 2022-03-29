Few Notes
Ed Curtin (07477090517)

1. For a while I dug down the idea that a booking system would only book for 1 day which was flawed
so now has a concept of a booking and rooms have a list of bookings

2. I used the blocking collection for thread safety but started with lists and linq queries which the thread safe collection didnt end up supporting
so thats why I have loops, i did introduce lock because you mention adds and reads can happen at the same time. 

3. API I believe works as have driven it via soap - checked for availabilty with date: (format needed is 2019-07-01T04:00:00.000Z )
Then should true and then driven add a booking and checked again for avialability and showed false and then parked driving via soap.

4. There is a singleton istance in play of the BookingManager

5. Setup and Teardown in xunit 

6. Aware that there could be many more unit tests, didnt go as far as moq or anything. 

7. Few different types of custom exceptions if room arg was incorrect or clash in availability


