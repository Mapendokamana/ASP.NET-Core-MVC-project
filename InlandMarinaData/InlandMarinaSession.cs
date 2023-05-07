using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class InlandMarinaSession
    {
        private const string SlipsKey = "myslips";
        private const string CountKey = "slipcount";

        private ISession session { get; set; }
        public InlandMarinaSession(ISession session)
        {
            this.session = session;
        }

        public void SetMySlips(List<Slip> slips)
        {
            session.SetObject(SlipsKey, slips);
            session.SetInt32(CountKey, slips.Count);
        }
        public List<Slip> GetMySlips() =>
            session.GetObject<List<Slip>>(SlipsKey) ?? new List<Slip>();
        public int? GetMySlipCount() => session.GetInt32(CountKey);

    }
}
