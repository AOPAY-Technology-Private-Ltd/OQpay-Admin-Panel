using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheEMIClubApplication.Model
{
    // -------------------------------
    // MODELS (map EXACTLY to your JSON)
    // -------------------------------
    public class CibilRoot
    {
        public string client_ref_num { get; set; }
        public string http_response_code { get; set; }
        public string message { get; set; }
        public string request_id { get; set; }
        public ResultWrapper result { get; set; }
        public string result_code { get; set; }
        public string Status { get; set; }
        public string Value { get; set; }
    }

    public class ResultWrapper { public ResultJson result_json { get; set; } }
    public class ResultJson { public INProfileResponse INProfileResponse { get; set; } }

    public class INProfileResponse
    {
        public CAIS_Account CAIS_Account { get; set; }
        public CAIS_Summary CAIS_Summary { get; set; }
        public CAPS CAPS { get; set; }
        public CreditProfileHeader CreditProfileHeader { get; set; }
        public Current_Application Current_Application { get; set; }
        public Header Header { get; set; }
        public Match_result Match_result { get; set; }
        public NonCreditCAPS NonCreditCAPS { get; set; }
        public SCORE SCORE { get; set; }
        public TotalCAPS_Summary TotalCAPS_Summary { get; set; }
        public UserMessage UserMessage { get; set; }
    }

    public class CAIS_Account { public List<CAIS_Account_DETAILS> CAIS_Account_DETAILS { get; set; } }

    public class CAIS_Account_DETAILS
    {
        public string AccountHoldertypeCode { get; set; }
        public string Account_Number { get; set; }
        public string Account_Status { get; set; }
        public string Account_Type { get; set; }
        public string Amount_Past_Due { get; set; }
        public List<CAIS_Account_History> CAIS_Account_History { get; set; } 
        public List<CAIS_Holder_Address_Details> CAIS_Holder_Address_Details { get; set; } 
        public List<CAIS_Holder_Details> CAIS_Holder_Details { get; set; }
        public List<CAIS_Holder_ID_Details> CAIS_Holder_ID_Details { get; set; } 
        public List<CAIS_Holder_Phone_Details> CAIS_Holder_Phone_Details { get; set; } 
        public string CurrencyCode { get; set; }
        public string Current_Balance { get; set; }
        public string Date_Closed { get; set; }
        public string DateOfAddition { get; set; }
        public string Date_of_Last_Payment { get; set; }
        public string Date_Reported { get; set; }
        public string Highest_Credit_or_Original_Loan_Amount { get; set; }
        public string Identification_Number { get; set; }
        public string Open_Date { get; set; }
        public string Payment_History_Profile { get; set; }
        public string Payment_Rating { get; set; }
        public string Portfolio_Type { get; set; }
        public string Repayment_Tenure { get; set; }
        public string Subscriber_Name { get; set; }
        public string SuitFiled_WilfulDefault { get; set; }
        public string Terms_Duration { get; set; }
        public string Terms_Frequency { get; set; }
        public string Credit_Limit_Amount { get; set; }
        public string Written_off_Settled_Status { get; set; }
        public string Written_Off_Amt_Principal { get; set; }
        public string Written_Off_Amt_Total { get; set; }
    }

    public class CAIS_Account_History
    {
        public string Asset_Classification { get; set; }
        public string Days_Past_Due { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class CAIS_Holder_Address_Details
    {
        public string Address_indicator_non_normalized { get; set; }
        public string CountryCode_non_normalized { get; set; }
        public string First_Line_Of_Address_non_normalized { get; set; }
        public string Second_Line_Of_Address_non_normalized { get; set; }
        public string State_non_normalized { get; set; }
        public string ZIP_Postal_Code_non_normalized { get; set; }
    }

    public class CAIS_Holder_Details
    {
        public string Date_of_birth { get; set; }
        public string Gender_Code { get; set; }
        public string Income_TAX_PAN { get; set; }
        public string Surname_Non_Normalized { get; set; }
        public string First_Name_Non_Normalized { get; set; }
    }

    public class CAIS_Holder_ID_Details
    {
        public string Income_TAX_PAN { get; set; }
        public string EMailId { get; set; }
    }

    public class CAIS_Holder_Phone_Details
    {
        public string EMailId { get; set; }
        public string Mobile_Telephone_Number { get; set; }
        public string Telephone_Number { get; set; }
        public string Telephone_Type { get; set; }
    }

    public class CAIS_Summary
    {
        public Credit_Account Credit_Account { get; set; }
        public Total_Outstanding_Balance Total_Outstanding_Balance { get; set; }
    }

    public class Credit_Account
    {
        public string CADSuitFiledCurrentBalance { get; set; }
        public string CreditAccountActive { get; set; }
        public string CreditAccountClosed { get; set; }
        public string CreditAccountDefault { get; set; }
        public string CreditAccountTotal { get; set; }
    }

    public class Total_Outstanding_Balance
    {
        public string Outstanding_Balance_All { get; set; }
        public string Outstanding_Balance_Secured { get; set; }
        public string Outstanding_Balance_Secured_Percentage { get; set; }
        public string Outstanding_Balance_UnSecured { get; set; }
        public string Outstanding_Balance_UnSecured_Percentage { get; set; }
    }

    public class CAPS { public CAPS_Summary CAPS_Summary { get; set; } }
    public class CAPS_Summary
    {
        public string CAPSLast180Days { get; set; }
        public string CAPSLast30Days { get; set; }
        public string CAPSLast7Days { get; set; }
        public string CAPSLast90Days { get; set; }
    }

    public class CreditProfileHeader
    {
        public string ReportDate { get; set; }
        public string ReportNumber { get; set; }
        public string ReportTime { get; set; }
        public string Subscriber_Name { get; set; }
        public string Version { get; set; }
    }

    public class Current_Application { public Current_Application_Details Current_Application_Details { get; set; } }

    public class Current_Application_Details
    {
        public string Amount_Financed { get; set; }
        public List<Current_Applicant_Address> Current_Applicant_Address_Details { get; set; }
        public Current_Applicant_Details Current_Applicant_Details { get; set; }
        public Current_Other_Details Current_Other_Details { get; set; }
        public string Duration_Of_Agreement { get; set; }
        public string Enquiry_Reason { get; set; }
    }

    public class Current_Applicant_Address { public string Country_Code { get; set; } }
    public class Current_Applicant_Details
    {
        public string First_Name { get; set; }
        public string IncomeTaxPan { get; set; }
        public string MobilePhoneNumber { get; set; }
    }
    public class Current_Other_Details { public string Income { get; set; } }

    public class Header
    {
        public string ReportDate { get; set; }
        public string ReportTime { get; set; }
        public string SystemCode { get; set; }
    }

    public class Match_result { public string Exact_match { get; set; } }
    public class NonCreditCAPS { public NonCreditCAPS_Summary NonCreditCAPS_Summary { get; set; } }
    public class NonCreditCAPS_Summary
    {
        public string NonCreditCAPSLast180Days { get; set; }
        public string NonCreditCAPSLast30Days { get; set; }
        public string NonCreditCAPSLast7Days { get; set; }
        public string NonCreditCAPSLast90Days { get; set; }
    }
    public class SCORE { public string BureauScore { get; set; } }
    public class TotalCAPS_Summary
    {
        public string TotalCAPSLast180Days { get; set; }
        public string TotalCAPSLast30Days { get; set; }
        public string TotalCAPSLast7Days { get; set; }
        public string TotalCAPSLast90Days { get; set; }
    }
    public class UserMessage { public string UserMessageText { get; set; } }

 

}