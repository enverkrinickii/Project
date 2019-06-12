using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ChatRoom
    {
        [Key]
        public int ChatRoomId { get; set; }

        public string Name => $"ChatRoom {ChatRoomId}";

        public int PictureId { get; set; }
        
        [ForeignKey("PictureId")]
        public Picture Picture { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public ChatRoom()
        {
            Users = new List<ApplicationUser>();
        }
    }
}