using System.ComponentModel.DataAnnotations;
using System;
using BM.Models;

namespace BM.Web.ViewModels
{
    public class LedgerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MailingName { get; set; }

        public string GroupName { get; set; }
        public decimal? OpeningBalance { get; set; }
    }

    public class LedgerEditViewModel
    {
        public Ledger Ledger { get; set; }
        public InterestParameter InterestParameter { get; set; }
    }
}
