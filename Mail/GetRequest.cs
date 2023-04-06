﻿using System;
using System.IO;
using System.Net;

namespace UNP.Models
{
    public class GetRequest
    {
        HttpWebRequest _request;
        string _address;

        public string Response { get; set; }

        public GetRequest(string address)
        {
            _address = address;
        }
        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "Get";

            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                    Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
            }

        }
    }
}