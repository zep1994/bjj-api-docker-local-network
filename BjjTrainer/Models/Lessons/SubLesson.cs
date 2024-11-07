﻿using BjjTrainer.Models.Lessons;

namespace BjjTrainer.Models
{
    public class SubLesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TimeSpan Duration { get; set; }
        public string VideoUrl { get; set; }
        public string DocumentUrl { get; set; }
        public List<string> Tags { get; set; }
        public string SkillLevel { get; set; }
        public string Notes { get; set; }
        public int LessonSectionId { get; set; }
        public LessonSection LessonSection { get; set; }
    }

}
