﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Model.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
