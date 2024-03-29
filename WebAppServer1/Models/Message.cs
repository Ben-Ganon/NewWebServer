﻿using System.ComponentModel.DataAnnotations;

namespace ServerFreak.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
         

        [Required]
        public string Content { get; set; }

        public string Type { get; set; }

        public string Created { get; set; }

        [Required]
        public bool Sent { get; set; }

       public Message() {}
        public Message(int id, string content, string type, string created, bool sent)
        {
            Id = id;
            Content = content;
            Type = type;
            Created = created;
            Sent = sent;
        }
        public Message(Message m)
        {
            Id = m.Id;
            Content = m.Content;
            Type = m.Type;
            Created = m.Created;
            Sent = m.Sent;
        }
    }
}
