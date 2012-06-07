using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberToWord
{
    class NumToThaiWord : IConvert
    {
        public String changeNumericToWords(double numb)
        {
            String num = numb.ToString();
            return changeToWords(num, false);
        }

        public String changeCurrencyToWords(String numb)
        {
            return changeToWords(numb, true);
        }

        public String changeNumericToWords(String numb)
        {
            return changeToWords(numb, false);
        }

        public String changeCurrencyToWords(double numb)
        {
            return changeToWords(numb.ToString(), true);
        }

        private String changeToWords(String numb, bool isCurrency)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = (isCurrency) ? ("Only") : ("");
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                       // andStr = (isCurrency) ? ("") : ("จุด");// just to separate whole numbers from points/Rupees
                        andStr = (isCurrency) ? ("") : ("");
                        endStr = (isCurrency) ? ("บาท " + endStr) : ("");
                       // pointStr = translateRupees(points);
                        pointStr = translateWholeNumber(points)+"สตางค์";
                    }
                }
                //val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
                val = String.Format("{0}{1}{2}{3}", translateWholeNumber(wholeNo).Trim()+"บาท", andStr, pointStr, endStr);
            }
            catch
            {
                ;
            }
            return val;
        }

        private String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))

                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");
                    int numDigits = number.Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...

                    if (beginsZero)
                    {
                        Double num = Convert.ToDouble(number);
                        number = num.ToString();
                        numDigits = number.Length;
                    }

                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            pos = (numDigits % 3) + 1;
                            //place = " Hundred ";
                            place = "ร้อย";
                            break;
                        case 4://thousands' range
                            pos = (numDigits % 4) + 1;
                            //place = " Thousand ";
                            place = "พัน";
                            break;
                        case 5:
                            pos = (numDigits % 5) + 1;
                            //place = " Thousand ";
                            place = "หมื่น";
                            break;
                        case 6:
                            pos = (numDigits % 6) + 1;
                            //place = " Thousand ";
                            place = "แสน";
                            break;
                        case 7://millions' range
                        case 8:
                        case 9:
                        case 10://Billions's range
                            pos = (numDigits % 7) + 1;
                            place = "ล้าน";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }

                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //Console.WriteLine("word : "+word+" number: "+number);
                        //check for trailing zeros
                        if (beginsZero) word = " and " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch
            {
                ;
            }
            return word.Trim();
        }

        private String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "สิบ";// "Ten";
                    break;
                case 11:
                    name = "สิบเอ็ด";// "Eleven";
                    break;
                case 12:
                    name = "สอบสอง";// "Twelve";
                    break;
                case 13:
                    name = "สิบสาม";// "Thirteen";
                    break;
                case 14:
                    name = "สิบสี่";// "Fourteen";
                    break;
                case 15:
                    name = "สิบห้า";// "Fifteen";
                    break;
                case 16:
                    name = "สิบหก";// "Sixteen";
                    break;
                case 17:
                    name = "สิบเจ็ด";// "Seventeen";
                    break;
                case 18:
                    name = "สิบแปด";// "Eighteen";
                    break;
                case 19:
                    name = "สิบเก้า";// "Nineteen";
                    break;
                case 20:
                    name = "ยี่สิบ";// "Twenty";
                    break;
                case 30:
                    name = "สามสิบ";// "Thirty";
                    break;
                case 40:
                    name = "สี่สิบ";// "Fourty";
                    break;
                case 50:
                    name = "ห้าสิบ";// "Fifty";
                    break;
                case 60:
                    name = "หกสิบ";// "Sixty";
                    break;
                case 70:
                    name = "เจ็ดสิบ";// "Seventy";
                    break;
                case 80:
                    name = "แปดสิบ";// "Eighty";
                    break;
                case 90:
                    name = "เก้าสิบ";// "Ninety";
                    break;
                default:
                    if (digt > 0)
                    {
                        name = tens(digit.Substring(0, 1) + "0") + "" + ones(digit.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    //Console.WriteLine("ones case 1 : " + digt);
                    name = "หนึ่ง";// "One";
                    break;
                case 2:
                    name = "สอง";// "Two";
                    break;
                case 3:
                    name = "สาม";//Three";
                    break;
                case 4:
                    name = "สี่";// "Four";
                    break;
                case 5:
                    name = "ห้า";// "Five";
                    break;
                case 6:
                    name = "หก";// "Six";
                    break;
                case 7:
                    name = "เจ็ด";// "Seven";
                    break;
                case 8:
                    name = "แปด";// "Eight";
                    break;
                case 9:
                    name = "เก้า";// "Nine";
                    break;
            }
            return name;
        }

        private String translateRupees(String Rupees)
        {
            String cts = "", digit = "", engOne = "";
            for (int i = 0; i < Rupees.Length; i++)
            {
                digit = Rupees[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cts += " " + engOne;
            }
            return cts;
        }
    }
}
