﻿namespace Application.DTO.Post.Request
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Type { get; set; }
        public string Category { get; set; }
        public int CustomerId { get; set; }
    }
}
