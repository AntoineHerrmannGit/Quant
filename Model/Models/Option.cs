using Models.Enums;

namespace Models;

public class Option : Instrument
{
    public double Value { get; set; }
    public double Strike { get; set; }
    public DateTime Maturity { get; set; }
    public Instrument Underlying { get; set; }
    public OptionType Type { get; set; }
    public double Delta { get; set; }
    public double Gamma { get; set; }
    public double Theta { get; set; }
    public double Vega { get; set; }
    public double Rho { get; set; }
}
