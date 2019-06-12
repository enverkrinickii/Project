using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate => DateTime.Now;

        public string UserId { get; set; }

        public int ChatRoomId { get; set; }

        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}