using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Garage2._0.Models
{
    public class CheckPersonNr : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string personNr)
            {
                var parts = personNr.Trim().Split('-');
                if (parts.Length != 2) return false;

                if (DateTime.TryParseExact(parts[0], new[] { "yyMMdd", "yyyyMMdd" }, new CultureInfo("sv-SE"), DateTimeStyles.None, out DateTime dt))
                {
                    return int.TryParse(parts[1], out int num) && parts[1].Length == 4;
                }
            }
            return false;
        }
    }
}
