using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IRU.RTS.WSEGIS
{
    // This class has been translated "as is" from Java class on 2014-04-28
    public class TIRVoucherUtils
    {
        private static string voucherRegexp = ("([0-9]{4}-){2}([0-9]{4})$");


        private static int WEIGHT_1 = 3;
        private static int WEIGHT_2 = 1;

        private static int[] weightA = new int[] { 1, 3, 1, 3, 1, 3, 1, 3 };
        private static int[] weightB = new int[] { 5, 7, 2, 5, 7, 2, 5, 7 };
        private static int[] weightX = new int[] { 2, 9, 3, 9, 2, 9, 3, 9 };
        private static int[] weightY = new int[] { 7, 7, 7, 7, 7, 7, 7, 7 };

        private static int positionA = 0;
        private static int positionB = 1;
        private static int positionX = 12;
        private static int positionY = 13;

        private static string SEPARATOR = "-";

        private static int returnWeight(int occurence, int weight1, int weight2)
        {
            if (occurence % 2 == 0)
            {
                return weight1;
            }
            else
            {
                return weight2;
            }
        }
        private static int returnWeight(int occurence, int[] weight)
        {
            return weight[occurence];
        }

        private static int getKeyForAweight(string voucherNumber, int[] weight)
        {
            int key;
            int sum = 0;
            //-- we start from the right --//            
            for (int i = 0; i < voucherNumber.Length; i++)
            {
                string val = voucherNumber.Substring(i, 1);
                int value = Int32.Parse(val);
                sum = sum + (value * returnWeight(i, weight));
            }

            //-- compute the rest --//
            key = sum % 10;

            return key;
        }

        public static string getVoucherFromInt(int voucherNumber)
        {
            string res;
            res = voucherNumber.ToString();
            while (res.Length != 8)
            {
                res = "0" + res;
            }

            int keyComputedA = getKeyForAweight(res, weightA);
            int keyComputedB = getKeyForAweight(res, weightB);
            int keyComputedX = getKeyForAweight(res, weightX);
            int keyComputedY = getKeyForAweight(res, weightY);



            res = keyComputedA.ToString() + keyComputedB.ToString() + res + keyComputedX.ToString() + keyComputedY.ToString();
            res = res.Substring(0, 4) + SEPARATOR + res.Substring(4, 4) + SEPARATOR + res.Substring(8);
            return res;

        }



        public static int getNumericVoucherNumber(string fullVoucherNumber)
        {
            
            if (!Regex.IsMatch(fullVoucherNumber, voucherRegexp))
            {
                throw new FormatException("Wrong Checked Voucher Number: " + fullVoucherNumber);
            }

            //-- get key value --//
            int keyA = int.Parse(fullVoucherNumber.Substring(positionA, 1));
            int keyB = int.Parse(fullVoucherNumber.Substring(positionB, 1));
            int keyX = int.Parse(fullVoucherNumber.Substring(positionX, 1));
            int keyY = int.Parse(fullVoucherNumber.Substring(positionY, 1));

            string voucherNumber = fullVoucherNumber.Substring(Math.Max(positionA, positionB) + 1, Math.Min(positionX, positionY) - Math.Max(positionA, positionB));
            //-- TODO - replace the below line by a regexp --//
            voucherNumber = voucherNumber.Replace(SEPARATOR, "");


            int keyComputedA = getKeyForAweight(voucherNumber, weightA);
            int keyComputedB = getKeyForAweight(voucherNumber, weightB);
            int keyComputedX = getKeyForAweight(voucherNumber, weightX);
            int keyComputedY = getKeyForAweight(voucherNumber, weightY);


            //-- compare key to to key computed --//
            if (keyComputedA == keyA && keyComputedB == keyB && keyComputedX == keyX && keyComputedY == keyY)
            {
                return int.Parse(voucherNumber);
            }
            else
            {
                throw new FormatException("Wrong Checked Voucher Number: " + fullVoucherNumber);
            }
        }
    }
}
