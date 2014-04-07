using System.ComponentModel.DataAnnotations;
using System;

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
}
