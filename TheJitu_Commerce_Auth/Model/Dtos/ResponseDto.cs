﻿namespace TheJitu_Commerce_Auth.Model.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
