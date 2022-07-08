using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Interfaces.Common
{
    public interface ISMSSender
    {
        bool SendSMS(string body, string ToCellNumber, string FromCellNumber);
    }
}
