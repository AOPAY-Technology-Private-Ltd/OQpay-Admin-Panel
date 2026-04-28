using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace TheEMIClubApplication.AppCode
{
    public class TwilioServices
    {
        public TwilioServices()
        {

        }

        //'US' for United States, 'CA' for Canada, 'GB' for United Kingdom

        #region Create Sub Account in Twilio
        public static string CreateTwilioSubAccount(string CompanyCode, string CompanyName, string AccountSid, string AuthToken)
        {
            try
            {
                string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                if (DomainName.Contains("afmsuite.com"))
                {
                    string AccountName = CompanyCode + "(" + CompanyName + ")";
                    var twilio = new TwilioRestClient(AccountSid, AuthToken);
                    var account = twilio.CreateSubAccount(AccountName);
                    string AccountDetails = account.Sid + "|" + account.AuthToken;
                    return AccountDetails;
                }
                else
                {
                    //string AccountDetails = "fgsdfgsfg989089090|ggdfgdfg9999999";
                    string AccountDetails = "testsid|authtoken";
                    return AccountDetails;
                }
                //string AccountName = CompanyCode + "(" + CompanyName + ")";
                //var twilio = new TwilioRestClient(AccountSid, AuthToken);
                //var account = twilio.CreateSubAccount(AccountName);
                //string AccountDetails = account.Sid + "|" + account.AuthToken;
                //string AccountDetails = "fgsdfgsfg989089090|ggdfgdfg9999999";                       
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion Generate Phone Number in Twilio's Sub Account

        #region Create Twilio Phone Number by Area Code
        public static string GeneratePhoneNumber_AreaCode(string AccountSid, string AuthToken, string AreaCode, string CountryISO)
        {
            try
            {
                string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                if (DomainName.Contains("afmsuite.com"))
                {
                    var twilio = new TwilioRestClient(AccountSid, AuthToken);
                    var options = new AvailablePhoneNumberListRequest();
                    options.AreaCode = AreaCode;

                    var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                    // Purchase the first number in the list
                    var availableNumber = result.AvailablePhoneNumbers[0];
                    var purchaseOptions = new PhoneNumberOptions();
                    purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                    var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                    return number.PhoneNumber;
                }
                else
                {
                    return "+16455555555";
                }
                //var twilio = new TwilioRestClient(AccountSid, AuthToken);
                //var options = new AvailablePhoneNumberListRequest();
                //options.AreaCode = AreaCode;

                //var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                //// Purchase the first number in the list
                //var availableNumber = result.AvailablePhoneNumbers[0];
                //var purchaseOptions = new PhoneNumberOptions();
                //purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                //var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                //return number.PhoneNumber;
                //return "+16455555555";
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion Generate Phone Number in Twilio's Sub Account

        #region Create Twilio Phone Number by Postal Code
        public static string GeneratePhoneNumber_InPostalCode(string AccountSid, string AuthToken, string InPostalCode, string CountryISO)
        {
            try
            {
                string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                if (DomainName.Contains("afmsuite.com"))
                {
                    var twilio = new TwilioRestClient(AccountSid, AuthToken);
                    var options = new AvailablePhoneNumberListRequest();
                    options.InPostalCode = InPostalCode;

                    var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                    // Purchase the first number in the list
                    var availableNumber = result.AvailablePhoneNumbers[0];
                    var purchaseOptions = new PhoneNumberOptions();
                    purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                    var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                    return number.PhoneNumber;
                }
                else
                {
                    return "+16455555555";
                }
                //var twilio = new TwilioRestClient(AccountSid, AuthToken);
                //var options = new AvailablePhoneNumberListRequest();
                //options.InPostalCode = InPostalCode;

                //var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                //// Purchase the first number in the list
                //var availableNumber = result.AvailablePhoneNumbers[0];
                //var purchaseOptions = new PhoneNumberOptions();
                //purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                //var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                //return number.PhoneNumber;
                //return "+16455555555";
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion Generate Phone Number in Twilio's Sub Account

        #region Create Twilio Phone Number by Area & Postal Code Both
        public static string GeneratePhoneNumber_InPostalCodeAreaCode(string AccountSid, string AuthToken, string InPostalCode, string CountryISO, string AreaCode)
        {
            try
            {
                string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                if (DomainName.Contains("afmsuite.com"))
                {
                    var twilio = new TwilioRestClient(AccountSid, AuthToken);
                    var options = new AvailablePhoneNumberListRequest();
                    options.InPostalCode = InPostalCode;
                    options.AreaCode = AreaCode;

                    var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                    // Purchase the first number in the list
                    var availableNumber = result.AvailablePhoneNumbers[0];
                    var purchaseOptions = new PhoneNumberOptions();
                    purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                    var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                    return number.PhoneNumber;
                }
                else
                {
                    return "+16455555555";
                }
                //var twilio = new TwilioRestClient(AccountSid, AuthToken);
                //var options = new AvailablePhoneNumberListRequest();
                //options.InPostalCode = InPostalCode;
                //options.AreaCode = AreaCode;

                //var result = twilio.ListAvailableLocalPhoneNumbers(CountryISO, options);

                //// Purchase the first number in the list
                //var availableNumber = result.AvailablePhoneNumbers[0];
                //var purchaseOptions = new PhoneNumberOptions();
                //purchaseOptions.PhoneNumber = availableNumber.PhoneNumber;

                //var number = twilio.AddIncomingPhoneNumber(purchaseOptions);
                //return number.PhoneNumber;
                //return "+16455555555";
            }
            catch (ApplicationException appEx)
            {
                throw appEx;
            }
        }
        #endregion Create Twilio Phone Number by Area & Postal Code Both

        public static double abc()
        {
            string AccountSid = "ACe1989a41c3c3f351006781d0d6b68fae";
            string AuthToken = "5585d2c8985a45bcab93d91f76726399";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            //This method will Count the number of messages sent between specified date range.
            //var records = twilio.ListUsage("sms", null, new DateTime(2016, 01, 01), new DateTime(2016, 06, 30));
            //var records = twilio.ListUsage("sms", "Today", null , null);

            //var records = twilio.ListUsage("phonenumbers", "LastMonth", null, null);
            //var records = twilio.ListUsage("phonenumbers", null, new DateTime(2016, 01, 01), new DateTime(2016, 06, 30));

            //var records = twilio.ListUsage("calls", "LastMonth", null, null);
            //var records = twilio.ListUsage("calls", "AllTime", null, null);//Voci call till yet
            //var records = twilio.ListUsage("calls", null, new DateTime(2016, 01, 01), new DateTime(2016, 06, 30));

            //var records = twilio.ListUsage("totalprice", null, new DateTime(2016, 01, 01), new DateTime(2016, 06, 30));
            //var records = twilio.ListUsage("totalprice", null, new DateTime(2016, 06, 01), new DateTime(2016, 06, 30));
            var records = twilio.ListUsage("totalprice", "LastMonth", null, null);

            foreach (var record in records.UsageRecords)
            {
                Console.WriteLine(record.Usage);//return double
                Console.WriteLine(record.UsageUnit);//it is a measuring unit like for sms - segment or for calls - minutes
                                                    //return record.Count;// Different for certain usage categories like calls
                return record.Price;
            }
            return 0.00;
        }
        //Usage Category - Calls / sms / phonenumbers / Recorded Minultes / 
        //Subresource - Daily / Monthly / Yearly / AllTime / Today / Yesterday / ThisMonth / LastMonth
        //Start Date (YYYY-MM-DD)
        //End Date (YYYY-MM-DD)        
        //D/F B/T Usage and Count - Count represents the number of calls and Usage represents the number of rounded, billed minutes.
    }
}