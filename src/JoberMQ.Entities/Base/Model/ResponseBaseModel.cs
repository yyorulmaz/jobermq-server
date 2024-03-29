﻿namespace JoberMQ.Entities.Base.Model
{
    public class ResponseBaseModel
    {
        public ResponseBaseModel()
        {
            IsOnline = false;
            IsSuccess = false;
            Message = null;
        }
        public bool? IsOnline { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
