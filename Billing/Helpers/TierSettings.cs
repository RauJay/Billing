namespace Billing.Helpers
{

    public class TierSettings
    {
        public const string Key = "TierSettings";

        public DesignImplant DesignImplants { get; set; }

        public PrintImplant PrintImplants { get; set; }
    }

    public class DesignImplant
    {
        public double Tier5 { get; set; }

        public double Tier10 { get; set; }

    }
    public class PrintImplant
    {
        public double Tier25 { get; set; }

        public double Tier30 { get; set; }

    }
}
