﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IESMater_WebAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class xPenEntities : DbContext
    {
        public xPenEntities()
            : base("name=xPenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<IESAcademicProfile> IESAcademicProfiles { get; set; }
        public virtual DbSet<IESClass> IESClasses { get; set; }
        public virtual DbSet<IESClass_Subject> IESClass_Subject { get; set; }
        public virtual DbSet<IESCollege> IESColleges { get; set; }
        public virtual DbSet<IESCourse> IESCourses { get; set; }
        public virtual DbSet<IESObjectiveQuestion> IESObjectiveQuestions { get; set; }
        public virtual DbSet<IESOrder> IESOrders { get; set; }
        public virtual DbSet<IESOrderTransaction> IESOrderTransactions { get; set; }
        public virtual DbSet<IESPaper_Question> IESPaper_Question { get; set; }
        public virtual DbSet<IESQuestionPaper> IESQuestionPapers { get; set; }
        public virtual DbSet<IESQuestion> IESQuestions { get; set; }
        public virtual DbSet<IESSemester> IESSemesters { get; set; }
        public virtual DbSet<IESStream> IESStreams { get; set; }
        public virtual DbSet<IESSubject> IESSubjects { get; set; }
        public virtual DbSet<IESUniversity> IESUniversities { get; set; }
        public virtual DbSet<IESUserProfile> IESUserProfiles { get; set; }
        public virtual DbSet<ViewIESCollege> ViewIESColleges { get; set; }
        public virtual DbSet<ViewIESCollegeWithCount> ViewIESCollegeWithCounts { get; set; }
        public virtual DbSet<ViewIESCountinStream> ViewIESCountinStreams { get; set; }
        public virtual DbSet<ViewIESPaperQuestion> ViewIESPaperQuestions { get; set; }
        public virtual DbSet<ViewIESQuestionPaper> ViewIESQuestionPapers { get; set; }
        public virtual DbSet<ViewIESQuestion> ViewIESQuestions { get; set; }
        public virtual DbSet<ViewIESStream> ViewIESStreams { get; set; }
        public virtual DbSet<ViewIESStreamofCollege> ViewIESStreamofColleges { get; set; }
        public virtual DbSet<ViewIESSubject> ViewIESSubjects { get; set; }
        public virtual DbSet<ViewIESUniversityWithCount> ViewIESUniversityWithCounts { get; set; }
        public virtual DbSet<ViewIESAcademicProfile> ViewIESAcademicProfiles { get; set; }
    }
}
