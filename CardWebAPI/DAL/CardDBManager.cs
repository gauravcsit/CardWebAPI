using CardWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CardWebAPI.DAL
{


    public class CardDBManager
    {
        private RequestResponse CheckValidationByLogs(RequestResponse result)
        {
            string strChar = result.CardNumber.Substring(0, 1);
            int strLength = result.CardNumber.Length;
            int strExpYear = Convert.ToInt32(result.ExpiryDate.Substring(2, 4));

            result.Result = "Valid";

            if (strChar != "3" && strChar != "4" && strChar != "5")
            {
                result.CardType = "Unknown";
            }

            else if (strChar == "4" && strLength == 16 && strExpYear % 4 == 0) // 1
            {
                result.CardType = "Visa";
            }
            else if (strChar == "5" && strLength == 16 && this.IsPrime(strExpYear))// 2
            {
                result.CardType = "MasterCard";
            }
            else if (strChar == "3" && strLength == 15) //  3
            {
                result.CardType = "Amex";
            }
            else if (strChar == "3" && strLength == 16) //  3
            {
                result.CardType = "JCB";
            }
            else
            {
                result.Result = "Invalid";
                result.CardType = "";
            }

            return result;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        internal RequestResponse CheckCardValid(string cardNumber, string expiryDate)
        {
            RequestResponse requestResponse = new RequestResponse();
            requestResponse.CardNumber = cardNumber;
            requestResponse.ExpiryDate = expiryDate;

            requestResponse = this.CheckValidationByLogs(requestResponse);


            SqlConnection con = null;
            DataSet ds = null;

            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            using (SqlCommand cmd = new SqlCommand("GetCardDetailsByCardNumber", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                    cmd.Parameters.AddWithValue("@Result", requestResponse.Result);
                    cmd.Parameters.AddWithValue("@ExpiryDate", requestResponse.ExpiryDate);
                    cmd.Parameters.AddWithValue("@CardType", requestResponse.CardType);

                    con.Open();
                    // for maintaining cuncurrency applied lock
                    SqlTransaction objtransaction = con.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    cmd.Transaction = objtransaction;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);

                    objtransaction.Commit();

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        requestResponse.CardNumber = Convert.ToString(ds.Tables[0].Rows[0]["CardNumber"]);
                        requestResponse.ExpiryDate = Convert.ToString(ds.Tables[0].Rows[0]["ExpiryDate"]);
                        requestResponse.Result = Convert.ToString(ds.Tables[0].Rows[0]["Result"]);
                        requestResponse.CardType = Convert.ToString(ds.Tables[0].Rows[0]["CardType"]);
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    con.Close();
                }
            }

            return requestResponse;

        }

        public List<LogTransaction> GetTransactionLog()
        {
            List<LogTransaction> listLog = new List<LogTransaction>();
            SqlConnection con = null;
            DataSet ds = null;

            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            using (SqlCommand cmd = new SqlCommand("GetLogTransactions", con))
            {
                try
                {
                    con.Open();
                    // for maintaining cuncurrency applied lock
                    SqlTransaction objtransaction = con.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    cmd.Transaction = objtransaction;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);

                    objtransaction.Commit();

                    for (int i = 0; ds.Tables[0] != null && i < ds.Tables[0].Rows.Count; i++)
                    {
                        LogTransaction log = new LogTransaction();
                        log.CardNumber = Convert.ToString(ds.Tables[0].Rows[i]["CardNumber"]);
                        log.ExpiryDate = Convert.ToString(ds.Tables[0].Rows[i]["ExpiryDate"]);
                        log.Result = Convert.ToString(ds.Tables[0].Rows[i]["Result"]);
                        log.CardType = Convert.ToString(ds.Tables[0].Rows[i]["CardType"]);
                        log.DateIn = Convert.ToDateTime(ds.Tables[0].Rows[i]["DateIn"]);

                        listLog.Add(log);
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    con.Close();
                }
            }

            return listLog;

        }
    }
}