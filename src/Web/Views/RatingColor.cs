namespace Web.Views  
{
    public enum ColorType   
    {
        Background,
        Font
    }

    public static class RatingColor     
    {
        public static string GetClass(double value, ColorType color)
        {
            double lowLimit = 3.34;
            double middleLimit = 6.67;

            if (value <= lowLimit)
                return color == ColorType.Background ? "low-rating" : "low-rating-font";
            if (value > lowLimit && value < middleLimit)
                return color == ColorType.Background ? "middle-rating" : "middle-rating-font";
            return color == ColorType.Background ? "high-rating" : "high-rating-font";
        }
    }
}
