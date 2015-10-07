using System.ComponentModel.DataAnnotations;

namespace Basketball.Common.Validation
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        // TODO Implement client side
        public EmailAttribute()
            : base("^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$")
        {
        }

    }
}
