using System;
using System.Collections.Generic;
using System.Linq;
using TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;


namespace HerdingSystem
{
    [Serializable]
    public class TicketManager
    {
        [SerializeField] private List<HerdingTicket> tickets = new List<HerdingTicket>();
        
        private Dictionary<TicketDifficulty, List<HerdingTicket>> ticketDifficulties = new Dictionary<TicketDifficulty, List<HerdingTicket>>
        {
            { TicketDifficulty.Easy , new List<HerdingTicket>()},
            { TicketDifficulty.Medium , new List<HerdingTicket>()},
            { TicketDifficulty.Hard , new List<HerdingTicket>()},
        };

        public void Init() {
            foreach (HerdingTicket ticket in tickets) {
                ticketDifficulties[ticket.difficulty].Add(ticket);
            }
        }

        /// <summary>
        /// Gets a herding ticket based on how many days the player has played
        /// 0 - 5 days = Easy
        /// 6 - 17 days = Easy + Medium
        /// 18 - ∞ = Easy + Medium + Hard
        /// </summary>
        public HerdingTicket GetTicket() {
            uint day = TimeManager.Instance.dayCount;
            HerdingTicket ticket;

            if (day >= 18) {
                List<HerdingTicket> hardTickets = ticketDifficulties[TicketDifficulty.Easy]
                    .Concat(ticketDifficulties[TicketDifficulty.Medium])
                    .Concat(ticketDifficulties[TicketDifficulty.Hard])
                    .ToList();
                int i = Random.Range(0, hardTickets.Count);
                ticket = hardTickets[i];
            } 
            else if (day >= 6) {
                List<HerdingTicket> mediumTickets = ticketDifficulties[TicketDifficulty.Easy]
                    .Concat(ticketDifficulties[TicketDifficulty.Medium])
                    .ToList();
                int i = Random.Range(0, mediumTickets.Count);
                ticket = mediumTickets[i];
            }
            else {
                int i = Random.Range(0, ticketDifficulties[TicketDifficulty.Easy].Count);
                ticket = ticketDifficulties[TicketDifficulty.Easy][i];
            }
            
            return ticket;
        }
    }
}
