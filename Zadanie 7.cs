using System;

namespace LancuchOdpowiedzialnosci
{
    public class SupportTicket
    {
        public string Type { get; }
        public string Message { get; }

        public SupportTicket(string type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public abstract class TicketHandler
    {
        protected TicketHandler _nextHandler;

        public TicketHandler SetNext(TicketHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual void Handle(SupportTicket ticket)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle(ticket);
            }
            else
            {
                Console.WriteLine($"[Błąd] Zgłoszenie '{ticket.Message}' o typie '{ticket.Type}' nie zostało obsłużone.");
            }
        }
    }

    public class TechnicalSupportHandler : TicketHandler
    {
        public override void Handle(SupportTicket ticket)
        {
            if (ticket.Type.Equals("Technical", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[Wsparcie Techniczne] Rozwiązano problem: {ticket.Message}");
            }
            else
            {
                base.Handle(ticket);
            }
        }
    }

    public class BillingSupportHandler : TicketHandler
    {
        public override void Handle(SupportTicket ticket)
        {
            if (ticket.Type.Equals("Billing", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[Dział Rozliczeń] Przetworzono płatność/fakturę: {ticket.Message}");
            }
            else
            {
                base.Handle(ticket);
            }
        }
    }

    public class GeneralSupportHandler : TicketHandler
    {
        public override void Handle(SupportTicket ticket)
        {
            if (ticket.Type.Equals("General", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[Biuro Obsługi Klienta] Udzielono odpowiedzi: {ticket.Message}");
            }
            else
            {
                base.Handle(ticket);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TicketHandler techHandler = new TechnicalSupportHandler();
            TicketHandler billingHandler = new BillingSupportHandler();
            TicketHandler generalHandler = new GeneralSupportHandler();

            techHandler.SetNext(billingHandler).SetNext(generalHandler);

            SupportTicket t1 = new SupportTicket("Technical", "Awaria bazy danych na serwerze produkcyjnym");
            SupportTicket t2 = new SupportTicket("Billing", "Błędne naliczenie opłaty za abonament");
            SupportTicket t3 = new SupportTicket("General", "Pytanie o godziny otwarcia infolinii");
            SupportTicket t4 = new SupportTicket("Marketing", "Prośba o ofertę partnerską");

            Console.WriteLine("=== URUCHOMIENIE SYSTEMU OBSŁUGI ZGŁOSZEŃ ===\n");
            
            techHandler.Handle(t1);
            techHandler.Handle(t2);
            techHandler.Handle(t3);
            techHandler.Handle(t4);

            Console.ReadKey();
        }
    }
}
