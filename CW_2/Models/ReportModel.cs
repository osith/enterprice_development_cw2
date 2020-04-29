using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CW_2.Models.EventModel;
using static CW_2.Models.TransactionModel;
using static CW_2.Models.UserModal;

namespace CW_2.Models
{
    public class ReportModel
    {
        private readonly TransactionModel _transactionModel;
        private readonly EventModel _eventModel;
        private readonly RegisterUserDTO _userModel;
        public ReportModel(RegisterUserDTO user)
        {
            _userModel = user;
            _transactionModel = new TransactionModel(user.Id);
            _eventModel = new EventModel();
        }

        public class ReportModelDTO
        {
            public Guid UserId { get; set; }
            public Nullable<int> Category { get; set; }
            public Nullable<DateTime> From { get; set; }
            public Nullable<DateTime> To { get; set; }
        }


        public async Task<List<TransactionViewDTO>> SearchTransactions(ReportModelDTO fields)
        {
            var transactions = Task.Run(() => _transactionModel.LoadAllTransactions(fields.UserId)).Result;
            if (fields.From.HasValue && fields.To.HasValue)
                transactions = transactions.Where(t => t.CreatedAt >= fields.From.Value && t.CreatedAt <= fields.To.Value).ToList();
            return transactions;
        }

        public async Task<List<EventViewDTO>> SearchEvents(ReportModelDTO fields)
        {
            var events = Task.Run(() => _eventModel.LoadAllEvents(fields.UserId)).Result;
            if (fields.From.HasValue && fields.To.HasValue)
                events = events.Where(e =>
                (e.EventOn >= fields.From.Value && e.EventOn <= fields.To.Value)
                ||
                (e.CompleteBefore >= fields.From.Value && e.CompleteBefore <= fields.To.Value)).ToList();
            return events;
        }

    }
}
