using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace TheEMIClubApplication.Admin
{
    public class CustomerPhotoUpdater
    {
        string connStr = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// Updates a single photo path column in CustomerMaster based on CustomerCode.
        /// </summary>
        /// <param name="customerCode">Customer unique code</param>
        /// <param name="columnName">Column to update (allowed: IMEINumber1_SealPhotoPath, IMEINumber2_SealPhotoPath, IMEINumber_PhotoPath, Invoice_Path)</param>
        /// <param name="newValue">New file path or value</param>
        /// <returns>True if update succeeded, False otherwise</returns>
        //public bool UpdateCustomerPhotoPath(string customerCode, string columnName, FileUpload fileUpload)
        //{
        //    try
        //    {
        //        // Validation
        //        if (string.IsNullOrEmpty(customerCode) || string.IsNullOrEmpty(columnName))
        //            throw new ArgumentException("CustomerCode and ColumnName are required.");

        //        if (fileUpload == null || !fileUpload.HasFile)
        //            throw new ArgumentException("Please select a file to upload.");

        //        // Determine folder and filename
        //        string folderName = "";
        //        string filePrefix = "";

        //        switch (columnName)
        //        {
        //            case "IMEINumber1_SealPhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumber1Image";
        //                break;

        //            case "IMEINumber2_SealPhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumber2Image";
        //                break;

        //            case "IMEINumber_PhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumberImage";
        //                break;

        //            case "Invoice_Path":
        //                folderName = "Invoices";
        //                filePrefix = "InvoiceImage";
        //                break;

        //            default:
        //                throw new ArgumentException("Invalid column name.");
        //        }

        //        // Physical server path
        //        string physicalFolder = "https://api.oqpay.in" + ("/" + folderName + "/");
        //        if (!Directory.Exists(physicalFolder))
        //            Directory.CreateDirectory(physicalFolder);

        //        // Unique file name
        //        string timeStamp = DateTime.Now.Ticks.ToString();
        //        string fileExtension = Path.GetExtension(fileUpload.FileName);
        //        string fileName = $"{timeStamp}_{filePrefix}{fileExtension}";

        //        // Save file to server
        //        string savePath = Path.Combine(physicalFolder, fileName);
        //        fileUpload.SaveAs(savePath);

        //        // Relative path for DB
        //        string relativePath = "/" + folderName + "/" + fileName;

        //        // Update DB using stored procedure
        //        using (SqlConnection con = new SqlConnection(connStr))
        //        using (SqlCommand cmd = new SqlCommand("usp_UpdateCustomerPhotoPath_Single", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
        //            cmd.Parameters.AddWithValue("@ColumnName", columnName);
        //            cmd.Parameters.AddWithValue("@NewValue", relativePath);

        //            con.Open();
        //            int rows = cmd.ExecuteNonQuery();
        //            con.Close();

        //            return rows > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error uploading photo: " + ex.Message);
        //    }
        //}


        //public async Task<bool> UpdateCustomerPhotoPathAsync(string customerCode, string columnName, FileUpload fileUpload)
        //{
        //    try
        //    {
        //        // Validation
        //        if (string.IsNullOrEmpty(customerCode) || string.IsNullOrEmpty(columnName))
        //            throw new ArgumentException("CustomerCode and ColumnName are required.");

        //        if (fileUpload == null || !fileUpload.HasFile)
        //            throw new ArgumentException("Please select a file to upload.");

        //        // Determine folder and filename
        //        string folderName = "";
        //        string filePrefix = "";

        //        switch (columnName)
        //        {
        //            case "IMEINumber1_SealPhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumber1Image";
        //                break;

        //            case "IMEINumber2_SealPhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumber2Image";
        //                break;

        //            case "IMEINumber_PhotoPath":
        //                folderName = "IMEIPhotos";
        //                filePrefix = "IMEINumberImage";
        //                break;

        //            case "Invoice_Path":
        //                folderName = "Invoices";
        //                filePrefix = "InvoiceImage";
        //                break;

        //            default:
        //                throw new ArgumentException("Invalid column name.");
        //        }

        //        // Generate unique file name
        //        string timeStamp = DateTime.Now.Ticks.ToString();
        //        string fileExtension = Path.GetExtension(fileUpload.FileName);
        //        string fileName = $"{timeStamp}_{filePrefix}{fileExtension}";

        //        // Remote API URL to upload file
        //        string uploadUrl = $"https://api.oqpay.in/{folderName}"; // Replace with actual API endpoint

        //        // Upload file using HttpClient
        //        using (var client = new HttpClient())
        //        using (var content = new MultipartFormDataContent())
        //        {
        //            byte[] fileBytes;
        //            using (var stream = fileUpload.PostedFile.InputStream)
        //            {
        //                fileBytes = new byte[stream.Length];
        //                stream.Read(fileBytes, 0, fileBytes.Length);
        //            }

        //            content.Add(new ByteArrayContent(fileBytes), "file", fileName);

        //            // Post to remote server
        //            HttpResponseMessage response = await client.PostAsync(uploadUrl, content);
        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new Exception("File upload failed: " + response.ReasonPhrase);
        //            }
        //        }

        //        // Construct relative path to store in DB
        //        string relativePath = "/" + folderName + "/" + fileName;

        //        // Update DB using stored procedure
        //        using (SqlConnection con = new SqlConnection(connStr))
        //        using (SqlCommand cmd = new SqlCommand("usp_UpdateCustomerPhotoPath_Single", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
        //            cmd.Parameters.AddWithValue("@ColumnName", columnName);
        //            cmd.Parameters.AddWithValue("@NewValue", relativePath);

        //            con.Open();
        //            int rows = cmd.ExecuteNonQuery();
        //            con.Close();

        //            return rows > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error uploading photo: " + ex.Message);
        //    }
        //}


        public async Task<bool> UpdateCustomerPhotoPathAsync(string customerCode, string columnName, string ImagePath)
        {
            try
            {
                // Validation
                if (string.IsNullOrEmpty(customerCode))
                    throw new ArgumentException("CustomerCode are required.");
                if (string.IsNullOrEmpty(columnName))
                    throw new ArgumentException(" ColumnName are required.");
                if (string.IsNullOrEmpty(ImagePath))
                    throw new ArgumentException("ImagePath are required.");

                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("usp_UpdateCustomerPhotoPath_Single", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    cmd.Parameters.AddWithValue("@NewValue", ImagePath);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();
                    if(rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading photo: " + ex.Message);
            }
        }

        //public async Task<bool> UpdateCustomerPhotoPathAsyncs(string customerCode, string columnName, string imageFilePath)
        //{
        //    try
        //    {
        //        // Validation
        //        if (string.IsNullOrEmpty(customerCode))
        //            throw new ArgumentException("CustomerCode is required.");
        //        if (string.IsNullOrEmpty(columnName))
        //            throw new ArgumentException("ColumnName is required.");
        //        if (string.IsNullOrEmpty(imageFilePath) || !File.Exists(imageFilePath))
        //            throw new ArgumentException("Valid image file path is required.");

        //        using (var httpClient = new HttpClient())
        //        using (var formData = new MultipartFormDataContent())
        //        {
        //            // Read the file
        //            var fileBytes = await File.ReadAllBytesAsync(imageFilePath);
        //            var fileContent = new ByteArrayContent(fileBytes);
        //            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

        //            // Add file and other fields to the form
        //            formData.Add(fileContent, "Image_FileName", Path.GetFileName(imageFilePath));
        //            formData.Add(new StringContent(customerCode), "CustomerCode");
        //            formData.Add(new StringContent(columnName), "ColumnName");

        //            // Call API
        //            string apiUrl = "https://api.oqpay.in/api/V1/OQFinance/UpdateCustomerPhotoPath";
        //            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, formData);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                string errorMsg = await response.Content.ReadAsStringAsync();
        //                throw new Exception($"API call failed: {errorMsg}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error uploading photo via API: " + ex.Message);
        //    }
        //}


    }
}