using System;

namespace Shopping.Logic
{
    public class DiscountManager
    {
        private float _totalPrice;
        private DiscountType _type;

        public DiscountManager(float totalPrice, DiscountType type = DiscountType.Percentage)
        {
            _totalPrice = totalPrice;
            _type = type;
        }

        public float GetPriceAfterDiscount()
        {
            if (_type == DiscountType.Percentage)
            {
                return GetPriceAfterPercentageDiscount();
            }
            else
            {
                return GetPriceAfterFixedPriceDiscount();
            }
        }

        private float GetPriceAfterPercentageDiscount()
        {
            float discPrice = _totalPrice;

            if (_totalPrice >= 1000)
            {
                discPrice = _totalPrice * (float)0.7;
            }
            else if (discPrice >= 500)
            {
                discPrice = _totalPrice * (float)0.8;
            }
            else if (discPrice >= 100)
            {
                discPrice = _totalPrice * (float)0.9;
            }

            return discPrice;
        }

        private float GetPriceAfterFixedPriceDiscount()
        {
            float discPrice = _totalPrice;

            if (_totalPrice >= 1000)
            {
                discPrice = _totalPrice - 300;
            }
            else if (discPrice >= 500)
            {
                discPrice = _totalPrice - 100;
            }
            else if (discPrice >= 100)
            {
                discPrice = _totalPrice - 10;
            }

            return discPrice;
        }
    }

    public enum DiscountType
    {
        Percentage = 1,
        FixedAmount = 2
    }
}
