using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;

        public double Price 
        {
            get { return price; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        private int nightsCount;

        public int NightsCount
        {
            get { return nightsCount; }
            set 
            {
                if ( value <= 0 )
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        private double discount;

        public double Discount 
        {
            get { return discount; }
            
            set 
            {
                if (value > 100)
                    throw new ArgumentException();
                discount = value;
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }


        public double Total
        {
            get
            {
                var newTotal = Price * NightsCount * (1 - Discount / 100);
                if (newTotal < 0)
                    throw new ArgumentException();
                
                return newTotal;
            }
            set 
            {
                if (value < 0)
                    throw new ArgumentException();
                Discount = (1 - value / (price * nightsCount)) * 100;
            }
        }
    }
}