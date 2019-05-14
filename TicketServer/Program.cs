using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;

namespace TicketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketServer();
        }
        //run server 
        static void TicketServer()
        {
            try
            {
                Console.WriteLine("Ticket Server started...");

                HttpChannel httpChannel = new HttpChannel(9998);
                ChannelServices.RegisterChannel(httpChannel);

                Type commonInterfaceType = Type.GetType("TicketServer.MovieTicket");

                RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
                    "MovieTicketBooking", WellKnownObjectMode.SingleCall);

                Console.WriteLine("Press ENTER to quitnn");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }

    //interface to achive polymorphisim 

    public interface MovieTicketInterface
    {
        string GetTicketStatus(string stringToPrint);
    }
    //service class mus be implemented Marshal 
    public class MovieTicket : MarshalByRefObject, MovieTicketInterface
    {
        public string GetTicketStatus(string stringToPrint)
        {
            try
            {
                string returnStatus = "Ticket Confirmed";
                Console.WriteLine("Enquiry for {0}", stringToPrint);
                Console.WriteLine("Sending back status: {0}", returnStatus);

                return returnStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

    }
}
