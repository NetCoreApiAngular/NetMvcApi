using System;

namespace HR.Core.Helpers
{
    public class ConvertHelper
    {
        public static TSource? TryParse<TSource>(object input) where TSource : struct
        {
            try
            {
                var result = Convert.ChangeType(input, typeof(TSource));
                if (result != null)
                {
                    return (TSource)result;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
