using Microsoft.OData.Edm;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Garage2._0.Models
{
    public class CheckPersonNr : ValidationAttribute
    {
        // public readonly DateTime PersonNr;
        //private DateTime dt;

        //public CheckPersonNr()
        //{
        //}

        public override bool IsValid(object? value)
        {
            //19500101-1111
            //195001011111
            if (value is string personNr)
            {
                var parts = personNr.Trim().Split('-');

                if (DateTime.TryParseExact(parts[0], new[] { "yyMMdd", "yyyyMMdd" }, new CultureInfo("sv-SE"), DateTimeStyles.None, out dt))
                {

                }

            }

            return false;

        }
    }
}
