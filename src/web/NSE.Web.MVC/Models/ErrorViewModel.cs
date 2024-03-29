using System;
using System.Collections.Generic;

namespace NSE.Web.MVC.Models
{
    public class ErrorViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }

    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string> Messages { get; set; }
    }

}

