using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    public class Comment
    {
        public string CommenterName { get; set; }
        public string CommentText { get; set; }

        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }
    }

    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthSeconds { get; set; }
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title;
            Author = author;
            LengthSeconds = lengthSeconds;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Create videos
            Video video1 = new Video("Introduction to C#", "John Doe", 300);
            Video video2 = new Video("Advanced C# Concepts", "Jane Smith", 600);
            Video video3 = new Video("C# Best Practices", "Peter Jones", 450);
            Video video4 = new Video("C# for Beginners", "Alice Brown", 240);

            // Add comments to video 1
            video1.AddComment(new Comment("User1", "Great video!"));
            video1.AddComment(new Comment("User2", "Very helpful."));
            video1.AddComment(new Comment("User3", "Could you explain more about...?"));

            // Add comments to video 2
            video2.AddComment(new Comment("Coder123", "Excellent explanation!"));
            video2.AddComment(new Comment("CSharpFan", "I learned a lot."));
            video2.AddComment(new Comment("DevNewbie", "Thanks for sharing!"));
            video2.AddComment(new Comment("ExpertDev", "Nice job!"));

            // Add comments to video 3
            video3.AddComment(new Comment("CodeMaster", "Concise and informative."));
            video3.AddComment(new Comment("TechGuru", "Well done!"));

            // Add comments to video 4
            video4.AddComment(new Comment("NewbieCoder", "Easy to understand."));
            video4.AddComment(new Comment("LearningDev", "Perfect for beginners!"));
            video4.AddComment(new Comment("CodeStudent", "Thank you!"));

            // Create a list of videos
            List<Video> videos = new List<Video> { video1, video2, video3, video4 };

            // Iterate through the list and display video information and comments
            foreach (Video video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthSeconds} seconds");
                Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

                Console.WriteLine("Comments:");
                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
                }

                Console.WriteLine(); // Add a blank line between videos
            }
        }
    }
}
