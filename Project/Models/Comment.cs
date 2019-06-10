using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string CommentMessage { get; set; }

        public DateTime CreatedDate => DateTime.Now;

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int ParentCommentId { get; set; }

        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }
    }
}