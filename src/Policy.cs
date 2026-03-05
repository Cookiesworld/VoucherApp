using System;

namespace VoucherApp;

public class Policy
{
    public string PolicyNumber { get; private set; }
    public decimal BasePrice { get; private set; }

    public Policy(string policyNumber, decimal basePrice)
    {
        PolicyNumber = policyNumber;
        BasePrice = basePrice;
    }
}
