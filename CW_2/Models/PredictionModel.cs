using System;
using System.Linq;
using System.Threading.Tasks;

namespace CW_2.Models
{
    public class PredictionModel : BaseEntityContextService
    {
        public class PredictionDTO
        {
            public int PredictDate { get; set; }
            public Guid UserId { get; set; }
        }

        public class TransactionPredictDTO
        {
            public Guid Id { get; set; }
            public double Amount { get; set; }
            public DateTime CreatedAt { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public string RecurringType { get; set; }
            public string ContactName { get; set; }
            public string ContactType { get; set; }
        }

        public class TransactionCategory
        {
            public string Type { get; set; }
            public double Amount { get; set; }
            public double Count { get; set; }

        }

        public class PredictionResultDTO
        {
            public double PredictIncome { get; set; }
            public double PredictExpence { get; set; }
        }


        public async Task<PredictionResultDTO> MakePrediction(PredictionDTO model)
        {
            var transactions = (from t in DbContext.TransactionEnities
                                join c in DbContext.ContactDatas
                                on t.ContactDataId equals c.Id
                                where t.IsDeleted == null && t.UserDataId == model.UserId
                                && t.CreatedAt.Day == model.PredictDate
                                select new TransactionPredictDTO()
                                {
                                    Id = t.Id,
                                    Amount = t.Amount,
                                    ContactName = c.Name,
                                    Recurring = t.Recurring,
                                    RecurringType = t.RecurringType.ToString(),
                                    CreatedAt = t.CreatedAt,
                                    ContactType = c.Type
                                }).ToList();

            if (transactions.Count > 0)
            {
                var datecounts = transactions.GroupBy(d => d.CreatedAt.Date).Count();
                var groupTransactions = transactions.GroupBy(i => i.ContactType).Select(s => new TransactionCategory()
                {
                    Amount = s.Sum(t => t.Amount) / datecounts,
                    Count = s.Count(),
                    Type = s.First().ContactType
                }).ToList();

                return new PredictionResultDTO()
                {
                    PredictExpence = groupTransactions.Where(s => s.Type == "Payee").Select(p => p.Amount).First(),
                    PredictIncome = groupTransactions.Where(s => s.Type == "Payer").Select(p => p.Amount).First(),
                };
            }
            else
            {
                return new PredictionResultDTO()
                {
                    PredictExpence = 0,
                    PredictIncome = 0
                };
            }
        }

    }
}
